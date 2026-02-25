using Amazon.Lambda.Core;
using SupermarketCheckout.Lambda.Interfaces;

namespace SupermarketCheckout.Lambda.Services;

public class CheckoutHandler : ICheckout
{
    private readonly IPricingRuleProvider _pricingRuleProvider;
    private readonly ICheckoutStorageProvider _storage;
    private readonly ILambdaLogger _logger;

    public CheckoutHandler(IPricingRuleProvider pricingRuleProvider, ICheckoutStorageProvider storageProvider, ILambdaLogger logger, string sessionId)
    {
        _pricingRuleProvider = pricingRuleProvider ?? throw new ArgumentNullException(nameof(pricingRuleProvider));
        _storage = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        SessionId = sessionId ?? throw new ArgumentNullException(nameof(sessionId));

        _logger.LogDebug("CheckoutHandler initialized for session {sessionId} with storage type {storageType}",
            SessionId, storageProvider.GetType().Name);
    }

    public string SessionId { get; }

    /// <inheritdoc />
    public void Scan(string item)
    {
        AddScannedSkuToBasket(item).GetAwaiter().GetResult();
    }

    public async Task AddScannedSkuToBasket(string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);
        _logger.LogDebug("Scanning sku: {sku} for session {sessionId}", sku, SessionId);
        var normalizedItem = sku.ToUpperInvariant();
        var pricingRule = _pricingRuleProvider.GetPricingRule(normalizedItem);

        if (pricingRule == null)
        {
            var skus = _pricingRuleProvider.GetAllPricingRules().Select(r => r.Sku).ToList();
            _logger.LogError("Unknown sku scanned: {sku} for session {sessionId}. Available skus({skuCount}): {skus}", sku, SessionId, skus.Count, string.Join(", ", skus));
            throw new ArgumentException($"Unknown sku: {sku}", nameof(sku));
        }
        _logger.LogDebug("Sku {sku} is valid with unit price {unitPrice} for session {sessionId}", normalizedItem, pricingRule.UnitPrice, SessionId);
        await _storage.AddSkuAsync(SessionId, normalizedItem);
        _logger.LogDebug("Sku {sku} added to session {sessionId}", normalizedItem, SessionId);
    }

    /// <inheritdoc />
    public int GetTotalPrice()
    {
        return GetTotalPriceAsync().GetAwaiter().GetResult();
    }

    public async Task<int> GetTotalPriceAsync()
    {
        _logger.LogDebug("Calculating total price for session {sessionId}", SessionId);
        var items = await _storage.GetAllSkusAsync(SessionId);
        var total = 0;

        foreach (var (sku, quantity) in items)
        {
            var pricingRule = _pricingRuleProvider.GetPricingRule(sku);
            if (pricingRule == null)
            {
                throw new InvalidOperationException($"Pricing rule not found for SKU: {sku}");
            }
            total += pricingRule.CalculateTotalPriceOfQuantityOfItems(quantity);
        }
        _logger.LogDebug("Total price calculated for session {sessionId}: {total}", SessionId, total);
        return total;
    }

    public async Task<IReadOnlyDictionary<string, int>> GetAllSkusAndCountsAsync()
    {
        _logger.LogDebug("Retrieving sku counts for session {sessionId}", SessionId);
        var skus = await _storage.GetAllSkusAsync(SessionId);
        _logger.LogDebug("Retrieved sku counts for session {sessionId}: {skuCounts}", SessionId, string.Join(", ", skus.Select(kv => $"{kv.Key}: {kv.Value}")));
        return skus;
    }
}
