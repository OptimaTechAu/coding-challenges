namespace SupermarketCheckout.Lambda.Models;

public class PricingRule
{

    public required string Sku { get; init; }

    public required int UnitPrice { get; init; }

    public int? SpecialQuantity { get; init; }

    public int? SpecialPrice { get; init; }

    public bool HasSpecialOffer => SpecialQuantity.HasValue && SpecialPrice.HasValue;

    public int CalculateTotalPriceOfQuantityOfItems(int quantity)
    {

        if (!HasSpecialOffer)
        {
            return quantity * UnitPrice;
        }

        // Apply special offer as many times as possible
        var specialQuantity = SpecialQuantity!.Value;
        var specialPrice = SpecialPrice!.Value;

        var numberOfSpecialOffers = quantity / specialQuantity;
        var remainingItems = quantity % specialQuantity;

        var specialOfferTotal = numberOfSpecialOffers * specialPrice;
        var remainingTotal = remainingItems * UnitPrice;

        return specialOfferTotal + remainingTotal;
    }
}
