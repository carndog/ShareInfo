using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IEtoroTransactionService
    {
        Task<int> AddAsync(EtoroTransaction etoroTransaction);
        
        Task<EtoroTransaction> GetAsync(int id);
    }
}