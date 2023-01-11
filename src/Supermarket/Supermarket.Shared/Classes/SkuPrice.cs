namespace Supermarket.Shared
{
    public class SkuPrice
    {
        public string SKU { get; set; }
        public uint SinglePrice { get; set; }
        public SpecialPrice? SpecialPrice { get; set; }

        public SkuPrice(string sku, uint singlePrice)
        {
            SKU = sku;
            SinglePrice = singlePrice;
        }

        public SkuPrice(string sku, uint singlePrice, SpecialPrice specialPrice)
        : this(sku, singlePrice)
        {
            SpecialPrice = specialPrice;
        }
    }
}