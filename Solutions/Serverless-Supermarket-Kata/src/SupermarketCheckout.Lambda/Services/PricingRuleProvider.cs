using SupermarketCheckout.Lambda.Interfaces;
using SupermarketCheckout.Lambda.Models;

namespace SupermarketCheckout.Lambda.Services;

/// <summary>
/// Default implementation of IPricingRuleProvider.
/// Provides the current pricing rules for the supermarket.
/// </summary>
public class PricingRuleProvider : IPricingRuleProvider
{
    private Dictionary<string, PricingRule> _pricingRules;

    public PricingRuleProvider()
    {
        _pricingRules = GetDefaultPricingRules().ToDictionary(
            rule => rule.Sku.ToUpperInvariant(),
            rule => rule);
    }

    // This is for testing but could also be used to update pricing rules at runtime if there was a mechanism to persist changes.
    public void SetPricingRules(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules.ToDictionary(
            rule => rule.Sku.ToUpperInvariant(),
            rule => rule);
    }

    /// <inheritdoc />
    public PricingRule? GetPricingRule(string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);
        return _pricingRules.GetValueOrDefault(sku.ToUpperInvariant());
    }

    /// <inheritdoc />
    public IEnumerable<PricingRule> GetAllPricingRules()
    {
        return _pricingRules.Values;
    }

    private IEnumerable<PricingRule> GetDefaultPricingRules()
    {
        return new[]
        {
            new PricingRule
            {
                Sku = "A",
                UnitPrice = 50,
                SpecialQuantity = 3,
                SpecialPrice = 130
            },
            new PricingRule
            {
                Sku = "B",
                UnitPrice = 30,
                SpecialQuantity = 2,
                SpecialPrice = 45
            },
            new PricingRule
            {
                Sku = "C",
                UnitPrice = 20
            },
            new PricingRule
            {
                Sku = "D",
                UnitPrice = 15
            }
        };
    }
}
