using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareInfo;
using ShareInfo.DataExtraction;

namespace SharesTests
{
    [TestClass]
    public class GoogleSearchExtractorTests
    {
        private static ShareExtract _extract;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            IEnumerable<string> symbols = new[] { "TW" };

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            IEnumerable<ShareExtract> extracts = await director.GetExtracts();

            _extract = extracts.First();
        }

        [TestMethod]
        public void ExtractDataWithoutError()
        {
            _extract.Should().NotBe(null);
        }

        [TestMethod]
        public void ExtractChangeProperty()
        {
            _extract.Change.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractChangePercentageProperty()
        {
            _extract.ChangePercentage.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractNameProperty()
        {
            _extract.Name.Should().BeEquivalentTo("Taylor Wimpey PLC");
        }

        [TestMethod]
        public void ExtractPriceProperty()
        {
            _extract.Price.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractShareIndexProperty()
        {
            _extract.ShareIndex.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ExtractSymbolProperty()
        {
            _extract.Symbol.Should().Be("TW");
        }
    }
}
