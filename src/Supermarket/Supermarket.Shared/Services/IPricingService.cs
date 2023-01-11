using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Shared.Services
{
    public interface IPricingService
    {
        uint GetSkuTotalPrice(string sku, uint count);

        uint AddAllItemsInCart(IDictionary<string, uint> cart);
    }
}
