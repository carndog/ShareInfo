using System.Threading.Tasks;
using System.Web.Http;

namespace Info.Controllers
{
    public class StatusController : ApiController
    {
        public async Task<IHttpActionResult> GetStatusAsync()
        {
            return await Task.FromResult(Ok("Hello"));
        }
    }
}