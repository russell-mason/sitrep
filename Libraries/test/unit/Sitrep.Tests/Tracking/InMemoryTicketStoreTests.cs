namespace Sitrep.Tests.Tracking;

[TestFixture]
public class InMemoryTicketStoreTests
{
    private Faker _faker;
    private InMemoryTicketStore _store;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();

        var options = new InMemoryTicketStoreOptions();
        var optionsWrapper = new OptionsWrapper<InMemoryTicketStoreOptions>(options);

        _store = new InMemoryTicketStore(optionsWrapper);
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
        var ticket = CreateTicket();

        await _store.StoreTicketAsync(ticket);

        // Act
        var result = await _store.GetTicketAsync(ticket.TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(ticket);
    }

    [Test]
    public async Task GetTicketAsync_WhenMultipleEntriesInStore_ThenReturnsTheCorrectEntry()
    {
        // Arrange
        var tickets = CreateTickets(3);

        await StoreTicketAsync(tickets);

        // Act
        var result = await _store.GetTicketAsync(tickets[1].TrackingNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(tickets[1]);
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
        var tickets1 = CreateTicketsWithSameIssuedTo(2);
        var tickets2 = CreateTicketsWithSameIssuedTo(3);
        var tickets3 = CreateTicketsWithSameIssuedTo(4);

        var all = tickets1.Concat(tickets2).Concat(tickets3);

        await StoreTicketAsync(all);

        var filterBy = tickets2.First().IssuedTo;

        // Act
        var result = await _store.GetTicketsAsync(filterBy);

        // Assert
        result.Count().Should().Be(3);
    }

    [TestCase(true, int.MaxValue, 0, 11, 10, 1, 1)] // Removes all expired tickets even when threshold not hit
    [TestCase(false, 10, 6, 10, 0, 10, 10)] // Hits threshold so nothing removed
    [TestCase(false, 10, 6, 1, 0, 11, 4)] // Last entry exceeds threshold removes 6 + 1 (exceeds threshold by 1)
    [TestCase(false, 10, 6, 5, 0, 11, 11)] // Check on 5*2 so 11th entry misses check
    [TestCase(false, 10, 6, 5, 0, 12, 12)] // Check on 5*2 so 11th entry misses check
    [TestCase(false, 10, 6, 5, 0, 13, 13)] // Check on 5*2 so 11th entry misses check
    [TestCase(false, 10, 6, 5, 0, 14, 14)] // Check on 5*2 so 11th entry misses check
    [TestCase(false, 10, 6, 5, 0, 15, 4)] // Check on 5*3 so hit removes so that result is 10 - 6 (15 to 6 = 9 removed
    [TestCase(true, 10, 6, 12, 7, 5, 5)] // 5 + 7 total - removes 7 expired, so under threshold, no others removed
    [TestCase(true, 10, 6, 12, 5, 7, 7)] // 5 + 7 total - removes 7 expired, so under threshold, no others removed
    [TestCase(true, 10, 6, 20, 8, 12, 4)] // 8 + 12 total - removes 8 expired, so 12 above threshold, removes to be 6 under
    public async Task StoreTicketAsync_WhenIntervalTriggered_ThenTicketsAreRemoved(
        bool discardExpired,
        int discardThreshold,
        int discardCount,
        int discardInternal,
        int expiredCount,
        int currentCount,
        int expectedCount)
    {
        // Arrange
        var options = new InMemoryTicketStoreOptions
                      {
                          DiscardExpired = discardExpired,
                          DiscardThreshold = discardThreshold,
                          DiscardCount = discardCount,
                          DiscardInterval = discardInternal
                      };

        var optionsWrapper = new OptionsWrapper<InMemoryTicketStoreOptions>(options);

        _store = new InMemoryTicketStore(optionsWrapper);

        var issuedTo = _faker.Random.AlphaNumeric(10);

        Ticket.ExpirationPeriodInMinutes = -10;
        var expiredTickets = CreateTicketsWithSameIssuedTo(expiredCount, issuedTo, true);

        Ticket.ExpirationPeriodInMinutes = 10;
        var currentTickets = CreateTicketsWithSameIssuedTo(currentCount, issuedTo, true);

        // Act
        await StoreTicketAsync(expiredTickets);
        await StoreTicketAsync(currentTickets);

        // Assert
        var all = await _store.GetTicketsAsync(issuedTo);

        all.Count().Should().Be(expectedCount);
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

    private List<Ticket> CreateTickets(int quantity) =>
        Enumerable.Range(0, quantity).Select(_ => CreateTicket()).ToList();

    private List<Ticket> CreateTicketsWithSameIssuedTo(int quantity, string? issuedTo = null, bool closed = false)
    {
        var selectedIssuedTo = issuedTo ?? _faker.Random.AlphaNumeric(10);

        var result = Enumerable.Range(0, quantity)
                               .Select(_ => CreateTicket() with
                                            {
                                                IssuedTo = selectedIssuedTo,
                                                ProcessingState = closed ? ProcessingState.Succeeded : ProcessingState.Pending
                                            })
                               .ToList();

        return result;
    }

    private async Task StoreTicketAsync(IEnumerable<Ticket> tickets)
    {
        foreach (var ticket in tickets)
        {
            await _store.StoreTicketAsync(ticket);
        }
    }
}