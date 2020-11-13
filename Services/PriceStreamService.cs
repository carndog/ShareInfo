using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;
using log4net;
using NodaTime;

namespace Services
{
    public class PriceStreamService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IPriceStreamRepository _priceStreamRepository;

        private readonly IDuplicatePriceStreamExistsQuery _duplicatePriceStreamExistsQuery;

        private readonly IIsMarketHoursFactory _marketHoursFactory;

        public PriceStreamService(
            IPriceStreamRepository priceStreamRepository, 
            IDuplicatePriceStreamExistsQuery duplicatePriceStreamExistsQuery, 
            IIsMarketHoursFactory marketHoursFactory)
        {
            _priceStreamRepository = priceStreamRepository;
            _duplicatePriceStreamExistsQuery = duplicatePriceStreamExistsQuery;
            _marketHoursFactory = marketHoursFactory;
        }

        public async Task<int> AddAsync(PriceStream priceStream)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                priceStream.PriceStreamId = Guid.NewGuid();
                
                bool exists = await _duplicatePriceStreamExistsQuery.GetAsync(priceStream);

                IIsMarketHours isMarketHours = _marketHoursFactory.Create(priceStream.Exchange);

                if (!exists && (isMarketHours == null || isMarketHours.Get(priceStream.Date)))
                {
                    int id = await _priceStreamRepository.AddAsync(priceStream).ConfigureAwait(false);

                    scope.Complete();
                    
                    return id;
                }

                return -1;
            }
        }

        public async Task<PriceStream> GetAsync(int id)
        {
            if (await _priceStreamRepository.ExistsAsync(id))
            {
                PriceStream priceStream = await _priceStreamRepository.GetAsync(id);
                return priceStream;
            }

            throw new PeriodPriceNotFoundException();
        }

        public async Task<LocalDate?> GetLatestAsync(string symbol)
        {
            LocalDate? latest = await _priceStreamRepository.GetLatestAsync(symbol);
            return latest;
        }
    }
}