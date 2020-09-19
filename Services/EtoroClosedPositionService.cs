using DataStorage;
using DataStorage.Queries;
using DTO;

namespace Services
{
    public class EtoroClosedPositionService : HistoricEntityService<EtoroClosedPosition>, IEtoroClosedPositionService
    {
        public EtoroClosedPositionService(
            IEtoroClosedPositionRepository etoroClosedPositionRepository, 
            IDuplicateEtoroClosedPositionExistsQuery duplicateEtoroClosedPositionExistsQuery) : 
            base(etoroClosedPositionRepository, duplicateEtoroClosedPositionExistsQuery)
        {
        }
    }
}