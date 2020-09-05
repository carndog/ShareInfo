using NodaTime;

namespace DTO
{
    public class HalifaxTransaction
    {
        public LocalDateTime Date { get; set; }

        public string Type { get; set; }
        
        public string CompanyCode { get; set; }
        
        public string Exchange { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal ExecutedPrice { get; set; }
        
        public decimal NetConsideration { get; set; }
        
        public string Reference { get; set; }
    }
}