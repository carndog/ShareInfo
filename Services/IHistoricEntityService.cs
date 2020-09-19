using System.Threading.Tasks;

namespace Services
{
    public interface IHistoricEntityService<T> where T : class, new()
    {
        Task<int> AddAsync(T entity);
        
        Task<T> GetAsync(int id);
    }
}