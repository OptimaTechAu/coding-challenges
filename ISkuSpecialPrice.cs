namespace Renu.Supermarket.Kata
{
    public interface ISkuSpecialPrice
    {
        int Id { get; }
        int Count { get; }
        decimal Amount { get; }
    }

    internal class SkuSpecialPrice : ISkuSpecialPrice
    {
        public SkuSpecialPrice(int count, decimal amount)
        {
            Count = count;
            Amount = amount;    
            Id = Guid.NewGuid().GetHashCode();
        }

        public int Id { get; private set; }
        public int Count { get; private set; }
        public decimal Amount { get; private set; }
    }
}
