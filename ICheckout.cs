namespace Renu.Supermarket.Kata
{
    public interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
        void Reset();
    }

    internal class Checkout : ICheckout
    {
        private readonly IDynamicPriceHandler _appraiser;
        private readonly ISkuEnquiry _skuEnquiry;
        private readonly List<Sku> items = new();

        public Checkout(IDynamicPriceHandler appraiser, ISkuEnquiry skuEnquiry)
        {
            _appraiser = appraiser;
            _skuEnquiry = skuEnquiry;
        }

        decimal ICheckout.GetTotalPrice()
        {
            decimal amount = 0;

            foreach (var item in items.GroupBy(x => x))
            {
                amount += _appraiser.GetTotal(item.Key, item.Count());
            }

            return amount;
        }

        void ICheckout.Reset()
        {
            items.Clear();
        }

        void ICheckout.Scan(string item)
        {
            ArgumentNullException.ThrowIfNull(item);
            Sku? sku = _skuEnquiry.Get(item);

            if (sku == null)
            {
                throw new Exception("Invalid SKU");
            }

            items.Add(sku.Value);
        }
    }
}
