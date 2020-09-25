using System.Threading.Tasks;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;
using NodaTime;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class EtoroTransactionServiceTests
    {
        private EtoroTransactionService _service;
        
        private EtoroTransactionRepository _etoroTransactionRepository;

        [SetUp]
        public void Setup()
        {
            _etoroTransactionRepository = new EtoroTransactionRepository();
            
            _service = new EtoroTransactionService(_etoroTransactionRepository,
                new DuplicateDuplicateEtoroTransactionExistsQuery());
        }

        [Test]
        public async Task Should_LoadPositions_When_RepositoryEmpty()
        {
            int id = await _service.AddAsync(new EtoroTransaction
            {
                Amount = 200,
                Date = new LocalDateTime(2000, 1, 1, 1, 0, 0),
                Details = "Ripple",
                Type = "Type",
                AccountBalance = 2000,
                PositionId = 1,
                RealizedEquity = 200,
                RealizedEquityChange = 2000
            });

            Assert.That(id, Is.EqualTo(1));
            
            int count = await _etoroTransactionRepository.CountAsync();
            Assert.That(count, Is.EqualTo(1));
        }
        
        [Test]
        public async Task Should_Not_LoadPositions_When_RepositoryContainsDuplicate()
        {
            CreateTransactions();

            Assert.ThrowsAsync<DuplicateExistsException>(async () => await _service.AddAsync(new EtoroTransaction
            {
                Amount = 200,
                Date = new LocalDateTime(2000, 1, 1, 1, 0, 0),
                Details = "Ripple",
                Type = "Type",
                AccountBalance = 2000,
                PositionId = 1,
                RealizedEquity = 200,
                RealizedEquityChange = 2000
            }));
            
            int count = await _etoroTransactionRepository.CountAsync();
            Assert.That(count, Is.EqualTo(2));
        }

        private void CreateTransactions()
        {
            _etoroTransactionRepository.AddAsync(new EtoroTransaction
            {
                Amount = 200,
                Date = new LocalDateTime(2000, 1, 1, 1, 0, 0),
                Details = "Ripple",
                Type = "Type",
                AccountBalance = 2000,
                PositionId = 1,
                RealizedEquity = 200,
                RealizedEquityChange = 2000
            });

            _etoroTransactionRepository.AddAsync(new EtoroTransaction
            {
                Amount = 200,
                Date = new LocalDateTime(2000, 1, 1, 1, 0, 0),
                Details = "Ripple",
                Type = "Type 2",
                AccountBalance = 2000,
                PositionId = 1,
                RealizedEquity = 3000,
                RealizedEquityChange = 2000
            });
        }
    }
}