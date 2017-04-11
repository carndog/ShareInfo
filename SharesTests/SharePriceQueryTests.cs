using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shares;

namespace SharesTests
{
    [TestClass]
    public class SharePriceQueryTests
    {
        [TestMethod]
        public async void TestSharesQuery()
        {
            SharePriceQuery query = new SharePriceQuery();

            string price = await query.GetPrice("LLOY.L");

            price.Should()
                .NotBeNullOrWhiteSpace("there must be a web response")
                .And.Contain("LLOY", "the correct share price should be returned");  
        }
    }
}
