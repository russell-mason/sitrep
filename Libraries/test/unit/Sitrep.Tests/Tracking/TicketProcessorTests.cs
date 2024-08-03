namespace Sitrep.Tests.Tracking;

[TestFixture]
public class TicketProcessorTests
{
    private Mock<ITicketStore> _ticketStoreMock;
    private Mock<ITicketNotification> _ticketNotificationMock;
    private TicketProcessor _ticketProcessor;
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _ticketStoreMock = new Mock<ITicketStore>();
        _ticketNotificationMock = new Mock<ITicketNotification>();
        _ticketProcessor = new TicketProcessor(_ticketStoreMock.Object, _ticketNotificationMock.Object);
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
        var ticket = await _ticketProcessor.CreateTicketAsync(creator);

        // Assert
        ticket.ProcessingState.Should().Be(ProcessingState.Pending);
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
    }

    [Test]
    public async Task CreateTicketAsync_StoresTicket()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticket = await _ticketProcessor.CreateTicketAsync(creator);

        // Assert
        _ticketStoreMock.Verify(tts => tts.StoreTicketAsync(ticket), Times.Once);
    }

    [Test]
    public async Task CreateTicketAsync_SendsTicketCreationNotification()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        CreatedEvent? capturedEvent = null;

        _ticketNotificationMock.Setup(tn => tn.NotifyAsync(It.IsAny<CreatedEvent>()))
                               .Callback<CreatedEvent>(createdEvent => capturedEvent = createdEvent);

        // Act
        var ticket = await _ticketProcessor.CreateTicketAsync(creator);

        // Assert
        _ticketNotificationMock.Verify(tn => tn.NotifyAsync(It.IsAny<CreatedEvent>()), Times.Once);

        capturedEvent.Should().NotBeNull();
        capturedEvent!.Action.Should().Be(creator.Action);
        capturedEvent.Ticket.Should().BeEquivalentTo(ticket);
    }

    [Test]
    public async Task TransitionTicketAsync_WhenTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();

        var transition = new TestTransitionTicketState();

        _ticketStoreMock.Setup(tts => tts.GetTicketAsync(trackingNumber))
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

        _ticketStoreMock.Setup(tts => tts.GetTicketAsync(startingTicket.TrackingNumber))
                    .ReturnsAsync(startingTicket);

        // Act
        var ticket = await _ticketProcessor.TransitionTicketAsync(startingTicket.TrackingNumber, transition);

        // Assert
        ticket.ProcessingState.Should().Be(ProcessingState.InProgress);
    }

    [Test]
    public async Task TransitionTicketAsync_SendsTicketCreationNotification()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new TestCreateTicketState(issuedTo, issuedOnBehalfOf, reasonForIssuing);
        var startingTicket = creator.CreateState();

        var transition = new TestTransitionTicketState();

        _ticketStoreMock.Setup(tts => tts.GetTicketAsync(startingTicket.TrackingNumber))
                        .ReturnsAsync(startingTicket);

        TransitionEvent? capturedEvent = null;

        _ticketNotificationMock.Setup(tn => tn.NotifyAsync(It.IsAny<TransitionEvent>()))
                               .Callback<TransitionEvent>(transitionEvent => capturedEvent = transitionEvent);

        // Act
        var ticket = await _ticketProcessor.TransitionTicketAsync(startingTicket.TrackingNumber, transition);

        // Assert
        _ticketNotificationMock.Verify(tn => tn.NotifyAsync(It.IsAny<TransitionEvent>()), Times.Once);

        capturedEvent.Should().NotBeNull();
        capturedEvent!.Action.Should().Be(transition.Action);
        capturedEvent.PreTransitionTicket.Should().BeEquivalentTo(startingTicket);
        capturedEvent.PostTransitionTicket.Should().BeEquivalentTo(ticket);
    }

    private class TestCreateTicketState(string issuedTo, string issuedOnBehalfOf, string reasonForIssuing) : ICreateTicketState
    {
        private readonly Guid _trackingNumber = CombGuid.NewGuid();

        public string Action => "test:state:create";
        
        public Ticket CreateState() => new(_trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);
    }

    private class TestTransitionTicketState : ITransitionTicketState
    {
        public string Action => "test:transition:progress";

        public Ticket TransitionState(Ticket ticket) => ticket with { ProcessingState = ProcessingState.InProgress };
    }
}