namespace SitRep.Tests.Tracking.Transitions;

[TestFixture]
public class ValidationErrorTransitionTests
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

        var validationMessage = _faker.Random.AlphaNumeric(40);
        var validationErrors = CreateValidationErrors();

        var startingTicket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        var transition = new ValidationErrorTransition(validationMessage, validationErrors);

        // Act
        var ticket = transition.TransitionState(startingTicket);

        // Assert
        ticket.TrackingNumber.Should().NotBeEmpty();
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticket.ProcessingState.Should().Be(ProcessingState.Failed);
        ticket.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.DefaultExpirationInMinutes), 500.Milliseconds());
        ticket.ProcessingMessage.Should().Be(validationMessage);
        ticket.DateLastProgressed.Should().BeNull();
        ticket.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ResourceIdentifier.Should().BeNull();
        ticket.ValidationErrors.Should().BeEquivalentTo(validationErrors);
        ticket.ErrorCode.Should().BeNull();
    }

    private ValidationErrorDictionary CreateValidationErrors()
    {
        var errors = new ValidationErrorDictionary
        {
            { _faker.Random.AlphaNumeric(10), [_faker.Random.AlphaNumeric(20)] },
            { _faker.Random.AlphaNumeric(10), [_faker.Random.AlphaNumeric(20), _faker.Random.AlphaNumeric(20)] }
        };

        return errors;
    }
}
