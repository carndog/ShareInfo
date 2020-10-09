using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IPeriodPriceService
    {
        Task<int> AddAsync(PeriodPrice periodPrice);
        
        Task<PeriodPrice> GetAsync(int id);
    }
}