using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ShareInfo;

namespace SharesTests
{
    [TestFixture]
    public class SharePriceQueryTests
    {
        [Test]
        public async Task TestSharesQuery()
        {
            string price = await SharePriceQuery.GetFtse100Data("LLOY.L");

            price.Should()
                .NotBeNullOrWhiteSpace("there must be a web response")
                .And.Contain("LLOY", "the correct share price should be returned");  
        }
    }
}
