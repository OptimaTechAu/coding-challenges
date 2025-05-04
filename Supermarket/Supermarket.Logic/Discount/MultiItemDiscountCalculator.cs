using Supermarket.Logic.Data;

namespace Supermarket.Logic.Discount
{
    public class MultiItemDiscountCalculator : IDiscountCalculator
    {
        private readonly MultiItemDiscount _multiItemDiscount;

        public MultiItemDiscountCalculator(MultiItemDiscount multiItemDiscount)
        {
            _multiItemDiscount = multiItemDiscount;
        }

        public int ComputeDiscountedPrice(int cartQuantity, int unitPrice)
        {
            if (_multiItemDiscount.ForQuantity == 0)
                return cartQuantity * unitPrice;

            int discountMultiples = cartQuantity / _multiItemDiscount.ForQuantity;

            int remainderQuantity = cartQuantity % _multiItemDiscount.ForQuantity;

            return (discountMultiples * _multiItemDiscount.SpecialPrice) + (remainderQuantity * unitPrice);
        }
    }
}
