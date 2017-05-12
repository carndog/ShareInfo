namespace Info.Controllers
{
    public class SharePriceInfo
    {
        public string ShareIndex { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercentage { get; set; }

        public int NumberHeld { get; set; }

        public decimal? Value { get; set; }

        public string DisplayValue { get; set; }
    }
}