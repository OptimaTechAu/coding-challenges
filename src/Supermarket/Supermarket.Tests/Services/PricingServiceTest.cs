namespace Supermarket.Tests.Services
{
    [Collection("Supermarket Collection")]
    public class PricingServiceTest
    {
        private readonly TestFixture _testFixture;

        public PricingServiceTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public void Sku_A()
        {
            Assert.Equal(0, (double) _testFixture.pricingService.GetSkuTotalPrice("A", 0));
            Assert.Equal(50, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 1));
            Assert.Equal(100, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 2));
            Assert.Equal(130, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 3));
            Assert.Equal(180, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 4));
            Assert.Equal(230, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 5));
            Assert.Equal(260, (double)_testFixture.pricingService.GetSkuTotalPrice("A", 6));
        }

        [Fact]
        public void Sku_B()
        {
            Assert.Equal(0, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 0));
            Assert.Equal(30, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 1));
            Assert.Equal(45, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 2));
            Assert.Equal(75, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 3));
            Assert.Equal(90, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 4));
            Assert.Equal(120, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 5));
            Assert.Equal(135, (double)_testFixture.pricingService.GetSkuTotalPrice("B", 6));
        }

        [Fact]
        public void Sku_C()
        {
            Assert.Equal(0, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 0));
            Assert.Equal(20, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 1));
            Assert.Equal(40, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 2));
            Assert.Equal(60, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 3));
            Assert.Equal(80, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 4));
            Assert.Equal(100, (double)_testFixture.pricingService.GetSkuTotalPrice("C", 5));
        }

        [Fact]
        public void Sku_D()
        {
            Assert.Equal(0, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 0));
            Assert.Equal(15, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 1));
            Assert.Equal(30, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 2));
            Assert.Equal(45, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 3));
            Assert.Equal(60, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 4));
            Assert.Equal(75, (double)_testFixture.pricingService.GetSkuTotalPrice("D", 5));
        }
    }
}