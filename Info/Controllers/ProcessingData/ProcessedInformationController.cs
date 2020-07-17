using System.Threading.Tasks;
using System.Web.Http;
using DTO;
using Services;

namespace Info.Controllers.ProcessingData
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
            Progress progress = await _processedInformationService.Get();

            return Ok(progress);
        }
    }
}