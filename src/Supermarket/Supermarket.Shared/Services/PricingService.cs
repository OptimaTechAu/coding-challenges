using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Shared.Services
{
    public class PricingService : IPricingService
    {
        private Dictionary<string, SkuPrice> _storePrices;

        public PricingService()
        {
            _storePrices = new Dictionary<string, SkuPrice>();
            InititalizePrices();
        }

        // Sample price list
        private void InititalizePrices()
        {
            _storePrices.Add("A", new SkuPrice("A", 50, new SpecialPrice { Quantity = 3, QuantityPrice = 130 }));
            _storePrices.Add("B", new SkuPrice("B", 30, new SpecialPrice { Quantity = 2, QuantityPrice = 45 }));
            _storePrices.Add("C", new SkuPrice("C", 20));
            _storePrices.Add("D", new SkuPrice("D", 15));
        }

        /// <summary>
        /// If there is no Special Price just use the unique price * quantity
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public uint GetSkuTotalPrice(string sku, uint count)
        {
            if (count == 0)
                return 0;

            var skuPrice = GetSkuPrice(sku);
            if (skuPrice is null)
                return 0;

            if (skuPrice.SpecialPrice is null)
            {
                return skuPrice.SinglePrice * count;
            }
            else
            {
                var groups = count / skuPrice.SpecialPrice.Quantity;
                var remainder = count % skuPrice.SpecialPrice.Quantity;

                var total = groups * skuPrice.SpecialPrice.QuantityPrice;
                total += remainder * skuPrice.SinglePrice;
                return total;
            }
        }

        public uint AddAllItemsInCart(IDictionary<string, uint> cart)
        {
            uint total = 0; 

            foreach (KeyValuePair<string, uint> kvp in cart)
            {
                total += GetSkuTotalPrice(kvp.Key, kvp.Value);
            }

            return total;
        }

        private SkuPrice? GetSkuPrice(string sku)
        {
            if (!_storePrices.ContainsKey(sku))
                return null;

            return _storePrices[sku];
        }
    }
}
