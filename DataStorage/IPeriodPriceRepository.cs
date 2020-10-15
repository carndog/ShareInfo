using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace DataStorage
{
    public interface IPeriodPriceRepository : IRepository<PeriodPrice>
    {
        Task<LocalDate?> GetLatestAsync(string symbol);
    }
}