using SupermarketDemo.Item;
using SupermarketDemo.Discount;
using System.Collections.Generic;

namespace SupermarketDemo
{
    public class Checkout : ICheckout
    {
        private readonly IDataRepository _repo;
        private readonly IDiscountCalculatorFactory _discountCalculatorFactory;

        private readonly Dictionary<string, CartItem> _cart = new Dictionary<string, CartItem>();

        internal Dictionary<string, CartItem> Cart => _cart;

        public Checkout(IDataRepository repo, IDiscountCalculatorFactory discountCalculatorFactory)
        {
            _repo = repo;
            _discountCalculatorFactory = discountCalculatorFactory;
        }

        public void Scan(string item)
        {
            var product = _repo.LookupProduct(item);
            if (product != null)
            {
                if (_cart.TryGetValue(product.Sku, value: out var cartItem))
                {
                    // Increment quantity
                    cartItem.Quantity += 1;
                }
                else
                {
                    // Add item to cart
                    _cart.Add(product.Sku, new CartItem(product, 1));
                }
            }
        }

        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach (var cartItem in _cart.Values)
            {
                var multiSpecial = _repo.GetMutiItemDiscount(cartItem.Product.Sku);

                if (multiSpecial != null)
                {
                    var specialCalculator = _discountCalculatorFactory.CreateMultiItemDiscountCalculator(multiSpecial);
                    totalPrice += specialCalculator.ComputeDiscountedPrice(cartItem.Quantity, cartItem.Product.UnitPrice);
                }
                else
                {
                    totalPrice += cartItem.Quantity * cartItem.Product.UnitPrice;
                }
            }

            return totalPrice;
        }
    }
}
