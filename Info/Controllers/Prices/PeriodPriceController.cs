using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.DynamicProxy.Generators.Emitters;
using DTO;
using DTO.Exceptions;
using NodaTime;
using Services;

namespace Info.Controllers.Prices
{
    [RoutePrefix("periodprice")]
    public class PeriodPriceController : ApiController
    {
        private readonly IPeriodPriceService _periodPriceService;

        public PeriodPriceController(IPeriodPriceService periodPriceService)
        {
            _periodPriceService = periodPriceService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody] PeriodPrice periodPrice)
        {
            if (periodPrice == null)
            {
                return BadRequest("Not deserialized");
            }

            try
            {
                int id = await _periodPriceService.AddAsync(periodPrice);

                PeriodPrice createdPrice = await _periodPriceService.GetAsync(id);

                return CreatedAtRoute("DefaultApi", new {id}, createdPrice);
            }
            catch (DuplicateExistsException duplicateExistsException)
            {
                return BadRequest($"{duplicateExistsException.Message} Duplicate exists");
            }
        }
        
        [HttpPost]
        [Route("load")]
        public async Task<IHttpActionResult> LoadAsync([FromBody] PeriodPriceCollection periodPrices)
        {
            if (periodPrices == null)
            {
                return BadRequest("Not deserialized");
            }

            try
            {
                IEnumerable<PeriodPrice> prices = await _periodPriceService.AddListAsync(periodPrices.PeriodPrices);

                return Created("DefaultApi", new PeriodPriceCollection
                {
                    PeriodPrices = prices
                });
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
                PeriodPrice periodPrice = await _periodPriceService.GetAsync(id);
                return Ok(periodPrice);
            }
            catch (PriceNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{symbol}/latest")]
        public async Task<IHttpActionResult> Get([FromUri] string symbol)
        {
            LocalDate? latestDate = await _periodPriceService.GetLatestAsync(symbol);
            return Ok(new LatestPeriodPrice
            {
                Date = latestDate
            });
        }
        
        [HttpGet]
        [Route("{symbol}")]
        public async Task<IHttpActionResult> GetAll([FromUri] string symbol)
        {
            PeriodPriceCollection periodPrices = await _periodPriceService.GetAsync(symbol);
            return Ok(periodPrices);
        }
    }
}