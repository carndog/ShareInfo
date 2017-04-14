using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareInfo;

namespace SharesTests
{
    [TestClass]
    public class ShareFeedExtractorTests
    {
        private static ShareFeedExtractor _extractor;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            SharePriceQuery query = new SharePriceQuery();
            string data = await query.GetPrice("LLOY.L");

            _extractor = ShareFeedExtractor.Create(data);
        }

        [TestMethod]
        public void ExtractDataWithoutError()
        {
            _extractor.Should().NotBe(null);
        }

        [TestMethod]
        public void ExtractChangeProperty()
        {
            _extractor.Change.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractChangePercentageProperty()
        {
            _extractor.ChangePercentage.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractNameProperty()
        {
            _extractor.Name.Should().Be("LLOYDS BANKING GRP");
        }

        [TestMethod]
        public void ExtractPriceProperty()
        {
            _extractor.Price.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractShareIndexProperty()
        {
            _extractor.ShareIndex.Should().Be("FTSE100");
        }

        [TestMethod]
        public void ExtractSymbolProperty()
        {
            _extractor.Symbol.Should().Be("LLOY");
        }
    }
}
