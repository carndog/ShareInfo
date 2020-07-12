using System.Threading.Tasks;
using System.Web.Http;
using DTO;

namespace Info.Controllers.Prices
{
    public class PricesController : ApiController
    {
        private readonly IPricesService _pricesService;

        public PricesController(IPricesService pricesService)
        {
            _pricesService = pricesService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody]AssetPrice price)
        {
            await _pricesService.Add(price);

            return CreatedAtRoute(nameof(AssetPrice), new {id = 1}, price);
        }
    }
}