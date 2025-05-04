namespace Supermarket.Logic.Data
{
    public record MultiItemDiscount
    {
        public int ForQuantity { get; set; }
        public int SpecialPrice { get; set; }

        public MultiItemDiscount(int forQuantity, int specialPrice)
        {
            ForQuantity = forQuantity;
            SpecialPrice = specialPrice;
        }
    }
}
