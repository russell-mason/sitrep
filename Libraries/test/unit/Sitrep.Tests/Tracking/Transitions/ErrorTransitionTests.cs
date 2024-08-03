namespace Sitrep.Tests.Tracking.Transitions;

[TestFixture]
public class ErrorTransitionTests
{
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
    }

    [Test]
    public void Action_IsTicketTransitionError()
    {
        // Arrange
        var errorMessage = _faker.Random.AlphaNumeric(40);
        var errorCode = _faker.Random.AlphaNumeric(50);

        var transition = new ErrorTransition(errorMessage, errorCode);

        // Act
        var action = transition.Action;

        // Assert
        action.Should().Be("ticket:transition:error");
    }

    [Test]
    public void TransitionState_SetsTicketProperties()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var errorMessage = _faker.Random.AlphaNumeric(40);
        var errorCode = _faker.Random.AlphaNumeric(50);

        var startingTicket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        var transition = new ErrorTransition(errorMessage, errorCode);

        // Act
        var ticket = transition.TransitionState(startingTicket);

        // Assert
        ticket.TrackingNumber.Should().NotBeEmpty();
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticket.ProcessingState.Should().Be(ProcessingState.Failed);
        ticket.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.ExpirationPeriodInMinutes), 500.Milliseconds());
        ticket.ProcessingMessage.Should().Be(errorMessage);
        ticket.DateLastProgressed.Should().BeNull();
        ticket.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ResourceIdentifier.Should().BeNull();
        ticket.ValidationErrors.Should().BeNull();
        ticket.ErrorCode.Should().Be(errorCode);
    }
}