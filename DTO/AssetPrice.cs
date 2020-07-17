using System;
using NodaTime;

namespace DTO
{
    public class AssetPrice
    {
        public int Id { get; set; }
        
        public Guid AssetId { get; set; }
        
        public string Symbol { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal OriginalPrice { get; set; }
        
        public string Exchange { get; set; }
        
        public string AssetType { get; set; }
        
        public decimal? Change { get; set; }
        
        public decimal? ChangePercentage { get; set; }
        
        public decimal? Open { get; set; }
        
        public decimal? High { get; set; }
        
        public decimal? Low { get; set; }
        
        public decimal? Volume { get; set; }
        
        public string TradingDay  { get; set; }
        
        public ZonedDateTime Date { get; set; }

        public LocalDateTime CurrentDateTime => Date.LocalDateTime;

        public string TimeZone => Date.Zone.ToString();
    }
}
