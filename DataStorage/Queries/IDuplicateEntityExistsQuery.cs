using System.Threading.Tasks;

namespace DataStorage.Queries
{
    public interface IDuplicateEntityExistsQuery<T> where T : class, new()
    {
        Task<bool> GetAsync(T entity);
    }
}