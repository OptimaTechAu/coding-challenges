using Supermarket.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Tests
{
    public class TestFixture
    {
        public IPricingService pricingService = new PricingService();
    }
}
