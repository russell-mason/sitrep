namespace SitRep.Tests.Tracking;

[TestFixture]
public class TicketTests
{
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
    }

    [Test]
    public void Constructor_SetsDefaultProperties()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        // Act
        var result = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Assert
        result.TrackingNumber.Should().Be(trackingNumber);
        result.IssuedTo.Should().Be(issuedTo);
        result.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        result.ReasonForIssuing.Should().Be(reasonForIssuing);
        result.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        result.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(Ticket.ExpirationPeriodInMinutes), 500.Milliseconds());
        result.ProcessingState.Should().Be(ProcessingState.Pending);
        result.IsClosed.Should().BeFalse();
        result.ProcessingMessage.Should().BeNull();
        result.DateClosed.Should().BeNull();
        result.ResourceIdentifier.Should().BeNull();
        result.ValidationErrors.Should().BeNull();
        result.ErrorCode.Should().BeNull();
    }

    [TestCase(ProcessingState.Pending, false)]
    [TestCase(ProcessingState.InProgress, false)]
    [TestCase(ProcessingState.Succeeded, true)]
    [TestCase(ProcessingState.Failed, true)]
    public void Constructor_WhenProcessingStageChanges_ThenIsClosedIsReflected(ProcessingState stage, bool expectedIsClosed)
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var ticket = new Ticket(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var result = ticket with { ProcessingState = stage };

        // Assert
        result.IsClosed.Should().Be(expectedIsClosed);
    }
}