namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents the response to getting a list of tickets.
/// </summary>
public record GetTicketsResponse
{
    /// <summary>
    /// Create a new instance of the GetTicketsResponse class.
    /// </summary>
    /// <param name="tickets">A list of tickets to include in the response.</param>
    public GetTicketsResponse(IEnumerable<Ticket> tickets)
    {
        Tickets = tickets.Select(ticket => new TicketResponse(ticket));
    }

    /// <summary>
    /// Gets the list of tickets.
    /// </summary>
    public IEnumerable<TicketResponse> Tickets { get; }
}