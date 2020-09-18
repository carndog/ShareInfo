using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;

namespace Services
{
    public class EtoroTransactionService : IEtoroTransactionService
    {
        private readonly IEtoroTransactionRepository _etoroTransactionRepository;

        private readonly IEtoroTransactionExistsQuery _etoroTransactionExistsQuery;

        public EtoroTransactionService(
            IEtoroTransactionRepository etoroTransactionRepository, 
            IEtoroTransactionExistsQuery etoroTransactionExistsQuery)
        {
            _etoroTransactionRepository = etoroTransactionRepository;
            _etoroTransactionExistsQuery = etoroTransactionExistsQuery;
        }

        public async Task<int> AddAsync(EtoroTransaction etoroTransaction)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                bool exists = await _etoroTransactionExistsQuery.GetAsync(etoroTransaction);

                if (!exists)
                {
                    int id = await _etoroTransactionRepository.AddAsync(etoroTransaction).ConfigureAwait(false);

                    scope.Complete();

                    return id;
                }
                
                throw new DuplicateExistsException();
            }
        }

        public async Task<EtoroTransaction> GetAsync(int id)
        {
            if (await _etoroTransactionRepository.ExistsAsync(id))
            {
                EtoroTransaction etoroTransaction = await _etoroTransactionRepository.GetAsync(id);
                return etoroTransaction;
            }

            throw new EtoroTransactionNotFoundException();
        }
    }
}