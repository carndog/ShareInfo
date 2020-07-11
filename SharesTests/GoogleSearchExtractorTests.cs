using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FluentAssertions;
using NUnit.Framework;
using ShareInfo.DataExtraction;

namespace SharesTests
{
    [TestFixture]
    public class GoogleSearchExtractorTests
    {
        private static AssetPrice _extract;

        [SetUp]
        public static async Task Setup()
        {
            IEnumerable<string> symbols = new[] { "TW" };

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            IEnumerable<AssetPrice> extracts = await director.GetExtracts();

            _extract = extracts.First();
        }

        [Test]
        public void ExtractDataWithoutError()
        {
            _extract.Should().NotBe(null);
        }

        [Test]
        public void ExtractChangeProperty()
        {
            _extract.Change.Should().NotBeNull();
        }

        [Test]
        public void ExtractChangePercentageProperty()
        {
            _extract.ChangePercentage.Should().NotBeNull();
        }

        [Test]
        public void ExtractNameProperty()
        {
            _extract.Name.Should().BeEquivalentTo("Taylor Wimpey PLC");
        }

        [Test]
        public void ExtractPriceProperty()
        {
            _extract.Price.Should().NotBe(0);
        }

        [Test]
        public void ExtractShareIndexProperty()
        {
            _extract.Exchange.Should().BeNullOrWhiteSpace();
        }

        [Test]
        public void ExtractSymbolProperty()
        {
            _extract.Symbol.Should().Be("TW");
        }
    }
}
