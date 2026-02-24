using NUnit.Framework;
using Supermarket.Logic.Data;
using Supermarket.Logic.Discount;

namespace Supermarket.Test.Discount
{
    [TestFixture]
    public class MultiItemDiscountCalculatorTest
    {
        [TestCase(0,   0, 1, 10, ExpectedResult = 10)]
        [TestCase(3, 100, 1,  0, ExpectedResult = 0)]
        [TestCase(3, 100, 1, 40, ExpectedResult = 40)]
        [TestCase(3, 100, 2, 40, ExpectedResult = 80)]
        [TestCase(3, 100, 3, 40, ExpectedResult = 100)]
        [TestCase(3, 100, 4, 40, ExpectedResult = 140)]
        [TestCase(3, 100, 5, 40, ExpectedResult = 180)]
        [TestCase(3, 100, 6, 40, ExpectedResult = 200)]
        [TestCase(3, 100, 7, 40, ExpectedResult = 240)]
        public int ComputeDiscountedPrice(int discountForQuantity, int discountedPrice, int cartQuantity, int unitPrice)
        {
            var multiItemDiscount = new MultiItemDiscount(discountForQuantity, discountedPrice);
            var calculator = new MultiItemDiscountCalculator(multiItemDiscount);

            return calculator.ComputeDiscountedPrice(cartQuantity, unitPrice);
        }
    }
}
