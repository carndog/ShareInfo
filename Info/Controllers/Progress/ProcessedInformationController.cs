using System.Threading.Tasks;
using System.Web.Http;

namespace Info.Controllers.Progress
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
            DTO.Progress progress = await _processedInformationService.Get();

            return Ok(progress);
        }
    }
}