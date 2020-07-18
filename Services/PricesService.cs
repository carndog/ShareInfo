using System.Threading.Tasks;
using System.Transactions;
using DTO;
using DTO.Exceptions;
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

        public async Task<int> AddAsync(AssetPrice assetPrice)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int id = await _priceRepository.AddAsync(assetPrice).ConfigureAwait(false);

                scope.Complete();

                return id;
            }
        }

        public async Task<AssetPrice> GetAsync(int id)
        {
            if (await _priceRepository.ExistsAsync(id))
            {
                AssetPrice assetPrice = await _priceRepository.GetAsync(id);
                return assetPrice;
            }

            throw new PriceNotFoundException();
        }
    }
}