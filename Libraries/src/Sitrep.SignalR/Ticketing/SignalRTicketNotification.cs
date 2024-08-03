namespace Sitrep.SignalR.Ticketing;

/// <summary>
/// Provides SignalR-based ticket change notifications.
/// </summary>
/// <param name="ticketHub">The SignalR hub to use to send notifications through.</param>
public class SignalRTicketNotification(IHubContext<SignalRTicketHub> ticketHub) : ITicketNotification
{
    /// <inheritdoc/>
    public virtual async Task NotifyAsync(CreatedEvent createdEvent)
    {
        await ticketHub.Clients
                       .User(createdEvent.Ticket.IssuedTo)
                       .SendAsync(createdEvent.Action, createdEvent.Ticket);
    }

    /// <inheritdoc/>
    public virtual async Task NotifyAsync(TransitionEvent transitionEvent)
    {
        await ticketHub.Clients
                       .User(transitionEvent.PostTransitionTicket.IssuedTo)
                       .SendAsync(transitionEvent.Action, transitionEvent.PreTransitionTicket, transitionEvent.PostTransitionTicket);
    }
}
