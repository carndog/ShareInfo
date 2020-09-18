using System.Threading.Tasks;

namespace DataStorage.Queries
{
    public interface IEtoroTransactionExistsQuery
    {
        Task<bool> GetAsync();
    }
}