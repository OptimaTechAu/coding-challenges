namespace Supermarket.Shared
{
    public class SkuPrice
    {
        public string Sku { get; set; }
        public uint SinglePrice { get; set; }
        public SpecialPrice? SpecialPrice { get; set; }

        public SkuPrice(string sku, uint singlePrice)
        {
            Sku = sku;
            SinglePrice = singlePrice;
        }

        public SkuPrice(string sku, uint singlePrice, SpecialPrice specialPrice)
        : this(sku, singlePrice)
        {
            SpecialPrice = specialPrice;
        }
    }
}