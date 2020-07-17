using System.Threading.Tasks;
using DTO;
using Storage;

namespace Services
{
    public class ProcessedInformationService : IProcessedInformationService
    {
        private readonly IProgressRepository _progressRepository;
        
        public ProcessedInformationService(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }
        
        public async Task<Progress> Get()
        {
            Progress progress = await _progressRepository.Get();
            return progress;
        }
    }
}