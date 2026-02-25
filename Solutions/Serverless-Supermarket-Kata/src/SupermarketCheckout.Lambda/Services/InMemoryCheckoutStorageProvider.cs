using SupermarketCheckout.Lambda.Interfaces;

namespace SupermarketCheckout.Lambda.Services;

/// <summary>
/// In-memory implementation of ICheckoutStorage.
/// Useful for testing and single-request Lambda invocations.
/// </summary>
public class InMemoryCheckoutStorageProvider : ICheckoutStorageProvider
{
    private readonly Dictionary<string, int> _items;

    /// <summary>
    /// Initializes a new in-memory checkout storage with the specified session ID.
    /// </summary>
    public InMemoryCheckoutStorageProvider()
    {
        _items = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public Task AddSkuAsync(string sessionId, string sku, int quantity = 1)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        var normalizedSku = sku.ToUpperInvariant();

        if (_items.TryGetValue(normalizedSku, out var currentQuantity))
        {
            _items[normalizedSku] = currentQuantity + quantity;
        }
        else
        {
            _items[normalizedSku] = quantity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<int> GetSkuQuantityAsync(string sessionId, string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);

        var normalizedSku = sku.ToUpperInvariant();
        var quantity = _items.GetValueOrDefault(normalizedSku, 0);

        return Task.FromResult(quantity);
    }

    /// <inheritdoc />
    public Task<IReadOnlyDictionary<string, int>> GetAllSkusAsync(string sessionId)
    {
        IReadOnlyDictionary<string, int> result = new Dictionary<string, int>(_items);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task ClearAsync(string sessionId)
    {
        _items.Clear();
        return Task.CompletedTask;
    }
}
