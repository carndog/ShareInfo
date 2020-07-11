using System.Threading.Tasks;
using System.Web.Http;

namespace Info.Controllers
{
    public class ProcessedInformationController : ApiController
    {
        public async Task<IHttpActionResult> GetAllAsync()
        {
            return Ok(await Task.FromResult(new ProcessedInformation
            {
                Total = new Total
                {
                    Value = 2330m
                }
            }));
        }

        public async Task<IHttpActionResult> Get(string key)
        {
            return Ok(await Task.FromResult(new ProcessedInformation()));
        }
    }
}