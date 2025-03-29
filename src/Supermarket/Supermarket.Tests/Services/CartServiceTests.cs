using Supermarket.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Tests.Services
{
    public class CartServiceTests
    {
        [Fact]
        public void AddingItems()
        {
            var cartService = new CartService();
            Assert.Empty(cartService.GetAllItems());

            cartService.Add("A");
            Assert.True(cartService.GetAllItems().ContainsKey("A"));
            Assert.True(cartService.GetAllItems().TryGetValue("A", out uint count_A));
            Assert.Equal(1, (double) count_A);
        }
    }
}
