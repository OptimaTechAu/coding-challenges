using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Shared.Exceptions
{
    public class SupermarketException : Exception
    {
        public SupermarketException(string message) 
            : base(message) { }
    }
}
