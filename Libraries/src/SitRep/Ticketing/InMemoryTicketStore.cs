namespace SitRep.Ticketing;

/// <summary>
/// A persistent store for tracking tickets, where items are stored in memory.
/// <para>
/// Values will not be persisted between application restarts, or across process boundaries.
/// </para>
/// <para>
/// Should only be used for testing or basic experiments.
/// </para>
/// </summary>
public class InMemoryTicketStore : ITicketStore
{
    private readonly ConcurrentDictionary<Guid, Ticket> _ticketStatuses = [];

    public Task<Ticket?> GetTicketAsync(Guid trackingNumber)
    {
        var result = _ticketStatuses.TryGetValue(trackingNumber, out var ticket);

        return Task.FromResult(result ? ticket : null);
    }

    public Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo)
    {
        // Force immediate LINQ evaluation
        var tickets = _ticketStatuses.Values.Where(ts => ts.IssuedTo == issuedTo).ToList();

        return Task.FromResult(tickets.AsEnumerable());
    }

    public Task StoreTicketAsync(Ticket ticket)
    {
        _ticketStatuses.AddOrUpdate(ticket.TrackingNumber, ticket, (_, _) => ticket);

        return Task.CompletedTask;
    }
}