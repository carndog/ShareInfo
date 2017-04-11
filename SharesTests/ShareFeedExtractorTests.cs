using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareInfo;

namespace SharesTests
{
    [TestClass]
    public class ShareFeedExtractorTests
    {
        [TestMethod]
        public async Task ExtractData()
        {
            SharePriceQuery query = new SharePriceQuery();
            string data = await query.GetPrice("LLOY.L");

            ShareFeedExtractor extractor = ShareFeedExtractor.Create(data);

            extractor.Should().NotBe(null);
        }
    }
}
