using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using DTO.Exceptions;
using NodaTime;
using Services;

namespace Info.Controllers.Prices
{
    [RoutePrefix("pricestream")]
    public class PriceStreamController : ApiController
    {
        private readonly IPriceStreamService _priceStreamService;

        public PriceStreamController(IPriceStreamService priceStreamService)
        {
            _priceStreamService = priceStreamService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody] PriceStream priceStream)
        {
            if (priceStream == null)
            {
                return BadRequest("Not deserialized");
            }

            try
            {
                int id = await _priceStreamService.AddAsync(priceStream);

                PriceStream createdPriceStream = await _priceStreamService.GetAsync(id);

                return CreatedAtRoute("DefaultApi", new {id}, createdPriceStream);
            }
            catch (OutsideMarketHoursException)
            {
                return BadRequest("Outside market hours");
            }
            catch (DuplicateExistsException duplicateExistsException)
            {
                return BadRequest($"{duplicateExistsException.Message} Duplicate exists");
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            try
            {
                PriceStream periodPrice = await _priceStreamService.GetAsync(id);
                return Ok(periodPrice);
            }
            catch (PriceNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("symbol/{symbol}/latest")]
        public async Task<IHttpActionResult> Get([FromUri] string symbol)
        {
            LocalDateTime? latestDate = await _priceStreamService.GetLatestAsync(symbol);
            return Ok(new LatestDateTimePriceStream
            {
                Date = latestDate
            });
        }
        
        [HttpGet]
        [Route("symbol/{symbol}")]
        public async Task<IHttpActionResult> GetAll([FromUri] string symbol)
        {
            PriceStreamCollection periodPrices = await _priceStreamService.GetAsync(symbol);
            return Ok(periodPrices);
        }
    }
}