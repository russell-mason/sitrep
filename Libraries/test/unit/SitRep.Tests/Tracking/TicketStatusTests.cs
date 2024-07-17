namespace SitRep.Tests.Tracking;

[TestFixture]
public class TicketStatusTests
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
        var result = new TicketStatus(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Assert
        result.TrackingNumber.Should().Be(trackingNumber);
        result.IssuedTo.Should().Be(issuedTo);
        result.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        result.ReasonForIssuing.Should().Be(reasonForIssuing);
        result.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        result.ProcessingStage.Should().Be(ProcessingStage.Pending);
        result.IsClosed.Should().BeFalse();
        result.ProcessingMessage.Should().BeNull();
        result.DateClosed.Should().BeNull();
        result.ResourceIdentifier.Should().BeNull();
        result.ValidationErrors.Should().BeNull();
        result.ErrorCode.Should().BeNull();
    }

    [TestCase(ProcessingStage.Pending, false)]
    [TestCase(ProcessingStage.InProgress, false)]
    [TestCase(ProcessingStage.Succeeded, true)]
    [TestCase(ProcessingStage.Failed, true)]
    public void Constructor_WhenProcessingStageChanges_ThenIsClosedIsReflected(ProcessingStage stage, bool expectedIsClosed)
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var status = new TicketStatus(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var result = status with { ProcessingStage = stage };

        // Assert
        result.IsClosed.Should().Be(expectedIsClosed);
    }
}
