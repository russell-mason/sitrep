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

        var openState = new OpenState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketTracker.OpenTicketAsync(openState);

        // Assert
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Pending);
        ticketStatus.IssuedTo.Should().Be(issuedTo);
        ticketStatus.IssuedOnBehalfOf.Should().Be(issuedOnBehalfOf);
        ticketStatus.ReasonForIssuing.Should().Be(reasonForIssuing);
        ticketStatus.ProcessingMessage.Should().BeNull();
        ticketStatus.DateLastProgressed.Should().BeNull();
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

        var state = new OpenState(issuedTo, issuedOnBehalfOf, reasonForIssuing);

        // Act
        var ticketStatus = await _ticketTracker.OpenTicketAsync(state);

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
        ticketStatus.DateLastProgressed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
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
        var ticketStatus = await _ticketTracker.ProgressTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(ticketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenSuccessTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var successMessage = _faker.Random.AlphaNumeric(40);
        var resourceIdentifier = _faker.Internet.Url();
        
        var state = new SuccessState(successMessage, resourceIdentifier);

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
        var successMessage = _faker.Random.AlphaNumeric(40);
        var resourceIdentifier = _faker.Internet.Url();
        
        var state = new SuccessState(successMessage, resourceIdentifier);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Succeeded);
        ticketStatus.ProcessingMessage.Should().Be(successMessage);
        ticketStatus.DateLastProgressed.Should().BeNull();
        ticketStatus.ResourceIdentifier.Should().Be(resourceIdentifier);
        ticketStatus.ValidationErrors.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenSuccess_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var successMessage = _faker.Random.AlphaNumeric(40);
        var resourceIdentifier = _faker.Internet.Url();
        
        var state = new SuccessState(successMessage, resourceIdentifier);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(ticketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenValidationFailureTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var validationMessage = _faker.Random.AlphaNumeric(40);
        var validationErrors = CreateValidationErrors();
        
        var state = new ValidationState(validationMessage, validationErrors);

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
        var validationMessage = _faker.Random.AlphaNumeric(40);
        var validationErrors = CreateValidationErrors();

        var state = new ValidationState(validationMessage, validationErrors);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Failed);
        ticketStatus.ProcessingMessage.Should().Be(validationMessage);
        ticketStatus.DateLastProgressed.Should().BeNull();
        ticketStatus.ValidationErrors.Should().BeEquivalentTo(validationErrors);
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ErrorCode.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenValidationFailure_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var validationMessage = _faker.Random.AlphaNumeric(40);
        var validationErrors = CreateValidationErrors();

        var state = new ValidationState(validationMessage, validationErrors);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(ticketStatus), Times.Once);
    }

    [Test]
    public async Task CloseTicketAsync_WhenErrorFailureTicketDoesNotExist_ThenThrowException()
    {
        // Arrange
        var trackingNumber = CombGuid.NewGuid();
        var errorMessage = _faker.Random.AlphaNumeric(40);
        var errorCode = _faker.Random.AlphaNumeric(5);
        
        var state = new ErrorState(errorMessage, errorCode);

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
        var errorMessage = _faker.Random.AlphaNumeric(40);
        var errorCode = _faker.Random.AlphaNumeric(5);

        var state = new ErrorState(errorMessage, errorCode);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        ticketStatus.DateClosed.Should().BeCloseTo(DateTime.UtcNow, 500.Milliseconds());
        ticketStatus.ProcessingStage.Should().Be(ProcessingStage.Failed);
        ticketStatus.ProcessingMessage.Should().Be(errorMessage);
        ticketStatus.DateLastProgressed.Should().BeNull();
        ticketStatus.ErrorCode.Should().Be(errorCode);
        ticketStatus.ResourceIdentifier.Should().BeNull();
        ticketStatus.ValidationErrors.Should().BeNull();
    }

    [Test]
    public async Task CloseTicketAsync_WhenErrorFailure_ThenStoresTicketStatus()
    {
        // Arrange
        var startingTicketStatus = CreateTicketStatus();
        var errorMessage = _faker.Random.AlphaNumeric(40);
        var errorCode = _faker.Random.AlphaNumeric(5);

        var state = new ErrorState(errorMessage, errorCode);

        _ticketTrackingStore.Setup(tts => tts.GetTicketStatusAsync(startingTicketStatus.TrackingNumber))
                            .ReturnsAsync(startingTicketStatus);

        // Act
        var ticketStatus = await _ticketTracker.CloseTicketAsync(startingTicketStatus.TrackingNumber, state);

        // Assert
        _ticketTrackingStore.Verify(tts => tts.SetTicketStatusAsync(ticketStatus), Times.Once);
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
