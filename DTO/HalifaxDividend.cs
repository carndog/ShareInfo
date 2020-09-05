using NodaTime;

namespace DTO
{
    public class HalifaxDividend
    {
        public LocalDateTime IssueDate { get; set; }
        
        public LocalDateTime ExDividendDate { get; set; }
        
        public string Stock { get; set; }
        
        public int SharesHeld { get; set; }
        
        public decimal Amount { get; set; }
        
        public string HandlingOption { get; set; }
    }
}