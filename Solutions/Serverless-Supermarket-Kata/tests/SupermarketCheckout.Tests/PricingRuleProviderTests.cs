using SupermarketCheckout.Lambda.Models;
using SupermarketCheckout.Lambda.Services;

namespace SupermarketCheckout.Tests;

/// <summary>
/// Unit tests for the PricingRuleProvider class.
/// </summary>
public class PricingRuleProviderTests
{
    [Fact]
    public void GetPricingRule_ExistingSku_ReturnsPricingRule()
    {
        var provider = new PricingRuleProvider();
        var rule = provider.GetPricingRule("A");
        Assert.NotNull(rule);
        Assert.Equal("A", rule.Sku);
        Assert.Equal(50, rule.UnitPrice);
    }

    [Fact]
    public void GetPricingRule_NonExistingSku_ReturnsNull()
    {
        var provider = new PricingRuleProvider();
        var rule = provider.GetPricingRule("X");
        Assert.Null(rule);
    }

    [Fact]
    public void GetPricingRule_CaseInsensitive_ReturnsPricingRule()
    {
        var provider = new PricingRuleProvider();
        var ruleLower = provider.GetPricingRule("a");
        var ruleUpper = provider.GetPricingRule("A");
        Assert.NotNull(ruleLower);
        Assert.NotNull(ruleUpper);
        Assert.Equal(ruleLower.Sku, ruleUpper.Sku);
    }

    [Fact]
    public void GetAllPricingRules_ReturnsAllRules()
    {
        var provider = new PricingRuleProvider();
        var rules = provider.GetAllPricingRules().ToList();
        Assert.Equal(4, rules.Count);
        Assert.Contains(rules, r => r.Sku == "A");
        Assert.Contains(rules, r => r.Sku == "B");
        Assert.Contains(rules, r => r.Sku == "C");
        Assert.Contains(rules, r => r.Sku == "D");
    }

    [Fact]
    public void GetPricingRule_ItemWithSpecialOffer_HasSpecialOfferTrue()
    {
        var provider = new PricingRuleProvider();
        var rule = provider.GetPricingRule("A");
        Assert.NotNull(rule);
        Assert.True(rule.HasSpecialOffer);
        Assert.Equal(3, rule.SpecialQuantity);
        Assert.Equal(130, rule.SpecialPrice);
    }

    [Fact]
    public void GetPricingRule_ItemWithoutSpecialOffer_HasSpecialOfferFalse()
    {
        var provider = new PricingRuleProvider();
        var rule = provider.GetPricingRule("C");
        Assert.NotNull(rule);
        Assert.False(rule.HasSpecialOffer);
        Assert.Null(rule.SpecialQuantity);
        Assert.Null(rule.SpecialPrice);
    }

    [Fact]
    public void Constructor_WithCustomRules_UsesCustomRules()
    {
        var customRules = new[]
        {
            new PricingRule { Sku = "X", UnitPrice = 100, SpecialQuantity = 2, SpecialPrice = 150 }
        };
        var provider = new PricingRuleProvider();
        provider.SetPricingRules(customRules);
        var rule = provider.GetPricingRule("X");
        Assert.NotNull(rule);
        Assert.Equal("X", rule.Sku);
        Assert.Equal(100, rule.UnitPrice);
        Assert.True(rule.HasSpecialOffer);
    }

    [Fact]
    public void GetPricingRule_NullSku_ThrowsArgumentNullException()
    {
        var provider = new PricingRuleProvider();
        Assert.Throws<ArgumentNullException>(() => provider.GetPricingRule(null!));
    }

    [Fact]
    public void GetPricingRule_EmptySku_ThrowsArgumentException()
    {
        var provider = new PricingRuleProvider();
        Assert.Throws<ArgumentException>(() => provider.GetPricingRule(""));
    }

    [Fact]
    public void CalculateTotalPriceOfQuantityOfItems_NoSpecialOffer_ReturnsCorrectTotal()
    {
        var rule = new PricingRule { Sku = "C", UnitPrice = 20 };
        var total = rule.CalculateTotalPriceOfQuantityOfItems(5);
        Assert.Equal(100, total); // 5 * 20
    }

    [Fact]
    public void CalculateTotalPriceOfQuantityOfItems_WithSpecialOffer_AppliesOfferCorrectly()
    {
        var rule = new PricingRule { Sku = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 };
        var total = rule.CalculateTotalPriceOfQuantityOfItems(7);
        // 2 offers (6 items) + 1 item at unit price: 2*130 + 1*50 = 260 + 50 = 310
        Assert.Equal(310, total);
    }

    [Fact]
    public void CalculateTotalPriceOfQuantityOfItems_WithSpecialOffer_ExactOfferQuantity()
    {
        var rule = new PricingRule { Sku = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 };
        var total = rule.CalculateTotalPriceOfQuantityOfItems(4);
        // 2 offers (4 items): 2*45 = 90
        Assert.Equal(90, total);
    }

    [Fact]
    public void CalculateTotalPriceOfQuantityOfItems_WithSpecialOffer_LessThanOfferQuantity()
    {
        var rule = new PricingRule { Sku = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 };
        var total = rule.CalculateTotalPriceOfQuantityOfItems(1);
        // No offer, 1*30 = 30
        Assert.Equal(30, total);
    }
}
