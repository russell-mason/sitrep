namespace Sitrep.Tests.Tracking;

[TestFixture]
public class TrackingNumberNotFoundExceptionTests
{
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _faker = new Faker();
    }

    [Test]
    public void Type_IsPublicException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();

        // Act
        var exception = new TrackingNumberNotFoundException(trackingNumber);

        // Assert
        exception.Should().BeAssignableTo<PublicException>();
    }

    [Test]
    public void Constructor_WhenNoMessageIsProvided_ThenDefaultMessageIsUsed()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();

        // Act
        var exception = new TrackingNumberNotFoundException(trackingNumber);

        // Assert
        exception.TrackingNumber.Should().Be(trackingNumber);
        exception.Message.Should().StartWith("Ticket '");
        exception.Message.Should().EndWith("' not found");
    }

    [Test]
    public void Constructor_WhenTrackingNumberIsProvided_ThenTrackingNumberIsSet()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var message = _faker.Random.AlphaNumeric(30);

        // Act
        var exception = new TrackingNumberNotFoundException(trackingNumber, message);

        // Assert
        exception.TrackingNumber.Should().Be(trackingNumber);
        exception.Message.Should().Be(message);
    }

    [Test]
    public void Constructor_WhenTrackingNumberIsProvided_ThenTrackingNumberIsSetWithInnerException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var message = _faker.Random.AlphaNumeric(30);
        var innerException = new Exception();

        // Act
        var exception = new TrackingNumberNotFoundException(trackingNumber, message, innerException);

        // Assert
        exception.TrackingNumber.Should().Be(trackingNumber);
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
    }
}