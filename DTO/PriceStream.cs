using System;
using NodaTime;

namespace DTO
{
    public class PriceStream
    {
        public int Id { get; set; }
        
        public Guid PriceStreamId { get; set; }
        
        public string Exchange { get; set; }
        
        public string Symbol { get; set; }

        public decimal Price { get; set; }
        
        public decimal OriginalPrice { get; set; }

        public LocalDateTime Date  { get; set; }
    }
}
