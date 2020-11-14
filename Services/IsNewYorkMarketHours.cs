using NodaTime;

namespace Services
{
    public class IsNewYorkMarketHours : IIsMarketHours
    {
        public bool Get(LocalDateTime dateTime)
        {
            //TODO: bank holidays

            return !(dateTime.DayOfWeek == IsoDayOfWeek.Saturday ||
                   dateTime.DayOfWeek == IsoDayOfWeek.Sunday ||
                   dateTime.Hour < 13 ||
                   dateTime.Hour > 21 ||
                   (dateTime.Hour == 13 && dateTime.Minute < 30));
        }
    }
}