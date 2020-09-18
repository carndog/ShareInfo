using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;

namespace Services
{
    public class EtoroClosedPositionService : IEtoroClosedPositionService
    {
        private readonly IEtoroClosedPositionRepository _etoroClosedPositionRepository;

        private readonly IDuplicateEtoroClosedPositionExistsQuery _duplicateEtoroClosedPositionExistsQuery;

        public EtoroClosedPositionService(
            IEtoroClosedPositionRepository etoroClosedPositionRepository, 
            IDuplicateEtoroClosedPositionExistsQuery duplicateEtoroClosedPositionExistsQuery)
        {
            _etoroClosedPositionRepository = etoroClosedPositionRepository;
            _duplicateEtoroClosedPositionExistsQuery = duplicateEtoroClosedPositionExistsQuery;
        }

        public async Task<int> AddAsync(EtoroClosedPosition etoroClosedPosition)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                bool exists = await _duplicateEtoroClosedPositionExistsQuery.GetAsync(etoroClosedPosition);

                if (!exists)
                {
                    int id = await _etoroClosedPositionRepository.AddAsync(etoroClosedPosition).ConfigureAwait(false);

                    scope.Complete();
                    
                    return id;
                }

                throw new DuplicateExistsException();
            }
        }

        public async Task<EtoroClosedPosition> GetAsync(int id)
        {
            if (await _etoroClosedPositionRepository.ExistsAsync(id))
            {
                EtoroClosedPosition etoroClosedPosition = await _etoroClosedPositionRepository.GetAsync(id);
                return etoroClosedPosition;
            }

            throw new EtoroClosedPositionNotFoundException();
        }
    }
}