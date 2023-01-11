namespace Supermarket.Shared.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartService _cartService;
        private readonly IPricingService _pricingService;

        public CheckoutService(ICartService cartService, IPricingService pricingService)
        {
            _cartService = cartService;
            _pricingService = pricingService;
        }

        public void Scan(string item)
        {
            _cartService.Add(item);
        }

        public uint GetTotalPrice()
        {
            var cart = _cartService.GetAllItems();
            return _pricingService.AddAllItemsInCart(cart);
        }

        public void Reset()
        {
            _cartService.Reset();
        }
    }
}