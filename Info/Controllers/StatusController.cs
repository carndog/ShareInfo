using System.Threading.Tasks;
using System.Web.Http;
using DTO;

namespace Info.Controllers
{
    public class StatusController : ApiController
    {
        public async Task<IHttpActionResult> GetStatusAsync()
        {
            return await Task.FromResult(Ok(new Status
            {
                Hello = "Hello"
            }));
        }
    }
}