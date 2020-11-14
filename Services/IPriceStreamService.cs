using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace Services
{
    public interface IPriceStreamService
    {
        Task<int> AddAsync(PriceStream priceStream);
        
        Task<PriceStream> GetAsync(int id);
        
        Task<LocalDate?> GetLatestAsync(string symbol);
        
        Task<PriceStreamCollection> GetAsync(string symbol);
    }
}