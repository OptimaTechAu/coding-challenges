using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using SupermarketCheckout.Lambda.Interfaces;

namespace SupermarketCheckout.Lambda.Services;

public class DynamoDbCheckoutStorageProvider : ICheckoutStorageProvider
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    private readonly string _tableName;
    private readonly string SESSION_ID_KEY = "SessionId";
    private readonly string SKU_KEY = "SKU";
    private readonly string QTY_ATTRIBUTE = "Quantity";

    /// <summary>
    /// The default DynamoDB table name for checkout sessions.
    /// </summary>
    public const string DefaultTableName = "CheckoutSessions";

    /// <summary>
    /// Initializes a new DynamoDB checkout storage with the specified session ID.
    /// </summary>
    /// <param name="dynamoDbClient">The DynamoDB client.</param>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="tableName">The table name (optional, defaults to CheckoutSessions).</param>
    public DynamoDbCheckoutStorageProvider(IAmazonDynamoDB dynamoDbClient, string? tableName = null)
    {
        _dynamoDbClient = dynamoDbClient ?? throw new ArgumentNullException(nameof(dynamoDbClient));
        _tableName = tableName ?? Environment.GetEnvironmentVariable("CHECKOUT_TABLE_NAME") ?? DefaultTableName;
    }

    /// <inheritdoc />
    public async Task AddSkuAsync(string sessionId, string sku, int quantity = 1)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        var normalizedSku = sku.ToUpperInvariant();

        var request = new UpdateItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                [SESSION_ID_KEY] = new AttributeValue { S = sessionId },
                [SKU_KEY] = new AttributeValue { S = normalizedSku }
            },
            UpdateExpression = $"SET {QTY_ATTRIBUTE} = if_not_exists({QTY_ATTRIBUTE}, :zero) + :qty",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":qty"] = new AttributeValue { N = quantity.ToString() },
                [":zero"] = new AttributeValue { N = "0" }
            }
        };

        await _dynamoDbClient.UpdateItemAsync(request);
    }

    /// <inheritdoc />
    public async Task<int> GetSkuQuantityAsync(string sessionId, string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);

        var normalizedSku = sku.ToUpperInvariant();

        var request = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                [SESSION_ID_KEY] = new AttributeValue { S = sessionId },
                [SKU_KEY] = new AttributeValue { S = normalizedSku }
            },
            ProjectionExpression = QTY_ATTRIBUTE
        };

        var response = await _dynamoDbClient.GetItemAsync(request);

        if (response.Item == null || !response.Item.ContainsKey(QTY_ATTRIBUTE))
        {
            return 0;
        }

        return int.Parse(response.Item[QTY_ATTRIBUTE].N);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyDictionary<string, int>> GetAllSkusAsync(string sessionId)
    {
        var request = new QueryRequest
        {
            TableName = _tableName,
            KeyConditionExpression = $"{SESSION_ID_KEY} = :sessionId",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                [":sessionId"] = new AttributeValue { S = sessionId }
            },
            ProjectionExpression = $"{SKU_KEY}, {QTY_ATTRIBUTE}"
        };

        var response = await _dynamoDbClient.QueryAsync(request);

        var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var item in response.Items)
        {
            if (item.TryGetValue(SKU_KEY, out var skuAttr) &&
                item.TryGetValue(QTY_ATTRIBUTE, out var qtyAttr))
            {
                result[skuAttr.S] = int.Parse(qtyAttr.N);
            }
        }

        return result;
    }

    /// <inheritdoc />
    public async Task ClearAsync(string sessionId)
    {
        // First, get all items for this session
        var items = await GetAllSkusAsync(sessionId);

        // Delete each item
        foreach (var sku in items.Keys)
        {
            var request = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    [SESSION_ID_KEY] = new AttributeValue { S = sessionId },
                    [SKU_KEY] = new AttributeValue { S = sku }
                }
            };

            await _dynamoDbClient.DeleteItemAsync(request);
        }
    }
}
