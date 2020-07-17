using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using Services;

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
            int id = await _pricesService.Add(price);

            return CreatedAtRoute("DefaultApi", new {id}, price);
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]int id)
        {
            AssetPrice assetPrice = await _pricesService.Get(id);

            return Ok(assetPrice);
        }
    }
}