using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IEtoroTransactionRepository
    {
        Task<int> AddAsync(EtoroTransaction position);

        Task<EtoroTransaction> GetAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}