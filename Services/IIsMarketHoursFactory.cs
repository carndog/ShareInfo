namespace Services
{
    public interface IIsMarketHoursFactory
    {
        IIsMarketHours Create(string exchange);
    }
}