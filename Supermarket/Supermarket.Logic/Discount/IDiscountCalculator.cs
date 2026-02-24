namespace Supermarket.Logic.Discount
{
    public interface IDiscountCalculator
    {
        int ComputeDiscountedPrice(int cartQuantity, int unitPrice);
    }
}
