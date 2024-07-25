namespace SitRep.AspNetCore.Endpoints.Models;

public record GetTicketsResponse
{
    public GetTicketsResponse(IEnumerable<Ticket> tickets)
    {
        Tickets = tickets.Select(ticket => new TicketResponse(ticket));
    }

    public IEnumerable<TicketResponse> Tickets { get; }
}