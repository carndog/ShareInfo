using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IGetPriceStreamBySymbolQuery
    {
        Task<PriceStreamCollection> GetAsync(string symbol);
    }
}