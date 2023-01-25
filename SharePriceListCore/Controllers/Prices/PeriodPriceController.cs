using DTO;
using DTO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Services;

namespace SharePriceListCore.Controllers.Prices
{
    [ApiController]
    [Route("[controller]")]
    public class PeriodPriceController : ControllerBase
    {
        private readonly IPeriodPriceService _periodPriceService;

        public PeriodPriceController(IPeriodPriceService periodPriceService)
        {
            _periodPriceService = periodPriceService;
        }

        [HttpPost]
        public async Task<ActionResult<PeriodPrice>> PostAsync([FromBody] PeriodPrice? periodPrice)
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
        
        [HttpPost("load")]
        public async Task<ActionResult<IEnumerable<PeriodPrice>>> LoadAsync([FromBody] PeriodPriceCollection? periodPrices)
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeriodPrice>> Get(int id)
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

        [HttpGet("{symbol}/latest")]
        public async Task<ActionResult<LocalDate?>> Get(string symbol)
        {
            LocalDate? latestDate = await _periodPriceService.GetLatestAsync(symbol);
            return Ok(new LatestPeriodPrice
            {
                Date = latestDate
            });
        }
        
        [HttpGet("{symbol}")]
        public async Task<ActionResult<PeriodPriceCollection>> GetAll(string symbol)
        {
            PeriodPriceCollection periodPrices = await _periodPriceService.GetAsync(symbol);
            return Ok(periodPrices);
        }
    }
}