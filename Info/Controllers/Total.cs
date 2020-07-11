namespace Info.Controllers
{
    public class Total
    {
        public decimal? Value { get; set; }

        public string DisplayValue => string.Format($"{Value ?? 0:C}");
    }
}