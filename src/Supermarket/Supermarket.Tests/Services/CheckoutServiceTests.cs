using Supermarket.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Tests.Services
{
    [Collection("Supermarket Collection")]
    public class CheckoutServiceTests
    {
        private readonly TestFixture _testFixture;

        public CheckoutServiceTests(TestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public void Buy_A()
        {
            var checkoutService = MakeCheckoutService();
            Assert.Equal(0, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("A");
            Assert.Equal(50, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("A");
            Assert.Equal(100, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("A");
            Assert.Equal(130, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("A");
            Assert.Equal(180, (double)checkoutService.GetTotalPrice());
        }

        [Fact]
        public void Buy_B()
        {
            var checkoutService = MakeCheckoutService();
            Assert.Equal(0, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("B");
            Assert.Equal(30, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("B");
            Assert.Equal(45, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("B");
            Assert.Equal(75, (double)checkoutService.GetTotalPrice());
        }

        [Fact]
        public void Buy_A_B_C()
        {
            var checkoutService = MakeCheckoutService();
            Assert.Equal(0, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("A");
            Assert.Equal(50, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("B");
            Assert.Equal(80, (double)checkoutService.GetTotalPrice());
            
            checkoutService.Scan("C");
            Assert.Equal(100, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("A");
            Assert.Equal(150, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("B");
            Assert.Equal(165, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("C");
            Assert.Equal(185, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("A");
            Assert.Equal(215, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("B");
            Assert.Equal(245, (double)checkoutService.GetTotalPrice());

            checkoutService.Scan("C");
            Assert.Equal(265, (double)checkoutService.GetTotalPrice());
        }

        private CheckoutService MakeCheckoutService()
        {
            var cartService = new CartService();
            return new CheckoutService(cartService, _testFixture.pricingService);
        }
    }
}
