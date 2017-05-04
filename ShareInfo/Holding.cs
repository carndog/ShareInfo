using NodaTime;

namespace ShareInfo
{
    public class Holding
    {
        public LocalDate PurchaseDate { get; set; }

        public LocalDate? SoldDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal? SoldPrice { get; set; }

        public int NumberPurchased { get; set; }

        public int? NumberSold { get; set; }

        public decimal BookCost { get; set; }

        public string Symbol { get; set; }
    }
}
