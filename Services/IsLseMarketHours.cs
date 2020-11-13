using NodaTime;

namespace Services
{
    public class IsLseMarketHours : IIsMarketHours
    {
        public bool Get(LocalDateTime dateTime)
        {
            //TODO: bank holidays

            return !(dateTime.DayOfWeek == IsoDayOfWeek.Saturday ||
                   dateTime.DayOfWeek == IsoDayOfWeek.Sunday ||
                   dateTime.Hour < 8 ||
                   dateTime.Hour > 16 ||
                   (dateTime.Hour == 16 && dateTime.Minute > 30));
        }
    }
}