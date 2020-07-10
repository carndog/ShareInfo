using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ShareInfo;

namespace SharesTests
{
    [TestFixture]
    public class SymbolProviderTests
    {
        [Test]
        public void GetSymbols()
        {
            IEnumerable<string> symbols = new SymbolProvider().GetSymbols();

            symbols.Should().NotBeEmpty();
        }
    }
}
