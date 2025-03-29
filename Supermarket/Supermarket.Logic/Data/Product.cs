namespace Supermarket.Logic.Data
{
    public record Product
    {
        public string Sku { get; set; }
        public int UnitPrice { get; set; }

        public Product(string sku, int unitPrice)
        {
            Sku = sku;
            UnitPrice = unitPrice;
        }
    }
}