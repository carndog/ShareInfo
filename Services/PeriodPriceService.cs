using System;
using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;

namespace Services
{
    public class PeriodPriceService : IPeriodPriceService
    {
        private readonly IPeriodPriceRepository _periodPriceRepository;

        private readonly IDuplicatePeriodPriceExistsQuery _duplicatePeriodPriceExistsQuery;

        public PeriodPriceService(
            IPeriodPriceRepository periodPriceRepository, 
            IDuplicatePeriodPriceExistsQuery duplicatePeriodPriceExistsQuery)
        {
            _periodPriceRepository = periodPriceRepository;
            _duplicatePeriodPriceExistsQuery = duplicatePeriodPriceExistsQuery;
        }

        public async Task<int> AddAsync(PeriodPrice periodPrice)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                periodPrice.PeriodPriceId = Guid.NewGuid();
                
                bool exists = await _duplicatePeriodPriceExistsQuery.GetAsync(periodPrice);

                if (!exists)
                {
                    int id = await _periodPriceRepository.AddAsync(periodPrice).ConfigureAwait(false);

                    scope.Complete();
                    
                    return id;
                }

                throw new DuplicateExistsException();
            }
        }

        public async Task<PeriodPrice> GetAsync(int id)
        {
            if (await _periodPriceRepository.ExistsAsync(id))
            {
                PeriodPrice periodPrice = await _periodPriceRepository.GetAsync(id);
                return periodPrice;
            }

            throw new PeriodPriceNotFoundException();
        }
    }
}