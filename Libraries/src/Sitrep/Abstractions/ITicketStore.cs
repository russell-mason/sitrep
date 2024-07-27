namespace Sitrep.Abstractions;

/// <summary>
/// Represents a persistent store for tracking tickets.
/// </summary>
public interface ITicketStore
{
    /// <summary>
    /// Allows the ticket store to perform any necessary initialization, such as creating indexes.
    /// This is done automatically when registered using AspNetCore extensions; otherwise must be called
    /// manually after the store is created.
    /// </summary>
    public void Initialize();

    /// <summary>
    /// Gets the ticket with the specified tracking number.
    /// </summary>
    /// <param name="trackingNumber">A tacking number that identifies the ticket.</param>
    /// <returns>The ticket if available; otherwise null.</returns>
    Task<Ticket?> GetTicketAsync(Guid trackingNumber);

    /// <summary>
    /// Gets tickets that were issued to the specified origin.
    /// </summary>
    /// <param name="issuedTo">
    /// A unique identifier that can be used to filter tickets by who they were originally issued to.
    /// </param>
    /// <returns>A list of tickets associated with the specified origin.</returns>
    Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo);

    /// <summary>
    /// Updates an existing ticket, or creates a new entry if one doesn't already exist.
    /// The tracking number within the ticket is used to identify the existing entry.
    /// </summary>
    /// <param name="ticket">The desired state of the ticket.</param>
    /// <returns></returns>
    Task StoreTicketAsync(Ticket ticket);
}