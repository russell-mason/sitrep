namespace SitRep.Tests.Tracking.Transitions;

[TestFixture]
public class ProgressTransitionTests
{
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
    }

    [Test]
    public void TransitionState_SetsTicketProperties()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var progressMessage = _faker.Random.AlphaNumeric(40);

        var startingTicket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        var transition = new ProgressTransition(progressMessage);

        // Act
        var ticket = transition.TransitionState(startingTicket);

        // Assert
        ticket.TrackingNumber.Should().NotBeEmpty();
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticket.ProcessingState.Should().Be(ProcessingState.InProgress);
        ticket.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.DefaultExpirationInMinutes), 500.Milliseconds());
        ticket.ProcessingMessage.Should().Be(progressMessage);
        ticket.DateLastProgressed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.DateClosed.Should().BeNull();
        ticket.ResourceIdentifier.Should().BeNull();
        ticket.ValidationErrors.Should().BeNull();
        ticket.ErrorCode.Should().BeNull();
    }
}
