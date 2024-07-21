namespace SitRep.Tests.Tracking.Transitions;

[TestFixture]
public class OpenTicketTests
{
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
    }

    [Test]
    public void CreateState_CreatesTicketWithPropertiesSet()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var creator = new OpenTicket(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticket = creator.CreateState();

        // Assert
        ticket.TrackingNumber.Should().NotBeEmpty();
        ticket.IssuedTo.Should().Be(issuedTo);
        ticket.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticket.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticket.ProcessingState.Should().Be(ProcessingState.Pending);
        ticket.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticket.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.DefaultExpirationInMinutes), 500.Milliseconds());
        ticket.ProcessingMessage.Should().BeNull();
        ticket.DateLastProgressed.Should().BeNull();
        ticket.DateClosed.Should().BeNull();
        ticket.ResourceIdentifier.Should().BeNull();
        ticket.ValidationErrors.Should().BeNull();
        ticket.ErrorCode.Should().BeNull();
    }
}
