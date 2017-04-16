using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareInfo;

namespace SharesTests
{
    [TestClass]
    public class SharePriceQueryTests
    {
        [TestMethod]
        public async Task TestSharesQuery()
        {
            string price = await SharePriceQuery.GetFtse100Data("LLOY.L");

            price.Should()
                .NotBeNullOrWhiteSpace("there must be a web response")
                .And.Contain("LLOY", "the correct share price should be returned");  
        }
    }
}
