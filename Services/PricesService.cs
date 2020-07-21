using System.Threading.Tasks;
using System.Transactions;
using DTO;
using DTO.Exceptions;
using Storage;
using Storage.Queries;

namespace Services
{
    public class PricesService : IPricesService
    {
        private readonly IPriceRepository _priceRepository;

        private readonly IDuplicatePriceExistsQuery _duplicatePriceExistsQuery;

        public PricesService(IPriceRepository priceRepository, IDuplicatePriceExistsQuery duplicatePriceExistsQuery)
        {
            _priceRepository = priceRepository;
            _duplicatePriceExistsQuery = duplicatePriceExistsQuery;
        }

        public async Task<int> AddAsync(AssetPrice assetPrice)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                bool exists = await _duplicatePriceExistsQuery.GetAsync(assetPrice);

                if (!exists)
                {

                    int id = await _priceRepository.AddAsync(assetPrice).ConfigureAwait(false);

                    scope.Complete();
                    
                    return id;
                }

                throw new DuplicateExistsException();
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