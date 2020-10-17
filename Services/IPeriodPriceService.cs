using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace Services
{
    public interface IPeriodPriceService
    {
        Task<int> AddAsync(PeriodPrice periodPrice);
        
        Task<PeriodPrice> GetAsync(int id);

        Task<LocalDate?> GetLatestAsync(string symbol);
        
        Task<IEnumerable<PeriodPrice>> GetAsync(string symbol);
    }
}