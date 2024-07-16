namespace SitRep.Abstractions;

/// <summary>
/// Provides methods that allow a ticket to reflect the progress of the underlying process.
/// </summary>
public interface ITicketTracker
{
    /// <summary>
    /// Opens a new ticket and sets the current state to Pending.
    /// </summary>
    /// <param name="state">The state to transition to.</param>
    /// <returns>The updated status of the ticket.</returns>
    Task<TicketStatus> OpenTicketAsync(StartingState state);

    /// <summary>
    /// Updates an existing ticket to be In Progress. This can be called multiple times with different messages
    /// to reflect a process that carries out multiple actions as it progresses.
    /// </summary>
    /// <param name="trackingNumber">A tracking number that can be used to identify the ticket.</param>
    /// <param name="state">The state to transition to.</param>
    /// <returns>The updated status of the ticket.</returns>
    Task<TicketStatus> ProgressTicketAsync(Guid trackingNumber, ProgressState state);

    /// <summary>
    /// Closes an existing ticket and sets the state to Succeeded.
    /// </summary>
    /// <param name="trackingNumber">A tracking number that can be used to identify the ticket.</param>
    /// <param name="state">The state to transition to.</param>
    /// <returns>The updated status of the ticket.</returns>
    Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, SuccessState state);

    /// <summary>
    /// Closes an existing ticket and sets the state to Failed due to a validation error.
    /// </summary>
    /// <param name="trackingNumber">A tracking number that can be used to identify the ticket.</param>
    /// <param name="state">The state to transition to.</param>
    /// <returns>The updated status of the ticket.</returns>
    Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, ValidationState state);

    /// <summary>
    /// Closes an existing ticket and sets the state to Failed due to an error.
    /// </summary>
    /// <param name="trackingNumber">A tracking number that can be used to identify the ticket.</param>
    /// <param name="state">The state to transition to.</param>
    /// <returns>The updated status of the ticket.</returns>
    Task<TicketStatus> CloseTicketAsync(Guid trackingNumber, FailureState state);
}
