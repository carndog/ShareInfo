namespace ServicesTests;

[TestFixture]
public class HalifaxDividendServiceTests : SetupBase
{
    private HalifaxDividendService _service;
        
    private HalifaxDividendRepository _halifaxDividendRepository;

    [SetUp]
    public void Setup()
    {
        Initialise();
            
        _halifaxDividendRepository = new HalifaxDividendRepository();
            
        _service = new HalifaxDividendService(_halifaxDividendRepository,
            new DuplicateHalifaxDividendExistsQuery());
    }

    [Test]
    public async Task Should_LoadDividends_When_RepositoryEmpty()
    {
        int id = await _service.AddAsync(new HalifaxDividend
        {
            Stock = "Lloyds",
            Amount = 2490,
            HandlingOption = "Reinvest",
            IssueDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            SharesHeld = 3000,
            ExDividendDate = new LocalDateTime(1998, 1, 1, 1, 0, 0)
        });

        Assert.That(id, Is.EqualTo(1));
            
        int count = await _halifaxDividendRepository.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }
        
    [Test]
    public async Task Should_Not_LoadDividends_When_RepositoryContainsDuplicate()
    {
        CreateDividends();

        Assert.ThrowsAsync<DuplicateExistsException>(async () => await _service.AddAsync(new HalifaxDividend
        {
            Stock = "Lloyds",
            Amount = 2490,
            HandlingOption = "Reinvest",
            IssueDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            SharesHeld = 3000,
            ExDividendDate = new LocalDateTime(1998, 1, 1, 1, 0, 0),
        }));

        int count = await _halifaxDividendRepository.CountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    private void CreateDividends()
    {
        _halifaxDividendRepository.AddAsync(new HalifaxDividend
        {
            Stock = "Lloyds",
            Amount = 2490,
            HandlingOption = "Reinvest",
            IssueDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            SharesHeld = 3000,
            ExDividendDate = new LocalDateTime(1998, 1, 1, 1, 0, 0),
        });

        _halifaxDividendRepository.AddAsync(new HalifaxDividend
        {
            Stock = "Lloyds",
            Amount = 2490,
            HandlingOption = "Reinvest",
            IssueDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            SharesHeld = 3000,
            ExDividendDate = new LocalDateTime(1997, 1, 1, 1, 0, 0),
        });
    }

    protected override string TableName { get; set; } = "HalifaxDividend";
}