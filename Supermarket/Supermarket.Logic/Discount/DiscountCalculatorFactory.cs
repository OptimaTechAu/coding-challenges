using Supermarket.Logic.Data;

namespace Supermarket.Logic.Discount
{
    public class DiscountCalculatorFactory : IDiscountCalculatorFactory
    {
        public IDiscountCalculator CreateMultiItemDiscountCalculator(MultiItemDiscount multiItemDiscount)
        {
            return new MultiItemDiscountCalculator(multiItemDiscount);
        }
    }
}
