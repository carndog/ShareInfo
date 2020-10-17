using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IGetPeriodPriceBySymbolQuery
    {
        Task<IEnumerable<PeriodPrice>> GetAsync(string symbol);
    }
}