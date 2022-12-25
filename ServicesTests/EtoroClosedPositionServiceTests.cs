namespace ServicesTests;

[TestFixture]
public class EtoroClosedPositionServiceTests : SetupBase
{
    private EtoroClosedPositionService _service;
        
    private EtoroClosedPositionRepository _etoroClosedPositionRepository;

    [SetUp]
    public void Setup()
    {
        Initialise();
            
        _etoroClosedPositionRepository = new EtoroClosedPositionRepository();
            
        _service = new EtoroClosedPositionService(_etoroClosedPositionRepository,
            new DuplicateEtoroClosedPositionExistsQuery());
    }

    [Test]
    public async Task Should_LoadPositions_When_RepositoryEmpty()
    {
        int id = await _service.AddAsync(new EtoroClosedPosition
        {
            Action = "Action",
            Amount = 200,
            Profit = 2000,
            Spread = 30,
            Units = 1000,
            ClosedDate = new LocalDateTime(2000, 1, 1, 1, 0, 0),
            CloseRate = 100,
            OpenDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            OpenRate = 120,
            PositionId = 1,
            RollOverFees = 2,
            StopLossRate = 80,
            TakeProfitRate = 10
        });

        Assert.That(id, Is.EqualTo(1));
            
        int count = await _etoroClosedPositionRepository.CountAsync();
        Assert.That(count, Is.EqualTo(1));
    }
        
    [Test]
    public async Task Should_Not_LoadPositions_When_RepositoryContainsDuplicate()
    {
        CreatePositions();

        Assert.ThrowsAsync<DuplicateExistsException>(async () => await _service.AddAsync(new EtoroClosedPosition
        {
            Action = "Action",
            Amount = 200,
            Profit = 2000,
            Spread = 30,
            Units = 1000,
            ClosedDate = new LocalDateTime(2000, 1, 1, 1, 0, 0),
            CloseRate = 100,
            OpenDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            OpenRate = 120,
            PositionId = 1,
            RollOverFees = 2,
            StopLossRate = 80,
            TakeProfitRate = 10
        }));

        int count = await _etoroClosedPositionRepository.CountAsync();
        Assert.That(count, Is.EqualTo(2));
    }

    private void CreatePositions()
    {
        _etoroClosedPositionRepository.AddAsync(new EtoroClosedPosition
        {
            Action = "Action",
            Amount = 200,
            Profit = 2000,
            Spread = 30,
            Units = 1000,
            ClosedDate = new LocalDateTime(2000, 1, 1, 1, 0, 0),
            CloseRate = 100,
            OpenDate = new LocalDateTime(2000, 1, 1, 0, 30, 0),
            OpenRate = 120,
            PositionId = 1,
            RollOverFees = 2,
            StopLossRate = 80,
            TakeProfitRate = 10
        });

        _etoroClosedPositionRepository.AddAsync(new EtoroClosedPosition
        {
            Action = "Action",
            Amount = 100,
            Profit = 1000,
            Spread = 20,
            Units = 1000,
            ClosedDate = new LocalDateTime(1999, 1, 1, 1, 0, 0),
            CloseRate = 100,
            OpenDate = new LocalDateTime(1999, 1, 1, 0, 30, 0),
            OpenRate = 110,
            PositionId = 2,
            RollOverFees = 2,
            StopLossRate = 70,
            TakeProfitRate = 500
        });
    }

    protected override string TableName { get; set; } = "EtoroClosedPosition";
}