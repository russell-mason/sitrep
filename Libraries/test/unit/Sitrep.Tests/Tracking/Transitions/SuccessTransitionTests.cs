namespace Sitrep.Tests.Tracking.Transitions;

[TestFixture]
public class SuccessTransitionTests
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

        var successMessage = _faker.Random.AlphaNumeric(40);
        var resourceIdentifier = _faker.Internet.Url();

        var startingTicket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        var transition = new SuccessTransition(successMessage, resourceIdentifier);

        // Act
        var ticket = transition.TransitionState(startingTicket);

        // Assert
        ticket.TrackingNumber.Should().NotBeEmpty();
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticket.ProcessingState.Should().Be(ProcessingState.Succeeded);
        ticket.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.ExpirationPeriodInMinutes), 500.Milliseconds());
        ticket.ProcessingMessage.Should().Be(successMessage);
        ticket.DateLastProgressed.Should().BeNull();
        ticket.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ResourceIdentifier.Should().Be(resourceIdentifier);
        ticket.ValidationErrors.Should().BeNull();
        ticket.ErrorCode.Should().BeNull();
    }
}