using DTO;
using DTO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Services;

namespace SharePriceListCore.Controllers.Prices
{
    [ApiController]
    [Route("[controller]")]
    public class PriceStreamController : ControllerBase
    {
        private readonly IPriceStreamService _priceStreamService;

        public PriceStreamController(IPriceStreamService priceStreamService)
        {
            _priceStreamService = priceStreamService;
        }

        [HttpPost]
        public async Task<ActionResult<PriceStream>> PostAsync([FromBody] PriceStream? priceStream)
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PriceStream>> Get(int id)
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

        [HttpGet("symbol/{symbol}/latest")]
        public async Task<ActionResult<LatestDateTimePriceStream>> Get(string symbol)
        {
            LocalDateTime? latestDate = await _priceStreamService.GetLatestAsync(symbol);
            return Ok(new LatestDateTimePriceStream
            {
                Date = latestDate
            });
        }
        
        [HttpGet("symbol/{symbol}")]
        public async Task<ActionResult<PriceStreamCollection>> GetAll(string symbol)
        {
            PriceStreamCollection periodPrices = await _priceStreamService.GetAsync(symbol);
            return Ok(periodPrices);
        }
    }
}