namespace Supermarket.Shared.Services
{
    public interface IPricingService
    {
        uint GetSkuTotalPrice(string sku, uint count);

        uint AddAllItemsInCart(IDictionary<string, uint> cart);
    }
}