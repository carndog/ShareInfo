using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace Services
{
    public class PriceStreamServiceDecorator : IPriceStreamService
    {
        private readonly IPriceStreamService _service;

        public PriceStreamServiceDecorator(IPriceStreamService service)
        {
            _service = service;
        }
        
        public async Task<int> AddAsync(PriceStream priceStream)
        {
            priceStream.CurrentDateTime = priceStream.Date.ToDateTimeUtc();
            priceStream.TimeZone = priceStream.Date.Zone.ToString();

            return await _service.AddAsync(priceStream);
        }

        public async Task<PriceStream> GetAsync(int id)
        {
            PriceStream priceStream = await _service.GetAsync(id);

            ResolveZonedDateTime(priceStream);

            return priceStream;
        }

        public async Task<LocalDateTime?> GetLatestAsync(string symbol)
        {
            return await _service.GetLatestAsync(symbol);
        }

        public async Task<PriceStreamCollection> GetAsync(string symbol)
        {
            PriceStreamCollection priceStreamCollection = await _service.GetAsync(symbol);

            foreach (PriceStream priceStream in priceStreamCollection.PriceStreams)
            {
                ResolveZonedDateTime(priceStream);
            }
            
            return priceStreamCollection;
        }

        private static void ResolveZonedDateTime(PriceStream priceStream)
        {
            DateTimeZone dateTimeZone = DateTimeZoneProviders.Tzdb[priceStream.TimeZone];

            priceStream.Date = LocalDateTime.FromDateTime(priceStream.CurrentDateTime).InZoneStrictly(dateTimeZone);
        }
    }
}