namespace SitRep.Tests.Tracking;

[TestFixture]
public class TicketProcessorTests
{
    private Mock<ITicketStore> _ticketStore;
    private TicketProcessor _ticketProcessor;
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _ticketStore = new Mock<ITicketStore>();
        _ticketProcessor = new TicketProcessor(_ticketStore.Object);
        _faker = new Faker();
    }

    [Test]
    public async Task CreateTicketAsync_SetsPendingStage()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketProcessor.CreateTicketAsync(creator);

        // Assert
        ticketStatus.ProcessingState.Should().Be(ProcessingState.Pending);
        ticketStatus.IssuedTo.Should().Be(issuedTo);
        ticketStatus.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticketStatus.ReasonForIssuing.Should().Be(reasonForIssuing);
    }

    [Test]
    public async Task OpenTicketAsync_StoresTicketStatus()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketProcessor.CreateTicketAsync(creator);

        // Assert
        _ticketStore.Verify(tts => tts.StoreTicketAsync(ticketStatus), Times.Once);
    }

    [Test]
    public async Task TransitionTicketAsync_WhenTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();

        var transition = new TestTransitionTicketState();

        _ticketStore.Setup(tts => tts.GetTicketAsync(trackingNumber))
                    .ReturnsAsync((Ticket) null!);

        // Act
        var action = async () => await _ticketProcessor.TransitionTicketAsync(trackingNumber, transition);

        // Assert
        await action.Should().ThrowAsync<TrackingNumberNotFoundException>();
    }

    [Test]
    public async Task TransitionTicketAsync_SetsInProgressStage()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);
        var startingTicket = creator.CreateState();

        var transition = new TestTransitionTicketState();

        _ticketStore.Setup(tts => tts.GetTicketAsync(startingTicket.TrackingNumber))
                    .ReturnsAsync(startingTicket);

        // Act
        var ticketStatus = await _ticketProcessor.TransitionTicketAsync(startingTicket.TrackingNumber, transition);

        // Assert
        ticketStatus.ProcessingState.Should().Be(ProcessingState.InProgress);
    }

    private class TestCreateTicketState(string issuedTo, string issuedOnBehalfOf, string reasonForIssuing) : ICreateTicketState
    {
        private readonly Guid _trackingNumber = CombGuid.NewGuid();

        public Ticket CreateState() => new(_trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);
    }

    private class TestTransitionTicketState() : ITransitionTicketState
    {
        public Ticket TransitionState(Ticket ticket) => ticket with { ProcessingState = ProcessingState.InProgress };
    }
}