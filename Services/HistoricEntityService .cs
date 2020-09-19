using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO.Exceptions;

namespace Services
{
    public abstract class HistoricEntityService<T> : IHistoricEntityService<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;

        private readonly IDuplicateEntityExistsQuery<T> _duplicateEntityExistsQuery;

        public HistoricEntityService(
            IRepository<T> repository, 
            IDuplicateEntityExistsQuery<T> duplicateEntityExistsQuery)
        {
            _repository = repository;
            _duplicateEntityExistsQuery = duplicateEntityExistsQuery;
        }

        public async Task<int> AddAsync(T entity)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                bool exists = await _duplicateEntityExistsQuery.GetAsync(entity);

                if (!exists)
                {
                    int id = await _repository.AddAsync(entity).ConfigureAwait(false);

                    scope.Complete();

                    return id;
                }
                
                throw new DuplicateExistsException();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            if (await _repository.ExistsAsync(id))
            {
                T entity = await _repository.GetAsync(id);
                return entity;
            }

            throw new EntityNotFoundException();
        }
    }
}