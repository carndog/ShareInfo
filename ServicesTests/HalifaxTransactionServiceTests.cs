using Microsoft.Extensions.Configuration;
using Moq;

namespace ServicesTests;

[TestFixture]
public class HalifaxTransactionServiceTests : SetupBase
{
    private HalifaxTransactionService _service;
        
    private HalifaxTransactionRepository _halifaxTransactionRepository;

    [SetUp]
    public void Setup()
    {
        Initialise();
        
        IConfigurationRoot configuration = TestConfigurationProvider.GetConfigurationRoot();

        Mock<IGetDatabase> databaseMock = new Mock<IGetDatabase>();
        databaseMock.Setup(x => x.GetConnectionString()).Returns(configuration.GetSection("connectionString").Value);
            
        _halifaxTransactionRepository = new HalifaxTransactionRepository(databaseMock.Object);
            
        _service = new HalifaxTransactionService(_halifaxTransactionRepository,
            new DuplicateHalifaxTransactionExistsQuery(databaseMock.Object));
    }

    [Test]
    public async Task Should_LoadTransactions_When_RepositoryEmpty()
    {
        int id = await _service.AddAsync(new HalifaxTransaction
        {
            Date = new LocalDateTime(2000, 1, 1, 13, 0, 0),
            Exchange = "NY",
            Quantity = 1900,
            Reference = "hello",
            Type = "Buy",
            CompanyCode = "AMZ",
            ExecutedPrice = 33.45m,
            NetConsideration = 20.3m
        });

        Assert.That(id, Is.EqualTo(1));
            
        int count = await _halifaxTransactionRepository.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }
        
    [Test]
    public async Task Should_Not_LoadTransactions_When_RepositoryContainsDuplicate()
    {
        CreateTransactions();

        Assert.ThrowsAsync<DuplicateExistsException>(async () => await _service.AddAsync(new HalifaxTransaction
        {
            Date = new LocalDateTime(2000, 1, 1, 13, 0, 0),
            Exchange = "NY",
            Quantity = 1900,
            Reference = "hello",
            Type = "Buy",
            CompanyCode = "AMZ",
            ExecutedPrice = 33.45m,
            NetConsideration = 20.3m
        }));

        int count = await _halifaxTransactionRepository.CountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    private void CreateTransactions()
    {
        _halifaxTransactionRepository.AddAsync(new HalifaxTransaction
        {
            Date = new LocalDateTime(2000, 1, 1, 13, 0, 0),
            Exchange = "NY",
            Quantity = 1900,
            Reference = "hello",
            Type = "Buy",
            CompanyCode = "AMZ",
            ExecutedPrice = 33.45m,
            NetConsideration = 20.3m
        });

        _halifaxTransactionRepository.AddAsync(new HalifaxTransaction
        {
            Date = new LocalDateTime(2000, 1, 1, 13, 0, 0),
            Exchange = "NY",
            Quantity = 1900,
            Reference = "hello2",
            Type = "Buy",
            CompanyCode = "AMZ",
            ExecutedPrice = 33.45m,
            NetConsideration = 20.3m
        });
    }

    protected override string TableName { get; set; } = "HalifaxTransaction";
}