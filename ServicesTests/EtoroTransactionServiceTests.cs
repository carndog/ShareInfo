using Microsoft.Extensions.Configuration;
using Moq;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace ServicesTests;

[TestFixture]
public class EtoroTransactionServiceTests : SetupBase
{
    private EtoroTransactionService _service;
        
    private EtoroTransactionRepository _etoroTransactionRepository;

    [SetUp]
    public void Setup()
    {
        Initialise();

        IConfigurationRoot configuration = TestConfigurationProvider.GetConfigurationRoot();

        Mock<IGetDatabase> databaseMock = new Mock<IGetDatabase>();
        databaseMock.Setup(x => x.GetConnectionString()).Returns(configuration.GetSection("connectionString").Value);

        _etoroTransactionRepository = new EtoroTransactionRepository(databaseMock.Object);
            
        _service = new EtoroTransactionService(_etoroTransactionRepository,
            new DuplicateDuplicateEtoroTransactionExistsQuery(databaseMock.Object));
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

    protected override string TableName { get; set; } = "EtoroTransaction";
}