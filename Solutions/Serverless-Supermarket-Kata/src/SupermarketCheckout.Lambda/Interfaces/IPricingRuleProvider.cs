using SupermarketCheckout.Lambda.Models;

namespace SupermarketCheckout.Lambda.Interfaces;

public interface IPricingRuleProvider
{
    /// <summary>
    /// Gets the pricing rule for a specific SKU.
    /// </summary>
    /// <param name="sku">The Stock Keeping Unit identifier.</param>
    /// <returns>The pricing rule for the SKU, or null if the SKU is not found.</returns>
    PricingRule? GetPricingRule(string sku);

    /// <summary>
    /// Gets all available pricing rules.
    /// </summary>
    /// <returns>A collection of all pricing rules.</returns>
    IEnumerable<PricingRule> GetAllPricingRules();
}
