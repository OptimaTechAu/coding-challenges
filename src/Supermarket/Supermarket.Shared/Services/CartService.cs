namespace Supermarket.Shared.Services
{
    public class CartService : ICartService
    {
        private IDictionary<string, uint> Counts = new Dictionary<string, uint>();

        public void Add(string sku)
        {
            if (Counts.ContainsKey(sku))
            {
                Counts[sku]++;
            }
            else
            {
                Counts[sku] = 1;
            }
        }

        public IDictionary<string, uint> GetAllItems()
        {
            return Counts;
        }

        public void Reset()
        {
            Counts.Clear();
        }
    }
}