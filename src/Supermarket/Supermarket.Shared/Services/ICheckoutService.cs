using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Shared.Services
{
    public interface ICheckoutService
    {
        void Scan(string item);
        uint GetTotalPrice();
        void Reset();
    }
}
