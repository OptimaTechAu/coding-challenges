namespace Supermarket.Shared
{
    /// <summary>
    /// A 3 for 120 would be Quatity=3 and QuantityPrice=120
    /// </summary>
    public class SpecialPrice
    {
        public uint Quantity { get; set; }
        public uint QuantityPrice { get; set; }
    }
}