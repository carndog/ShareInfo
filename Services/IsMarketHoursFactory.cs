namespace Services
{
    public class IsMarketHoursFactory : IIsMarketHoursFactory
    {
        public IIsMarketHours Create(string exchange)
        {
            if (exchange == "London")
            {
                return new IsLseMarketHours();
            }

            return null;
        }
    }
}