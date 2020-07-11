using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using FluentAssertions;
using NUnit.Framework;
using ShareInfo;
using ShareInfo.DataExtraction;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SharesTests
{
    [TestFixture]
    public class Ftse100ShareFeedExtractorTests
    {
        private static IEnumerable<AssetPrice> _extracts;

        [SetUp]
        public static async Task Setup()
        {
            string data = await SharePriceQuery.GetFtse100Data("LLOY");

            IEnumerable<string> symbols = new [] {"LLOY"};

            ShareExtractorsDirector director = new ShareExtractorsDirector(symbols);

            _extracts = await director.GetExtracts();
        }

        [Test]
        public void ExtractDataWithoutError()
        {
            _extracts.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ExtractChangeProperty()
        {
            _extracts.First().Should().NotBeNull();
        }

        [Test]
        public void ExtractChangePercentageProperty()
        {
            _extracts.First().Should().NotBeNull();
        }

        [Test]
        public void ExtractChangePercentagePropertyAsRoundTo2Decimals()
        {
            decimal? changePercentage = _extracts.First().ChangePercentage;
            if(changePercentage == null) Assert.Fail("Change percentage cannot be null");
            changePercentage.Should().BeApproximately(changePercentage.Value, 2);
        }

        [Test]
        public void ExtractNameProperty()
        {
            _extracts.First().Name.Should().Be("LLOYDS BANKING GRP");
        }

        [Test]
        public void ExtractPriceProperty()
        {
            _extracts.First().Price.Should().NotBe(0);
        }

        [Test]
        public void ExtractShareIndexProperty()
        {
            _extracts.First().Exchange.Should().Be("FTSE100");
        }

        [Test]
        public void ExtractSymbolProperty()
        {
            _extracts.First().Symbol.Should().Be("LLOY");
        }
    }
}
