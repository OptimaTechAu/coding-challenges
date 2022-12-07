namespace Renu.Supermarket.Kata
{
    public interface IDynamicPriceHandler
    {
        decimal GetTotal(Sku sku, int count);
    }

    public class DynamicPriceHandler : IDynamicPriceHandler
    {
        private readonly ISkuOfferRepo _skuOfferRepo;

        public DynamicPriceHandler(ISkuOfferRepo skuOfferRepo)
        {
            _skuOfferRepo = skuOfferRepo;
        }

        decimal IDynamicPriceHandler.GetTotal(Sku sku, int itemCount)
        {
            var discounts = _skuOfferRepo.GetOffers(sku)?.OrderByDescending(x => x.Count).ToList();

            if (discounts != null && discounts.Any())
            {
                ISkuSpecialPrice? specialPrice = null;
                decimal price = 0;
                int i = 0;

                int total = discounts.Count();
                int remaingItemCount = itemCount;

                do
                {
                    for (; i <= total; i++)
                    {
                        if (remaingItemCount >= discounts[i].Count)
                        {
                            specialPrice = discounts[i];
                            i++;
                            break;
                        }
                    }

                    price += (remaingItemCount / specialPrice!.Count) * specialPrice.Amount;

                    remaingItemCount = (remaingItemCount % specialPrice!.Count);

                } while (remaingItemCount > 1);

                if (remaingItemCount > 0)
                {
                    price += remaingItemCount * sku.Price;
                }

                return price;
            }

            return sku.Price * itemCount;
        }
    }
}
