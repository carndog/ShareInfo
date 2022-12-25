namespace ServicesTests.Utilities;

public class IsLseMarketHoursTests
{
    [TestCase(23, 10, 0)]
    [TestCase(24, 22, 0)]
    [TestCase(22, 7, 59)]
    [TestCase(22, 16, 31)]
    [TestCase(22, 17, 0)]
    [TestCase(22, 18, 0)]
    public void Should_NotFindPrice_WhenOutsideMarketHours(int day, int hour, int minute)
    {
        IsLseMarketHours marketHours = new IsLseMarketHours();
        LocalDateTime localDateTime = new LocalDateTime(2000, 1, day, hour, minute, 0);
            
        bool isMarketHours = marketHours.Get(localDateTime);

        Assert.IsFalse(isMarketHours);
    }
}