using NodaTime;

namespace DTO
{
    public class EtoroTransaction
    {
        public LocalDateTime Date { get; set; }
        
        public decimal AccountBalance { get; set; }
        
        public string Type { get; set; }
        
        public string Details { get; set; }
        
        public long PositionId { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal RealizedEquityChange { get; set; }

        public decimal RealizedEquity { get; set; }
    }
}