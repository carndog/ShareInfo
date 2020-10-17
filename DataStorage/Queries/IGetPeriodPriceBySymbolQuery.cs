using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IGetPeriodPriceBySymbolQuery
    {
        Task<PeriodPriceCollection> GetAsync(string symbol);
    }
}