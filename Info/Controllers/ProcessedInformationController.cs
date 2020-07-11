using System.Threading.Tasks;
using System.Web.Http;

namespace Info.Controllers
{
    public class ProcessedInformationController : ApiController
    {
        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok(await Task.FromResult(new ProcessedInformation()));
        }

        public async Task<IHttpActionResult> Get(string key)
        {
            return Ok(await Task.FromResult(new ProcessedInformation()));
        }
    }
}