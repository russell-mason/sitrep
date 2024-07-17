namespace SitRep.Tracking;

/// <summary>
/// Provides methods that allow a ticket to reflect the progress of the underlying process.
/// </summary>
/// <param name="ticketTrackingStore">The store that provides persistence for the status of tickets.</param>
public class TicketTracker(ITicketTrackingStore ticketTrackingStore) : ITicketTracker
{
    public async Task<TicketStatus> OpenTicketAsync(StartingState state)
    {
        var ticketStatus = new TicketStatus(state.TrackingNumber,
                                            state.IssuedTo,
                                            state.IssuedOnBehalfOf,
                                            state.ReasonForIssuing);

        await Save(ticketStatus);

        return ticketStatus;
    }

    public async Task<TicketStatus> ProgressTicketAsync(Guid trackingNumber, ProgressState state)
    {
        var ticketStatus = await ticketTrackingStore.GetTicketStatusAsync(trackingNumber)
                           ?? throw new TrackingNumberNotFoundException(trackingNumber, state.ProgressMessage);

        var updatedTicketStatus = ticketStatus with
        {
            ProcessingStage = ProcessingStage.InProgress,
            ProcessingMessage = state.ProgressMessage,
            DateClosed = null,
            ResourceIdentifier = null,
            ValidationErrors = null,
            ErrorCode = null
        };

        await Save(ticketStatus);

        return updatedTicketStatus;
    }

    public async Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, SuccessState state)
    {
        var ticketStatus = await ticketTrackingStore.GetTicketStatusAsync(trackingNumber)
                           ?? throw new TrackingNumberNotFoundException(trackingNumber, state.SuccessMessage);

        var updatedTicketStatus = ticketStatus with
        {
            DateClosed = DateTime.UtcNow,
            ProcessingStage = ProcessingStage.Succeeded,
            ProcessingMessage = state.SuccessMessage,
            ResourceIdentifier = state.ResourceIdentifier,
            ValidationErrors = null,
            ErrorCode = null
        };

        await Save(ticketStatus);

        return updatedTicketStatus;
    }

    public async Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, ValidationState state)
    {
        var ticketStatus = await ticketTrackingStore.GetTicketStatusAsync(trackingNumber)
                           ?? throw new TrackingNumberNotFoundException(trackingNumber, state.ValidationMessage);

        var updatedTicketStatus = ticketStatus with
        {
            DateClosed = DateTime.UtcNow,
            ProcessingStage = ProcessingStage.Failed,
            ProcessingMessage = state.ValidationMessage,
            ValidationErrors = Copy(state.ValidationErrors),  // Copy to prevent reference issues
            ResourceIdentifier = null,
            ErrorCode = null
        };

        await Save(ticketStatus);

        return updatedTicketStatus;
    }

    public async Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, FailureState state)
    {
        var ticketStatus = await ticketTrackingStore.GetTicketStatusAsync(trackingNumber)
                           ?? throw new TrackingNumberNotFoundException(trackingNumber, state.ErrorMessage);

        var updatedTicketStatus = ticketStatus with
        {
            DateClosed = DateTime.UtcNow,
            ProcessingStage = ProcessingStage.Failed,
            ProcessingMessage = state.ErrorMessage,
            ErrorCode = state.ErrorCode,
            ResourceIdentifier = null,
            ValidationErrors = null
        };

        await Save(ticketStatus);

        return updatedTicketStatus;
    }

    protected virtual async Task Save(TicketStatus ticketStatus)
    {
        await ticketTrackingStore.SetTicketStatusAsync(ticketStatus);
    }

    // Because the dictionary is mutable, we need to copy it to prevent reference issues. However, because
    // it only contains strings, we can use a shallow copy.
    private static Dictionary<string, string[]> Copy(Dictionary<string, string[]> values) => new(values);
}

