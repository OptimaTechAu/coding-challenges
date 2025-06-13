namespace Supermarket.Shared.Services
{
    public interface ICartService
    {
        void Add(string sku);

        IDictionary<string, uint> GetAllItems();

        void Reset();
    }
}