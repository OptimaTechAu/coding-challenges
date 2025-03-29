namespace Supermarket.Shared.Services
{
    public interface ICheckoutService
    {
        void Scan(string item);

        uint GetTotalPrice();

        void Reset();
    }
}