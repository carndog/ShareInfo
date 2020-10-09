using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using DTO.Exceptions;
using Services;

namespace Info.Controllers.Prices
{
    public class PeriodPriceController : ApiController
    {
        private readonly IPeriodPriceService _periodPriceService;

        public PeriodPriceController(IPeriodPriceService periodPriceService)
        {
            _periodPriceService = periodPriceService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody]PeriodPrice periodPrice)
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
        
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]int id)
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
    }
}