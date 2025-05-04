using Moq;
using NUnit.Framework;
using Supermarket.Logic;
using Supermarket.Logic.Data;
using Supermarket.Logic.Discount;

namespace Supermarket.Test
{
    public class CheckoutTest
    {
        private Mock<IDataRepository> _mockRepo;
        private ICheckout _checkout;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IDataRepository>();

            var productA = new Product("A", 50);
            var productB = new Product("B", 30);
            var productC = new Product("C", 20);
            var productD = new Product("D", 15);

            _mockRepo.Setup(r => r.LookupProduct("A")).Returns(productA);
            _mockRepo.Setup(r => r.LookupProduct("B")).Returns(productB);
            _mockRepo.Setup(r => r.LookupProduct("C")).Returns(productC);
            _mockRepo.Setup(r => r.LookupProduct("D")).Returns(productD);

            var discountCalculatorFactory = new DiscountCalculatorFactory();

            _checkout = new Checkout(_mockRepo.Object, discountCalculatorFactory);
        }

        [Test]
        public void NoScan()
        {
            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public void UpsupportedProduct_MakesNoDiff()
        {
            _checkout.Scan("Z");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public void ScanOnce()
        {
            _checkout.Scan("A");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(50));
        }

        [Test]
        public void ScanAllExampleProducts()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(115));
        }

        [Test]
        public void ScaKataScenario_WithoutDiscount()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");

            _checkout.Scan("A");
            _checkout.Scan("B");

            _checkout.Scan("A");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(245));
        }

        [Test]
        public void ScaKataScenario_WithDiscount()
        {
            _mockRepo.Setup(r => r.GetMutiItemDiscount("A")).Returns(new MultiItemDiscount (3, 130));
            _mockRepo.Setup(r => r.GetMutiItemDiscount("B")).Returns(new MultiItemDiscount (2, 45));

            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");

            _checkout.Scan("A");
            _checkout.Scan("B");

            _checkout.Scan("A");

            var price = _checkout.GetTotalPrice();

            Assert.That(price, Is.EqualTo(210));
        }
    }
}
