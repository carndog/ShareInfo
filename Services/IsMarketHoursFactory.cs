namespace Services
{
    public class IsMarketHoursFactory : IIsMarketHoursFactory
    {
        public IIsMarketHours Create(string exchange)
        {
            switch (exchange)
            {
                case "London":
                    return new IsLseMarketHours();
                case "NewYork":
                    return new IsNewYorkMarketHours();
                default:
                    return null;
            }
        }
    }
}