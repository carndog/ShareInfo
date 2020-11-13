using System.Threading.Tasks;
using DataStorage;
using DataStorage.Queries;
using DTO;
using NodaTime;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class PriceStreamServiceTests : Setupbase
    {
        private PriceStreamService _service;
        
        private PriceStreamRepository _priceStreamRepository;
        
        private LocalDateTime _date;

        [SetUp]
        public void Setup()
        {
            Initialise();
            
            _priceStreamRepository = new PriceStreamRepository();
            
            _service = new PriceStreamService(
                _priceStreamRepository,
                new DuplicatePriceStreamExistsQuery(),
                new IsMarketHoursFactory());
            
            _date = new LocalDateTime(2020, 1, 1, 10, 3, 0);
        }

        [Test]
        public async Task Should_Insert_When_RepositoryEmpty()
        {
            int id = await _service.AddAsync(new PriceStream
            {
               Date = _date,
               Price = 12.56m,
               OriginalPrice = 12.5623545m,
               Symbol = "HHH",
               Exchange = "London"
            });

            PriceStream price = await _priceStreamRepository.GetAsync(id);
            Assert.That(price.Date, Is.EqualTo(_date));
            Assert.That(price.Price, Is.EqualTo(12.56m));
            Assert.That(price.OriginalPrice, Is.EqualTo(12.5623545m));
            Assert.That(price.Symbol, Is.EqualTo("HHH"));
            Assert.That(price.Exchange, Is.EqualTo("London"));

            int count = await _priceStreamRepository.CountAsync();
            Assert.That(count, Is.EqualTo(1));
        }
        
        [Test]
        public async Task Should_Not_Insert_When_RepositoryContainsDuplicate()
        {
            CreateRecords();

            int duplicate = await _service.AddAsync(new PriceStream
            {
                Date = _date,
                Price = 12.56m,
                OriginalPrice = 12.5623545m,
                Symbol = "HHH",
                Exchange = "London"
            });

            Assert.That(duplicate, Is.EqualTo(-1));
            int count = await _priceStreamRepository.CountAsync();
            Assert.That(count, Is.EqualTo(2));
        }

        private void CreateRecords()
        {
            _priceStreamRepository.AddAsync(new PriceStream
            {
                Date = _date,
                Price = 12.56m,
                OriginalPrice = 12.5623545m,
                Symbol = "HHH",
                Exchange = "London"
            });

            _priceStreamRepository.AddAsync(new PriceStream
            {
                Date = _date,
                Price = 12.56m,
                OriginalPrice = 12.5623545m,
                Symbol = "CCC",
                Exchange = "London"
            });
        }

        public override string TableName { get; protected set; } = "PriceStream";
    }
}