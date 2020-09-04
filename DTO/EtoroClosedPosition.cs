using NodaTime;

namespace DTO
{
    public class EtoroClosedPosition
    {
        public ulong PositionId { get; set; }
        
        public string Action { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal Units { get; set; }
        
        public decimal OpenRate { get; set; }
        
        public decimal CloseRate { get; set; }
        
        public decimal Spread { get; set; }
        
        public decimal Profit { get; set; }
        
        public LocalDateTime OpenDate { get; set; }
        
        public LocalDateTime ClosedDate { get; set; }
        
        public decimal TakeProfitRate { get; set; }
        
        public decimal StopLossRate { get; set; }
        
        public decimal RollOverFees { get; set; }
    }
}