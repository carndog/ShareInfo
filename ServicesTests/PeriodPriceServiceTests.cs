namespace ServicesTests;

[TestFixture]
public class PeriodPriceServiceTests : SetupBase
{
    private PeriodPriceService _service;
        
    private PeriodPriceRepository _periodPriceRepository;
    private LocalDate _date;

    protected override string TableName { get; set; } = "PeriodPrice";

    [SetUp]
    public void Setup()
    {
        Initialise();
            
        _periodPriceRepository = new PeriodPriceRepository();
            
        _service = new PeriodPriceService(
            _periodPriceRepository,
            new DuplicatePeriodPriceExistsQuery(),
            new GetPeriodPriceBySymbolQuery());
            
        _date = new LocalDate(2020, 1, 1);
    }

    [Test]
    public async Task Should_Insert_When_RepositoryEmpty()
    {
        int id = await _service.AddAsync(new PeriodPrice
        {
            Close = 23m,
            Date = _date,
            High = 34m,
            Low = 12m,
            Open = 12.56m,
            Symbol = "HHH",
            Volume = 23444.344m,
            PeriodType = PeriodType.Daily
        });

        PeriodPrice price = await _periodPriceRepository.GetAsync(id);
        Assert.That(price.Close, Is.EqualTo(23m));
        Assert.That(price.Date, Is.EqualTo(_date));
        Assert.That(price.High, Is.EqualTo(34m));
        Assert.That(price.Low, Is.EqualTo(12m));
        Assert.That(price.Open, Is.EqualTo(12.56m));
        Assert.That(price.Symbol, Is.EqualTo("HHH"));
        Assert.That(price.Volume, Is.EqualTo(23444.344m));
        Assert.That(price.PeriodType, Is.EqualTo(PeriodType.Daily));
        Assert.That(price.PeriodPriceId, Is.Not.EqualTo(Guid.Empty));
            
        int count = await _periodPriceRepository.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }
        
    [Test]
    public async Task Should_Not_Insert_When_RepositoryContainsDuplicate()
    {
        CreateRecords();
            
        Assert.ThrowsAsync<DuplicateExistsException>(async () => await _service.AddAsync(new PeriodPrice
        {
            Close = 23m,
            Date = _date,
            High = 34m,
            Low = 12m,
            Open = 12.56m,
            Symbol = "HHH",
            Volume = 23444.344m,
            PeriodType = PeriodType.Daily,
            PeriodPriceId = Guid.NewGuid()
        }));
            
        int count = await _periodPriceRepository.CountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    private void CreateRecords()
    {
        _periodPriceRepository.AddAsync(new PeriodPrice
        {
            Close = 23m,
            Date = _date,
            High = 34m,
            Low = 12m,
            Open = 12.56m,
            Symbol = "HHH",
            Volume = 23444.344m,
            PeriodType = PeriodType.Daily,
            PeriodPriceId = Guid.NewGuid()
        });

        _periodPriceRepository.AddAsync(new PeriodPrice
        {
            Close = 223m,
            Date = _date.PlusDays(1),
            High = 34m,
            Low = 12m,
            Open = 12.56m,
            Symbol = "GGG",
            Volume = 23444.344m,
            PeriodType = PeriodType.Daily,
            PeriodPriceId = Guid.NewGuid()
        });
    }
}