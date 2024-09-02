namespace Sitrep.ApiClient.Ticketing;

/// <summary>
/// Represents a list of tickets associated with a user.
/// </summary>
public class TicketList
{
    /// <summary>
    /// Gets or sets a list of tickets.
    /// </summary>
    public IEnumerable<Ticket> Tickets { get; set; } = [];
}
