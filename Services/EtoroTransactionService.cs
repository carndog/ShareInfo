using DataStorage;
using DataStorage.Queries;
using DTO;

namespace Services
{
    public class EtoroTransactionService : HistoricEntityService<EtoroTransaction>, IEtoroTransactionService
    {
        public EtoroTransactionService(
            IEtoroTransactionRepository etoroTransactionRepository, 
            IEtoroTransactionExistsQuery etoroTransactionExistsQuery) : 
            base(etoroTransactionRepository, etoroTransactionExistsQuery)
        {
        }
    }
}