using System;
using NodaTime;

namespace DTO
{
    public class PeriodPrice
    {
        public int Id { get; set; }
        
        public Guid PeriodPriceId { get; set; }
        
        public string Symbol { get; set; }
        
        public PeriodType PeriodType { get; set; }
        
        public decimal Open { get; set; }
        
        public decimal High { get; set; }
        
        public decimal Low { get; set; }
        
        public decimal Close { get; set; }
        
        public decimal Volume { get; set; }

        public LocalDate Date  { get; set; }
    }
}
