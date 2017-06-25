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
    public class Ftse100ShareFeedExtractorTests
    {
        private static IEnumerable<ShareExtract> _extracts;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            string data = await SharePriceQuery.GetFtse100Data("LLOY");

            IEnumerable<string> symbols = new [] {"LLOY"};

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            _extracts = await director.GetExtracts();
        }

        [TestMethod]
        public void ExtractDataWithoutError()
        {
            _extracts.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ExtractChangeProperty()
        {
            _extracts.First().Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractChangePercentageProperty()
        {
            _extracts.First().Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractChangePercentagePropertyAsRoundTo2Decimals()
        {
            decimal? changePercentage = _extracts.First().ChangePercentage;
            if(changePercentage == null) Assert.Fail("Change percentage cannot be null");
            changePercentage.Should().BeApproximately(changePercentage.Value, 2);
        }

        [TestMethod]
        public void ExtractNameProperty()
        {
            _extracts.First().Name.Should().Be("LLOYDS BANKING GRP");
        }

        [TestMethod]
        public void ExtractPriceProperty()
        {
            _extracts.First().Price.Should().NotBeNull();
        }

        [TestMethod]
        public void ExtractShareIndexProperty()
        {
            _extracts.First().ShareIndex.Should().Be("FTSE100");
        }

        [TestMethod]
        public void ExtractSymbolProperty()
        {
            _extracts.First().Symbol.Should().Be("LLOY");
        }
    }
}
