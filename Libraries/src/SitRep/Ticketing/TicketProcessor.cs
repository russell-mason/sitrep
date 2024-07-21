namespace SitRep.Ticketing;

/// <summary>
/// Handles the creation, and updating of tickets, allowing the ticket to reflect the progress of the underlying process.
/// </summary>
/// <param name="ticketStore">The store that provides persistence for tickets.</param>
public class TicketProcessor(ITicketStore ticketStore) : ITicketProcessor
{
    public async Task<Ticket> CreateTicketAsync(ICreateTicketState creator)
    {
        var ticket = creator.CreateState();

        await SaveAsync(ticket);

        return ticket;
    }

    public async Task<Ticket> TransitionTicketAsync(Guid trackingNumber, ITransitionTicketState transition)
    {
        var ticket = await LoadAsync(trackingNumber) ?? throw new TrackingNumberNotFoundException(trackingNumber);

        var updatedTicket= transition.TransitionState(ticket);

        await SaveAsync(updatedTicket);

        return updatedTicket;
    }

    /// <summary>
    /// Allows derived implementations to customise the loading process.
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <returns></returns>
    protected virtual Task<Ticket?> LoadAsync(Guid trackingNumber) => ticketStore.GetTicketAsync(trackingNumber);

    /// <summary>
    /// Allows derived implementations to customise the saving process.
    /// </summary>
    /// <param name="ticketStatus"></param>
    /// <returns></returns>
    protected virtual Task SaveAsync(Ticket ticketStatus) => ticketStore.StoreTicketAsync(ticketStatus);
}
