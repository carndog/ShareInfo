using System.Threading.Tasks;
using System.Web.Http;
using DTO;

namespace Info.Controllers
{
    public class ProcessedInformationController : ApiController
    {
        private readonly IProcessedInformationService _processedInformationService;

        public ProcessedInformationController(IProcessedInformationService processedInformationService)
        {
            _processedInformationService = processedInformationService;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            Progress progress = _processedInformationService.Get();

            return Ok(await Task.FromResult(progress));
        }
    }
}