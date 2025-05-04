namespace Supermarket.Logic.Data
{
    public interface IDataRepository
    {
        public Product LookupProduct(string id);
        public MultiItemDiscount GetMutiItemDiscount(string id);
    }
}
