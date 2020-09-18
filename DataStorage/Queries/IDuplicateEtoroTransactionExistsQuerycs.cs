using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IEtoroTransactionExistsQuery
    {
        Task<bool> GetAsync(EtoroTransaction transaction);
    }
}