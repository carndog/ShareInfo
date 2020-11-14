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
    public class PriceStreamService : IPriceStreamService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IPriceStreamRepository _priceStreamRepository;

        private readonly IDuplicatePriceStreamExistsQuery _duplicatePriceStreamExistsQuery;

        private readonly IIsMarketHoursFactory _marketHoursFactory;
        
        private readonly IGetPriceStreamBySymbolQuery _getPriceStreamBySymbolQuery;

        public PriceStreamService(
            IPriceStreamRepository priceStreamRepository, 
            IDuplicatePriceStreamExistsQuery duplicatePriceStreamExistsQuery, 
            IIsMarketHoursFactory marketHoursFactory, 
            IGetPriceStreamBySymbolQuery getPriceStreamBySymbolQuery)
        {
            _priceStreamRepository = priceStreamRepository;
            _duplicatePriceStreamExistsQuery = duplicatePriceStreamExistsQuery;
            _marketHoursFactory = marketHoursFactory;
            _getPriceStreamBySymbolQuery = getPriceStreamBySymbolQuery;
        }

        public async Task<int> AddAsync(PriceStream priceStream)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                priceStream.PriceStreamId = Guid.NewGuid();
                
                bool exists = await _duplicatePriceStreamExistsQuery.GetAsync(priceStream);

                IIsMarketHours isMarketHours = _marketHoursFactory.Create(priceStream.Exchange);

                LocalDateTime localDateTime = LocalDateTime.FromDateTime(priceStream.CurrentDateTime);
                
                bool marketHours = isMarketHours == null || isMarketHours.Get(localDateTime);

                if (!marketHours)
                {
                    throw new OutsideMarketHoursException();
                }
                
                if (!exists)
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

            throw new PriceStreamNotFoundException();
        }

        public async Task<LocalDateTime?> GetLatestAsync(string symbol)
        {
            LocalDateTime? latest = await _priceStreamRepository.GetLatestAsync(symbol);
            return latest;
        }
        
        public async Task<PriceStreamCollection> GetAsync(string symbol)
        {
            return await _getPriceStreamBySymbolQuery.GetAsync(symbol);
        }
    }
}