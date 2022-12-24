using DTO;
using DTO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace SharePriceListCore.Controllers.Prices
{
    [ApiController]
    [Route("[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _pricesService;

        public PricesController(IPricesService pricesService)
        {
            _pricesService = pricesService;
        }

        [HttpPost]
        public async Task<ActionResult<AssetPrice>> PostAsync([FromBody]AssetPrice? price)
        {
            if (price == null)
            {
                return BadRequest("Not deserialized");
            }
            
            try
            {
                int id = await _pricesService.AddAsync(price);

                AssetPrice createdPrice = await _pricesService.GetAsync(id);

                return CreatedAtRoute("DefaultApi", new {id}, createdPrice);
            }
            catch (DuplicateExistsException duplicateExistsException)
            {
                return BadRequest($"{duplicateExistsException.Message} Duplicate exists");
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AssetPrice>> Get(int id)
        {
            try
            {
                AssetPrice assetPrice = await _pricesService.GetAsync(id);
                return Ok(assetPrice);
            }
            catch (PriceNotFoundException)
            {
                return NotFound();
            }
        }
    }
}