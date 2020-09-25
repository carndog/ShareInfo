using DataStorage;
using DataStorage.Queries;
using DTO;

namespace Services
{
    public class EtoroTransactionService : HistoricEntityService<EtoroTransaction>, IEtoroTransactionService
    {
        public EtoroTransactionService(
            IEtoroTransactionRepository etoroTransactionRepository, 
            IDuplicateEtoroTransactionExistsQuery duplicateEtoroTransactionExistsQuery) : 
            base(etoroTransactionRepository, duplicateEtoroTransactionExistsQuery)
        {
        }
    }
}