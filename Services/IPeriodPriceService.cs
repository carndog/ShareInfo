using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace Services
{
    public interface IPeriodPriceService
    {
        Task<int> AddAsync(PeriodPrice periodPrice);
        
        Task<IEnumerable<PeriodPrice>> AddListAsync(IEnumerable<PeriodPrice> periodPrices);
        
        Task<PeriodPrice> GetAsync(int id);

        Task<LocalDate?> GetLatestAsync(string symbol);
        
        Task<PeriodPriceCollection> GetAsync(string symbol);
    }
}