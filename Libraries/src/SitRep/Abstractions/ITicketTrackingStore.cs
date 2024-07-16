namespace SitRep.Abstractions;

/// <summary>
/// Represents a persistent store for tracking the status of tickets.
/// </summary>
public interface ITicketTrackingStore
{
    /// <summary>
    /// Gets the status of a ticket with the specified tracking number.
    /// </summary>
    /// <param name="trackingNumber">The tacking number that identifies the ticket.</param>
    /// <returns>The status of the ticket if available; otherwise null.</returns>
    Task<TicketStatus?> GetTicketStatusAsync(Guid trackingNumber);

    /// <summary>
    /// Gets the status of all tickets that were issued to the specified origin.
    /// </summary>
    /// <param name="issuedTo">
    /// A unique identifier that can be used to filter tickets by who they were originally issued to.
    /// </param>
    /// <returns>A list fo statuses associated with the specified </returns>
    Task<IEnumerable<TicketStatus>> GetTicketStatusesAsync(string issuedTo);

    /// <summary>
    /// Updates the status of an existing ticket, or creates a new entry if one doesn't already exist.
    /// The tracking number within the status is used to identify the ticket.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <returns></returns>
    Task SetTicketStatusAsync(TicketStatus status);
}
