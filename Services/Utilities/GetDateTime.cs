using System;
using NodaTime;

namespace Services.Utilities
{
    public class GetDateTime : IGetDateTime
    {
        public LocalDateTime Get()
        {
            return LocalDateTime.FromDateTime(DateTime.Now);
        }
    }
}