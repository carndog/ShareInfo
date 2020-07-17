using System;
using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IPricesService
    {
        Task<int> Add(AssetPrice assetPrice);
        
        Task<AssetPrice> Get(int id);
    }
}