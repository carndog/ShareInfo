using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareInfo;

namespace SharesTests
{
    [TestClass]
    public class SymbolProviderTests
    {
        [TestMethod]
        public void GetSymbols()
        {
            IEnumerable<string> symbols = SymbolProvider.GetSymbols();

            symbols.Should().NotBeEmpty();
        }
    }
}
