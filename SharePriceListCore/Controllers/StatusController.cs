using Microsoft.AspNetCore.Mvc;

namespace SharePriceListCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetStatusAsync()
        {
            return await Task.FromResult("Hello");
        }
    }
}