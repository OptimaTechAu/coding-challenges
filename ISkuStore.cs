using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Renu.Supermarket.Kata.Test")]

namespace Renu.Supermarket.Kata
{
    public interface ISkuOfferOperations
    {
        void ApplyOffer(Sku sku, ISkuSpecialPrice specialPrice);
        void RemoveOffer(Sku sku, ISkuSpecialPrice specialPrice);
    }

    public interface ISkuOfferRepo
    {
        List<ISkuSpecialPrice> GetOffers(Sku sku);
    }

    public interface ISkuEnquiry
    {
        Sku? Get(string sku);
    }

    public interface ISkuStore : ISkuOfferOperations, ISkuOfferRepo, ISkuEnquiry
    {
        void Add(Sku sku);
    }

    internal class SkuStore : ISkuStore
    {
        readonly Dictionary<Sku, List<ISkuSpecialPrice>> registry = new();

        void ISkuStore.Add(Sku sku)
        {
            ArgumentNullException.ThrowIfNull(sku);

            if (registry.Keys.Any(x => x == sku))
            {
                throw new Exception($"There is already a Sku '${sku}'.");
            }

            registry.Add(sku, new List<ISkuSpecialPrice>());
        }

        void ISkuOfferOperations.ApplyOffer(Sku sku, ISkuSpecialPrice specialPrice)
        {
            ArgumentNullException.ThrowIfNull(sku);
            ArgumentNullException.ThrowIfNull(specialPrice);

            if (!registry.Keys.Any(x => x == sku))
            {
                throw new Exception($"Not found: Sku '${sku}'");
            }

            registry[sku].Add(specialPrice);
        }

        void ISkuOfferOperations.RemoveOffer(Sku sku, ISkuSpecialPrice specialPrice)
        {
            ArgumentNullException.ThrowIfNull(sku);
            ArgumentNullException.ThrowIfNull(specialPrice);

            if (!registry.Keys.Any(x => x == sku))
            {
                throw new Exception($"Not found: Sku '${sku}'");
            }

            var found = registry[sku].FirstOrDefault(x => x.Id == specialPrice.Id);
            if (found == null)
            {
                throw new Exception($"The offer has already been applied to Sku '${sku}'.");
            }

            registry[sku].Remove(specialPrice);
        }

        List<ISkuSpecialPrice> ISkuOfferRepo.GetOffers(Sku sku)
        {
            if (!registry.Keys.Any(x => x == sku))
            {
                throw new Exception($"Not found: Sku '${sku}'");
            }

            return registry[sku];
        }

        Sku? ISkuEnquiry.Get(string sku)
        {
            return registry.Keys.FirstOrDefault(x => x.Id.Equals(sku, StringComparison.OrdinalIgnoreCase));
        }
    }
}
