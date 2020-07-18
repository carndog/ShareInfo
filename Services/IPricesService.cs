using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IPricesService
    {
        Task<int> AddAsync(AssetPrice assetPrice);
        
        Task<AssetPrice> GetAsync(int id);
    }
}