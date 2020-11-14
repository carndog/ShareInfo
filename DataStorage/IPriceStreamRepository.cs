using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace DataStorage
{
    public interface IPriceStreamRepository : IRepository<PriceStream>
    {
        Task<LocalDateTime?> GetLatestAsync(string symbol);
    }
}