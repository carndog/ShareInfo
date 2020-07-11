using DTO;
using Storage;

namespace Info
{
    public class ProcessedInformationService : IProcessedInformationService
    {
        private readonly IProgressRepository _progressRepository;
        
        public ProcessedInformationService(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }
        
        public Progress Get()
        {
            Progress progress = _progressRepository.Get();
            return progress;
        }
    }
}