using Supermarket.Logic.Data;

namespace Supermarket.Logic.Discount
{
    public interface IDiscountCalculatorFactory
    {
        IDiscountCalculator CreateMultiItemDiscountCalculator(MultiItemDiscount multiItemDiscount);
    }
}
