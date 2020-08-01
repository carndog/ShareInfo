using System;
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

        private readonly IProgressRepository _progressRepository;

        public PricesService(
            IPriceRepository priceRepository, 
            IDuplicatePriceExistsQuery duplicatePriceExistsQuery, 
            IProgressRepository progressRepository)
        {
            _priceRepository = priceRepository;
            _duplicatePriceExistsQuery = duplicatePriceExistsQuery;
            _progressRepository = progressRepository;
        }

        public async Task<int> AddAsync(AssetPrice assetPrice)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                assetPrice.AssetId = Guid.NewGuid();
                
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