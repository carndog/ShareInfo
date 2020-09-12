using System.Threading.Tasks;

namespace Storage.Queries
{
    public interface IEtoroTransactionExistsQuery
    {
        Task<bool> GetAsync();
    }
}