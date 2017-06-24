using System.Collections.Generic;

namespace Info.Controllers
{
    public class InformationBoard
    {
        public Total Total { get; set; }

        public IEnumerable<SharePrice> SharePrices { get; set; }
    }
}