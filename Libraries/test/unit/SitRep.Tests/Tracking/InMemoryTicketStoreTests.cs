namespace SitRep.Tests.Tracking;

[TestFixture]
public class InMemoryTicketStoreTests
{
    private Faker _faker;
    private InMemoryTicketStore _store;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
        _store = new InMemoryTicketStore();
    }

    [Test]
    public async Task StoreTicketAsync_WhenNotInStore_ThenIsAdded()
    {
        // Arrange
        var ticket = CreateTicket();

        // Act
        await _store.StoreTicketAsync(ticket);

        // Assert
        var result = await _store.GetTicketAsync(ticket.TrackingNumber);

        result.Should().NotBeNull();
        result.Should().Be(ticket);
    }

    [Test]
    public async Task StoreTicketAsync_WhenAlreadyInStore_ThenIsUpdated()
    {
        // Arrange

        // Pending is the default opening state, but specified to ensure change is clearly verifiable
        var ticket = CreateTicket() with { ProcessingState = ProcessingState.Pending };

        await _store.StoreTicketAsync(ticket);

        var updatedTicket = ticket with { ProcessingState = ProcessingState.InProgress };

        // Act
        await _store.StoreTicketAsync(updatedTicket);

        // Assert
        var result = await _store.GetTicketAsync(ticket.TrackingNumber);

        result.Should().NotBeNull();
        result!.ProcessingState.Should().NotBe(ProcessingState.Pending);
        result.ProcessingState.Should().Be(ProcessingState.InProgress);
    }

    [Test]
    public async Task GetTicketAsync_WhenNotInStore_ThenIsNull()
    {
        // Arrange

        // Act
        var result = await _store.GetTicketAsync(CombGuid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Test] public async Task GetTicketAsync_WhenInStore_ThenIsReturned()
    {
        // Arrange
        var ticketStatus = CreateTicket();

        await _store.StoreTicketAsync(ticketStatus);

        // Act
        var result = await _store.GetTicketAsync(ticketStatus.TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(ticketStatus);
    }

    [Test]
    public async Task GetTicketAsync_WhenMultipleEntriesInStore_ThenReturnsTheCorrectEntry()
    {
        // Arrange
        var ticketStatus1 = CreateTicket();
        var ticketStatus2 = CreateTicket();
        var ticketStatus3 = CreateTicket();

        await _store.StoreTicketAsync(ticketStatus1);
        await _store.StoreTicketAsync(ticketStatus2);
        await _store.StoreTicketAsync(ticketStatus3);

        // Act
        var result = await _store.GetTicketAsync(ticketStatus2.TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(ticketStatus2);
    }

    [Test]
    public async Task GetTicketsAsync_WhenNoEntries_ThenReturnsEmpty()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);

        // Act
        var result = (await _store.GetTicketsAsync(issuedTo)).ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetTicketsAsync_FiltersByIssuedTo()
    {
        // Arrange
        var ticketStatuses1 = CreateTicketsWithSameIssuedTo(2);
        var ticketStatuses2 = CreateTicketsWithSameIssuedTo(3);
        var ticketStatuses3 = CreateTicketsWithSameIssuedTo(4);

        var all = ticketStatuses1.Concat(ticketStatuses2).Concat(ticketStatuses3);

        foreach (var ticketStatus in all)
        {
            await _store.StoreTicketAsync(ticketStatus);
        }

        var filterBy = ticketStatuses2.First().IssuedTo;

        // Act
        var result = await _store.GetTicketsAsync(filterBy);

        // Assert
        result.Count().Should().Be(3);
    }

    private Ticket CreateTicket()
    {
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var ticket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        return ticket;
    }

    private List<Ticket> CreateTicketsWithSameIssuedTo(int quantity)
    {
        var issuedTo = _faker.Random.AlphaNumeric(10);

        var result = Enumerable.Range(0, quantity)
                               .Select(_ => CreateTicket() with { IssuedTo = issuedTo })
                               .ToList();

        return result;
    }
}