using System.Threading.Tasks;
using DTO;
using Storage;

namespace Info
{
    public class PricesService : IPricesService
    {
        private readonly IPriceRepository _priceRepository;
        
        public PricesService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }
        
        public async Task Add(AssetPrice assetPrice)
        { 
            await _priceRepository.Add(assetPrice);
        }
    }
}