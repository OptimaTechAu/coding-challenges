using SupermarketCheckout.Lambda.Interfaces;
using SupermarketCheckout.Lambda.Services;

namespace SupermarketCheckout.Tests;

public class CheckoutTests
{
    private readonly IPricingRuleProvider _pricingRuleProvider;

    public CheckoutTests()
    {
        _pricingRuleProvider = new PricingRuleProvider();
    }

    private CheckoutHandler CreateCheckout()
    {
        return new CheckoutHandler(_pricingRuleProvider, new InMemoryCheckoutStorageProvider(), new TestLambdaLogger(), Guid.NewGuid().ToString());
    }

    [Fact]
    public void GetTotalPrice_NoItemsScanned_ReturnsZero()
    {
        var checkout = CreateCheckout();
        var total = checkout.GetTotalPrice();
        Assert.Equal(0, total);
    }

    [Theory]
    [InlineData("A", 50)]
    [InlineData("B", 30)]
    [InlineData("C", 20)]
    [InlineData("D", 15)]
    public void GetTotalPrice_SingleItem_ReturnsUnitPrice(string sku, int expectedPrice)
    {
        var checkout = CreateCheckout();
        checkout.Scan(sku);
        var total = checkout.GetTotalPrice();
        Assert.Equal(expectedPrice, total);
    }

    [Theory]
    [InlineData("a", 50)]
    [InlineData("b", 30)]
    [InlineData("c", 20)]
    [InlineData("d", 15)]
    public void GetTotalPrice_SingleItemLowerCase_ReturnsUnitPrice(string sku, int expectedPrice)
    {
        var checkout = CreateCheckout();
        checkout.Scan(sku);
        var total = checkout.GetTotalPrice();
        Assert.Equal(expectedPrice, total);
    }

    [Fact]
    public void GetTotalPrice_TwoAs_ReturnsDoubleUnitPrice()
    {
        var checkout = CreateCheckout();
        checkout.Scan("A");
        checkout.Scan("A");
        var total = checkout.GetTotalPrice();
        Assert.Equal(100, total);
    }

    [Fact]
    public void GetTotalPrice_ThreeAs_AppliesSpecialOffer()
    {
        var checkout = CreateCheckout();
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("A");
        var total = checkout.GetTotalPrice();
        Assert.Equal(130, total);
    }

    [Fact]
    public void GetTotalPrice_FourAs_AppliesSpecialOfferAndAddsOneRegular()
    {
        var checkout = CreateCheckout();
        for (int i = 0; i < 4; i++)
        {
            checkout.Scan("A");
        }
        var total = checkout.GetTotalPrice();
        Assert.Equal(180, total); // 130 + 50
    }

    [Fact]
    public void GetTotalPrice_SixAs_AppliesTwoSpecialOffers()
    {
        var checkout = CreateCheckout();
        for (int i = 0; i < 6; i++)
        {
            checkout.Scan("A");
        }
        var total = checkout.GetTotalPrice();
        Assert.Equal(260, total); // 130 * 2
    }

    [Fact]
    public void GetTotalPrice_TwoBs_AppliesSpecialOffer()
    {
        var checkout = CreateCheckout();
        checkout.Scan("B");
        checkout.Scan("B");
        var total = checkout.GetTotalPrice();
        Assert.Equal(45, total);
    }

    [Fact]
    public void GetTotalPrice_ThreeBs_AppliesSpecialOfferAndAddsOneRegular()
    {
        var checkout = CreateCheckout();
        for (int i = 0; i < 3; i++)
        {
            checkout.Scan("B");
        }
        var total = checkout.GetTotalPrice();
        Assert.Equal(75, total); // 45 + 30
    }

    [Fact]
    public void GetTotalPrice_MixedItems_CalculatesCorrectTotal()
    {
        var checkout = CreateCheckout();
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");
        checkout.Scan("D");
        var total = checkout.GetTotalPrice();
        Assert.Equal(115, total); // 50 + 30 + 20 + 15
    }

    [Fact]
    public void GetTotalPrice_ItemsInAnyOrder_RecognizesSpecialOffer()
    {
        var checkout = CreateCheckout();
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");
        var total = checkout.GetTotalPrice();
        Assert.Equal(95, total); // 45 (2 Bs special) + 50 (1 A)
    }

    [Fact]
    public void GetTotalPrice_ComplexBasket_AppliesMultipleOffers()
    {
        var checkout = CreateCheckout();
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("C");
        checkout.Scan("B");
        checkout.Scan("D");
        checkout.Scan("A");
        var total = checkout.GetTotalPrice();
        Assert.Equal(210, total); // 130 (3 As) + 45 (2 Bs) + 20 (C) + 15 (D)
    }

    [Fact]
    public void Scan_UnknownItem_ThrowsArgumentException()
    {
        var checkout = CreateCheckout();
        var exception = Assert.Throws<ArgumentException>(() => checkout.Scan("X"));
        Assert.Contains("Unknown sku", exception.Message);
    }

    [Fact]
    public void Scan_NullItem_ThrowsArgumentNullException()
    {
        var checkout = CreateCheckout();
        Assert.Throws<ArgumentNullException>(() => checkout.Scan(null!));
    }

    [Fact]
    public void Scan_EmptyItem_ThrowsArgumentException()
    {
        var checkout = CreateCheckout();
        Assert.Throws<ArgumentException>(() => checkout.Scan(""));
    }

    [Fact]
    public async Task GetAllItemCounts_ReturnsCorrectCounts()
    {
        var checkout = CreateCheckout();
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        var breakdown = await checkout.GetAllSkusAndCountsAsync();
        Assert.Equal(2, breakdown.Count);
        Assert.Equal(3, breakdown["A"]);
        Assert.Equal(1, breakdown["B"]);
    }

    [Theory]
    [InlineData(new[] { "A" }, 50)]
    [InlineData(new[] { "A", "B" }, 80)]
    [InlineData(new[] { "C", "D", "B", "A" }, 115)]
    [InlineData(new[] { "A", "A" }, 100)]
    [InlineData(new[] { "A", "A", "A" }, 130)]
    [InlineData(new[] { "A", "A", "A", "A" }, 180)]
    [InlineData(new[] { "A", "A", "A", "A", "A" }, 230)]
    [InlineData(new[] { "A", "A", "A", "A", "A", "A" }, 260)]
    [InlineData(new[] { "A", "A", "A", "B" }, 160)]
    [InlineData(new[] { "A", "A", "A", "B", "B" }, 175)]
    [InlineData(new[] { "A", "A", "A", "B", "B", "D" }, 190)]
    [InlineData(new[] { "D", "A", "B", "A", "B", "A" }, 190)]
    public void GetTotalPrice_VariousBaskets_ReturnsExpectedTotal(string[] items, int expectedTotal)
    {
        var checkout = CreateCheckout();
        foreach (var item in items)
        {
            checkout.Scan(item);
        }
        var total = checkout.GetTotalPrice();
        Assert.Equal(expectedTotal, total);
    }
}
