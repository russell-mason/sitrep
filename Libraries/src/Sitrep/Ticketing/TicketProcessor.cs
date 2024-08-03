namespace Sitrep.Ticketing;

/// <summary>
/// Handles the creation, and updating of tickets, allowing the ticket to reflect the progress of the underlying process.
/// </summary>
/// <param name="ticketStore">The store that provides persistence for tickets.</param>
/// <param name="ticketNotification">The mechanism to use for ticket change notification.</param>
public class TicketProcessor(ITicketStore ticketStore, ITicketNotification ticketNotification) : ITicketProcessor
{
    /// <inheritdoc />
    public virtual async Task<Ticket> CreateTicketAsync(ICreateTicketState creator)
    {
        var ticket = creator.CreateState();

        await SaveAsync(ticket);

        var createdEvent = new CreatedEvent(creator.Action, ticket);
        
        await ticketNotification.NotifyAsync(createdEvent);

        return ticket;
    }

    /// <inheritdoc />
    public virtual async Task<Ticket> TransitionTicketAsync(Guid trackingNumber, ITransitionTicketState transition)
    {
        var ticket = await LoadAsync(trackingNumber) ?? throw new TrackingNumberNotFoundException(trackingNumber);

        var updatedTicket = transition.TransitionState(ticket);

        await SaveAsync(updatedTicket);

        var transitionEvent = new TransitionEvent(transition.Action, ticket, updatedTicket);

        await ticketNotification.NotifyAsync(transitionEvent);

        return updatedTicket;
    }

    /// <summary>
    /// Allows derived implementations to customize the loading process.
    /// </summary>
    /// <param name="trackingNumber"></param>
    /// <returns></returns>
    protected virtual Task<Ticket?> LoadAsync(Guid trackingNumber) => ticketStore.GetTicketAsync(trackingNumber);

    /// <summary>
    /// Allows derived implementations to customize the saving process.
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    protected virtual Task SaveAsync(Ticket ticket) => ticketStore.StoreTicketAsync(ticket);
}