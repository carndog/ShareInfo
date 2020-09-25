using System.Threading.Tasks;

namespace DataStorage
{
    public interface IRepository<T> where T : class, new()
    {
        Task<int> AddAsync(T entity);

        Task<T> GetAsync(int id);

        Task<bool> ExistsAsync(int id);
        
        Task<int> CountAsync();
    }
}