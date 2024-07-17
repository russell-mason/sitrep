namespace SitRep.Tracking;

/// <summary>
/// A persistent store for tracking the status of tickets, where items are stored in memory.
/// Values will not be persisted between application restarts, or across process boundaries.
/// Should only be used for testing or basic experiments.
/// </summary>
public class InMemoryTicketTrackingStore : ITicketTrackingStore
{
   private readonly ConcurrentDictionary<Guid, TicketStatus> _ticketStatuses = [];

    public Task<TicketStatus?> GetTicketStatusAsync(Guid trackingNumber)
    {
        var result = _ticketStatuses.TryGetValue(trackingNumber, out var ticketStatus);

        return Task.FromResult(result ? ticketStatus : null);
    }

    public Task<IEnumerable<TicketStatus>> GetTicketStatusesAsync(string issuedTo)
    {
        // Force immediate LINQ evaluation
        var statuses = _ticketStatuses.Values.Where(ts => ts.IssuedTo == issuedTo).ToList();

        return Task.FromResult(statuses.AsEnumerable());
    }

    public Task SetTicketStatusAsync(TicketStatus ticketStatus)
    {
        _ticketStatuses.AddOrUpdate(ticketStatus.TrackingNumber, ticketStatus, (_, _) => ticketStatus);

        return Task.CompletedTask;
    }
}
