using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IPricesService
    {
        Task<int> AddAsync(AssetPrice price);
        
        Task<AssetPrice> GetAsync(int id);
    }
}