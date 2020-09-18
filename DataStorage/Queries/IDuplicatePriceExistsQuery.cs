using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IDuplicatePriceExistsQuery
    {
        Task<bool> GetAsync(AssetPrice price);
    }
}