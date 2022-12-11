namespace Renu.Supermarket.Kata
{
    public struct Sku
    {
        public Sku(string id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public string Id { get; }
        public decimal Price { get; }

        public static implicit operator string(Sku sku)
        {
            ArgumentNullException.ThrowIfNull(sku);
            return sku.Id.ToString();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
