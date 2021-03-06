﻿using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using DTO.Exceptions;
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
        
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]int id)
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