namespace Sitrep.Ticketing;

/// <summary>
/// A persistent store for tracking tickets, where items are stored in memory.
/// <para>
/// Values will not be persisted between application restarts, or across process boundaries.
/// </para>
/// <para>
/// Should only be used for testing or basic experiments.
/// </para>
/// </summary>
public class InMemoryTicketStore(IOptions<InMemoryTicketStoreOptions> options) : ITicketStore
{
    private readonly object _syncLock = new();
    private readonly ConcurrentDictionary<Guid, Ticket> _tickets = [];
    private int _interval = 0;

    /// <inheritdoc />
    public void Initialize()
    {
        // No initialization required
    }

    /// <inheritdoc />
    public Task<Ticket?> GetTicketAsync(Guid trackingNumber)
    {
        var result = _tickets.TryGetValue(trackingNumber, out var ticket);

        return Task.FromResult(result ? ticket : null);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Ticket>> GetTicketsAsync(string issuedTo)
    {
        // Force immediate LINQ evaluation
        var tickets = _tickets.Values.Where(ts => ts.IssuedTo == issuedTo).ToList();

        return Task.FromResult(tickets.AsEnumerable());
    }

    /// <inheritdoc />
    public Task StoreTicketAsync(Ticket ticket)
    {
        _tickets.AddOrUpdate(ticket.TrackingNumber, ticket, (_, _) => ticket);

        lock (_syncLock)
        {
            if (ShouldReleaseSpace())
            {
                ReleaseSpace();
            }
        }

        return Task.CompletedTask;
    }

    private InMemoryTicketStoreOptions Options { get; } = options.Value;

    private bool ShouldReleaseSpace()
    {
        // Assumes this is executed within a lock

        _interval++;

        if (_interval % Options.DiscardInterval != 0)
        {
            return false;
        }

        _interval = 0;

        return true;
    }

    private void ReleaseSpace()
    {
        // Assumes this is executed within a lock

        if (Options.DiscardExpired)
        {
            RemoveTickets(_tickets.Values.Where(ticket => ticket.IsClosed && ticket.ExpirationDate < DateTime.UtcNow));
        }

        if (_tickets.Count > Options.DiscardThreshold)
        {
            RemoveTickets(_tickets.Values
                                  .Where(ticket => ticket.IsClosed)
                                  .OrderBy(ticket => ticket.DateIssued)
                                  .Take(_tickets.Count - Options.DiscardThreshold + Options.DiscardCount));
        }
    }

    private void RemoveTickets(IEnumerable<Ticket> tickets)
    {
        // Assumes this is executed within a lock

        foreach (var ticket in tickets)
        {
            _tickets.TryRemove(ticket.TrackingNumber, out _);
        }
    }
}