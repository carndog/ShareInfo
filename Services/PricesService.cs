using System.Threading.Tasks;
using DTO;
using Storage;

namespace Services
{
    public class PricesService : IPricesService
    {
        private readonly IPriceRepository _priceRepository;
        
        public PricesService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }
        
        public async Task<int> Add(AssetPrice assetPrice)
        {
            int id = await _priceRepository.Add(assetPrice);
            return id;
        }

        public async Task<AssetPrice> Get(int id)
        {
            AssetPrice assetPrice = await _priceRepository.Get(id);
            return assetPrice;
        }
    }
}