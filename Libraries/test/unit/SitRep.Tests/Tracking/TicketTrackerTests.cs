namespace SitRep.Tests.Tracking;

[TestFixture]
public class TicketTrackerTests
{
    private Mock<ITicketTrackingStore> _ticketTrackingStore;
    private TicketTracker _ticketTracker;
    private Faker _faker;

    [SetUp]
    public void SetUp()
    {
        _ticketTrackingStore = new Mock<ITicketTrackingStore>();
        _ticketTracker = new TicketTracker(_ticketTrackingStore.Object);
        _faker = new Faker();
    }

    [Test]
    public async Task OpenTicketAsync_SetsPendingStage()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var startingState = new StartingState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketTracker.OpenTicketAsync(startingState);

        // Assert
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Pending);
        ticketStatus.IssuedTo.Should().Be(issuedTo);
        ticketStatus.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticketStatus.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticketStatus.ProcessingMessage.Should().BeNull();
        ticketStatus.DateClosed.Should().BeNull();
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ValidationErrors.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task OpenTicketAsync_StoresTicketStatus()
    {
        // Arrange
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var startingState = new StartingState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketTracker.OpenTicketAsync(startingState);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(ticketStatus), Times.Once);
    }

    [Test]
    public async Task ProgressTicketAsync_WhenTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var progressMessage = _faker.Random.AlphaNumeric(40);

        var state = new ProgressState(progressMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(trackingNumber))
                            .ReturnsAsync((TicketStatus) null!);

        // Act
        var action = async () => await _ticketTracker.ProgressTicketAsync(trackingNumber, state);

        // Assert
        await action.Should().ThrowAsync<TrackingNumberNotFoundException>();
    }

    [Test]
    public async Task ProgressTicketAsync_SetsInProgressStage()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var progressMessage = _faker.Random.AlphaNumeric(40);

        var state = new ProgressState(progressMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.ProgressTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.InProgress);
        ticketStatus.ProcessingMessage.Should().Be(progressMessage);
        ticketStatus.DateClosed.Should().BeNull();
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ValidationErrors.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task ProgressTicketAsync_StoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var progressMessage = _faker.Random.AlphaNumeric(40);

        var state = new ProgressState(progressMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        await _ticketTracker.ProgressTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(startingTicketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenSuccessTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var resourceIdentifier = _faker.Internet.Url();
        var successMessage = _faker.Random.AlphaNumeric(40);

        var state = new SuccessState(resourceIdentifier, successMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(trackingNumber))
                            .ReturnsAsync((TicketStatus) null!);

        // Act
        var action = async () => await _ticketTracker.CloseTicketAsync(trackingNumber, state);

        // Assert
        await action.Should().ThrowAsync<TrackingNumberNotFoundException>();
    }

    [Test]
    public async Task CloseTicketAsync_WhenSuccess_ThenSetsClosedStage()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var resourceIdentifier = _faker.Internet.Url();
        var successMessage = _faker.Random.AlphaNumeric(40);

        var state = new SuccessState(resourceIdentifier, successMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Succeeded);
        ticketStatus.ProcessingMessage.Should().Be(successMessage);
        ticketStatus.ResourceIdentifier.Should().Be(resourceIdentifier);
        ticketStatus.ValidationErrors.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenSuccess_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var resourceIdentifier = _faker.Internet.Url();
        var successMessage = _faker.Random.AlphaNumeric(40);

        var state = new SuccessState(resourceIdentifier, successMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(startingTicketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenValidationFailureTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var validationErrors = CreateValidationErrors();
        var validationMessage = _faker.Random.AlphaNumeric(40);

        var state = new ValidationState(validationErrors, validationMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(trackingNumber))
                            .ReturnsAsync((TicketStatus) null!);

        // Act
        var action = async () => await _ticketTracker.CloseTicketAsync(trackingNumber, state);

        // Assert
        await action.Should().ThrowAsync<TrackingNumberNotFoundException>();
    }

    [Test]
    public async Task CloseTicketAsync_WhenValidationFailure_ThenSetsClosedStage()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var validationErrors = CreateValidationErrors();
        var validationMessage = _faker.Random.AlphaNumeric(40);

        var state = new ValidationState(validationErrors, validationMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Failed);
        ticketStatus.ProcessingMessage.Should().Be(validationMessage);
        ticketStatus.ValidationErrors.Should().BeEquivalentTo(validationErrors);
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenValidationFailure_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var validationErrors = CreateValidationErrors();
        var validationMessage = _faker.Random.AlphaNumeric(40);

        var state = new ValidationState(validationErrors, validationMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(startingTicketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenErrorFailureTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var errorCode = _faker.Random.AlphaNumeric(5);
        var errorMessage = _faker.Random.AlphaNumeric(40);

        var state = new FailureState(errorCode, errorMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(trackingNumber))
                            .ReturnsAsync((TicketStatus) null!);

        // Act
        var action = async () => await _ticketTracker.CloseTicketAsync(trackingNumber, state);

        // Assert
        await action.Should().ThrowAsync<TrackingNumberNotFoundException>();
    }

    [Test]
    public async Task CloseTicketAsync_WhenErrorFailure_ThenSetsClosedStage()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var errorCode = _faker.Random.AlphaNumeric(5);
        var errorMessage = _faker.Random.AlphaNumeric(40);

        var state = new FailureState(errorCode, errorMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Failed);
        ticketStatus.ProcessingMessage.Should().Be(errorMessage);
        ticketStatus.ErrorCode.Should().Be(errorCode);
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ValidationErrors.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenErrorFailure_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var errorCode = _faker.Random.AlphaNumeric(5);
        var errorMessage = _faker.Random.AlphaNumeric(40);

        var state = new FailureState(errorCode, errorMessage);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(startingTicketStatus), Times.Once);
    }

    private TicketStatus CreateTicketStatus()
    {
        var trackingNumber = CombGuid.NewGuid();
        var issuedTo = _faker.Random.AlphaNumeric(10);
        var issuedOnBehalfOf = _faker.Random.AlphaNumeric(20);
        var reasonForIssuing = _faker.Random.AlphaNumeric(30);

        var result = new TicketStatus(trackingNumber, issuedTo, issuedOnBehalfOf, reasonForIssuing);

        return result;
    }

    private Dictionary<string, string[]> CreateValidationErrors()
    {
        var errors = new Dictionary<string, string[]>
        {
            { _faker.Random.AlphaNumeric(10), [_faker.Random.AlphaNumeric(20)] },
            { _faker.Random.AlphaNumeric(10), [_faker.Random.AlphaNumeric(20), _faker.Random.AlphaNumeric(20)] }
        };

        return errors;
    }
}
