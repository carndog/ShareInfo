using Microsoft.AspNetCore.Mvc;

namespace SharePriceListCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        public async Task<string> GetStatusAsync()
        {
            return await Task.FromResult("Hello");
        }
    }
}