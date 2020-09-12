using System.Threading.Tasks;
using System.Transactions;
using DTO;
using DTO.Exceptions;
using Storage;

namespace Services
{
    public class EtoroTransactionService : IEtoroTransactionService
    {
        private readonly IEtoroTransactionRepository _etoroTransactionRepository;

        public EtoroTransactionService(IEtoroTransactionRepository etoroTransactionRepository)
        {
            _etoroTransactionRepository = etoroTransactionRepository;
        }

        public async Task<int> AddAsync(EtoroTransaction etoroTransaction)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int id = await _etoroTransactionRepository.AddAsync(etoroTransaction).ConfigureAwait(false);

                scope.Complete();

                return id;
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