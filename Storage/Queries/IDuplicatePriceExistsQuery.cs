using System.Threading.Tasks;
using DTO;

namespace Storage.Queries
{
    public interface IDuplicatePriceExistsQuery
    {
        Task<bool> GetAsync(AssetPrice price);
    }
}