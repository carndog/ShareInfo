using Microsoft.Extensions.Configuration;
using Moq;

namespace ServicesTests;

[TestFixture]
public class PricesServiceTests : SetupBase
{
    private IPricesService _service;
        
    private PriceRepository _priceStreamRepository;
        
    private ZonedDateTime _date;
        
    private DateTime _dateTime;

    private string _timeZone;

    [SetUp]
    public void Setup()
    {
        Initialise();
        
        IConfigurationRoot configuration = TestConfigurationProvider.GetConfigurationRoot();

        Mock<IGetDatabase> databaseMock = new Mock<IGetDatabase>();
        databaseMock.Setup(x => x.GetConnectionString()).Returns(configuration.GetSection("connectionString").Value);
            
        _priceStreamRepository = new PriceRepository(databaseMock.Object);
            
        PricesService pricesService = new PricesService(
            _priceStreamRepository,
            new DuplicatePriceExistsQuery(databaseMock.Object));
            
        _service = new PricesServiceDecorator(pricesService);
            
        LocalDateTime localDateTime = new LocalDateTime(2020, 1, 1, 10, 3, 0);
        _date = new ZonedDateTime(localDateTime, DateTimeZone.Utc, Offset.Zero);
        _dateTime = localDateTime.ToDateTimeUnspecified();
        _timeZone = DateTimeZone.Utc.ToString();
    }

    [Test]
    public async Task Should_Insert_When_RepositoryEmpty()
    {
        int id = await _service.AddAsync(new AssetPrice
        {
            Date = _date,
            Price = 12.56m,
            OriginalPrice = 12.56237843m,
            Symbol = "HHH",
            Exchange = "London",
            TimeZone = _timeZone,
            CurrentDateTime = _dateTime
        });

        AssetPrice price = await _priceStreamRepository.GetAsync(id);
            
        Assert.That(price.CurrentDateTime, Is.EqualTo(_date.LocalDateTime.ToDateTimeUnspecified()));
        Assert.That(price.TimeZone, Is.EqualTo(_date.Zone.ToString()));
        Assert.That(price.Price, Is.EqualTo(12.56m));
        Assert.That(price.OriginalPrice, Is.EqualTo(12.5624m));
        Assert.That(price.Symbol, Is.EqualTo("HHH"));
        Assert.That(price.Exchange, Is.EqualTo("London"));

        int count = await _priceStreamRepository.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }
        
    [Test]
    public async Task Should_Not_Insert_When_RepositoryContainsDuplicate()
    {
        CreateRecords();

        Assert.ThrowsAsync<DuplicateExistsException>(async () =>  await _service.AddAsync(new AssetPrice
        {
            Date = _date,
            Price = 12.56m,
            OriginalPrice = 12.5623545m,
            Symbol = "HHH",
            Exchange = "London",
            TimeZone = _timeZone,
            CurrentDateTime = _dateTime
        }));
            
        int count = await _priceStreamRepository.CountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    private void CreateRecords()
    {
        _priceStreamRepository.AddAsync(new AssetPrice
        {
            Date = _date,
            Price = 12.56m,
            OriginalPrice = 12.5623545m,
            Symbol = "HHH",
            Exchange = "London",
            TimeZone = _timeZone,
            CurrentDateTime = _dateTime
        });

        _priceStreamRepository.AddAsync(new AssetPrice
        {
            Date = _date,
            Price = 12.56m,
            OriginalPrice = 12.5623545m,
            Symbol = "CCC",
            Exchange = "London",
            TimeZone = _timeZone,
            CurrentDateTime = _dateTime
        });
    }

    protected override string TableName { get; set; } = "Prices";
}