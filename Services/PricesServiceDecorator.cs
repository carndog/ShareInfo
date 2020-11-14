using System.Threading.Tasks;
using DTO;
using NodaTime;

namespace Services
{
    public class PricesServiceDecorator : IPricesService
    {
        private readonly IPricesService _service;

        public PricesServiceDecorator(IPricesService service)
        {
            _service = service;
        }
        
        public async Task<int> AddAsync(AssetPrice price)
        {
            price.CurrentDateTime = price.Date.ToDateTimeUtc();
            price.TimeZone = price.Date.Zone.ToString();

            return await _service.AddAsync(price);
        }

        public async Task<AssetPrice> GetAsync(int id)
        {
            AssetPrice priceStream = await _service.GetAsync(id);

            DateTimeZone dateTimeZone = DateTimeZoneProviders.Tzdb[priceStream.TimeZone];

            priceStream.Date = LocalDateTime.FromDateTime(priceStream.CurrentDateTime).InZoneStrictly(dateTimeZone);

            return priceStream;
        }
    }
}