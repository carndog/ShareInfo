using DataStorage;
using DataStorage.Queries;
using DTO;

namespace Services
{
    public class HalifaxTransactionService : HistoricEntityService<HalifaxTransaction>, IHalifaxTransactionService
    {
        public HalifaxTransactionService(
            IHalifaxTransactionRepository halifaxTransactionRepository, 
            IDuplicateHalifaxTransactionExistsQuery halifaxTransactionExistsQuery) : 
            base(halifaxTransactionRepository, halifaxTransactionExistsQuery)
        {
        }
    }
}