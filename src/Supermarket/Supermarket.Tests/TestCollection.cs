using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Supermarket.Tests
{
    [CollectionDefinition("Supermarket Collection")]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
    }
}
