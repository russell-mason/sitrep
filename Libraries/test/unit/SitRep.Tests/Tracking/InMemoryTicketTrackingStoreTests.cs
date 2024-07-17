namespace SitRep.Tests.Tracking;

[TestFixture]
public class InMemoryTicketTrackingStoreTests
{
    private Faker _faker;
    private InMemoryTicketTrackingStore _store;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
        _store = new InMemoryTicketTrackingStore();
    }

    [Test]
    public async Task SetTicketStatusAsync_WhenNotInStore_ThenIsAdded()
    {
        // Arrange
        var ticketStatus = CreateTicketStatus();

        // Act
        await _store.SetTicketStatusAsync(ticketStatus);

        // Assert
        var result = await _store.GetTicketStatusAsync(ticketStatus.TrackingNumber);

        result.Should().NotBeNull();
        result.Should().Be(ticketStatus);
    }

    [Test]
    public async Task SetTicketStatusAsync_WhenAlreadyInStore_ThenIsUpdated()
    {
        // Arrange

        // Pending is the default opening stage, but specified to ensure change is clearly verifiable
        var ticketStatus = CreateTicketStatus() with { ProcessingStage = ProcessingStage.Pending };

        await _store.SetTicketStatusAsync(ticketStatus);

        var updatedTicketStatus = ticketStatus with { ProcessingStage = ProcessingStage.InProgress };

        // Act
        await _store.SetTicketStatusAsync(updatedTicketStatus);

        // Assert
        var result = await _store.GetTicketStatusAsync(ticketStatus.TrackingNumber);

        result.Should().NotBeNull();
        result!.ProcessingStage.Should().NotBe(ProcessingStage.Pending);
        result.ProcessingStage.Should().Be(ProcessingStage.InProgress);
    }

    [Test]
    public async Task GetTicketStatusAsync_WhenNotInStore_ThenIsNull()
    {
        // Arrange

        // Act
        var result = await _store.GetTicketStatusAsync(CombGuid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Test] public async Task GetTicketStatusAsync_WhenInStore_ThenIsReturned()
    {
        // Arrange
        var ticketStatus = CreateTicketStatus();

        await _store.SetTicketStatusAsync(ticketStatus);

        // Act
        var result = await _store.GetTicketStatusAsync(ticketStatus.TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(ticketStatus);
    }

    [Test]
    public async Task GetTicketStatusAsync_WhenMultipleEntriesInStore_ThenReturnsTheCorrectEntry()
    {
        // Arrange
        var ticketStatus1 = CreateTicketStatus();
        var ticketStatus2 = CreateTicketStatus();
        var ticketStatus3 = CreateTicketStatus();

        await _store.SetTicketStatusAsync(ticketStatus1);
        await _store.SetTicketStatusAsync(ticketStatus2);
        await _store.SetTicketStatusAsync(ticketStatus3);

        // Act
        var result = await _store.GetTicketStatusAsync(ticketStatus2.TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(ticketStatus2);
    }

    [Test]
    public async Task GetTicketStatusesAsync_WhenNoEntries_ThenReturnsEmpty()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);

        // Act
        var result = (await _store.GetTicketStatusesAsync(issuedTo)).ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetTicketStatusesAsync_FiltersByIssuedTo()
    {
        // Arrange
        var ticketStatuses1 = CreateTicketStatusesWithSameIssuedTo(2);
        var ticketStatuses2 = CreateTicketStatusesWithSameIssuedTo(3);
        var ticketStatuses3 = CreateTicketStatusesWithSameIssuedTo(4);

        var all = ticketStatuses1.Concat(ticketStatuses2).Concat(ticketStatuses3);

        foreach (var ticketStatus in all)
        {
            await _store.SetTicketStatusAsync(ticketStatus);
        }

        var filterBy = ticketStatuses2.First().IssuedTo;

        // Act
        var result = await _store.GetTicketStatusesAsync(filterBy);

        // Assert
        result.Count().Should().Be(3);
    }

    private TicketStatus CreateTicketStatus()
    {
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var ticketStatus = new TicketStatus(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        return ticketStatus;
    }

    private List<TicketStatus> CreateTicketStatusesWithSameIssuedTo(int quantity)
    {
        var issuedTo = _faker.Random.AlphaNumeric(10);

        var result = Enumerable.Range(0, quantity)
                               .Select(_ => CreateTicketStatus() with { IssuedTo = issuedTo })
                               .ToList();

        return result;
    }
}
