namespace Sitrep.SignalR.Ticketing;

/// <summary>
/// Provides SignalR-based ticket change notifications.
/// </summary>
/// <param name="ticketHub">The SignalR hub to use to send notifications through.</param>
public class SignalRTicketNotification(IHubContext<SignalRTicketHub> ticketHub) : ITicketNotification
{
    /// <inheritdoc/>
    public virtual async Task NotifyAsync(OpenEvent openEvent)
    {
        await ticketHub.Clients
                       .User(openEvent.Ticket.IssuedTo)
                       .SendAsync(openEvent.Action, openEvent.Ticket);
    }

    /// <inheritdoc/>
    public virtual async Task NotifyAsync(TransitionEvent transitionEvent)
    {
        await ticketHub.Clients
                       .User(transitionEvent.PostTransitionTicket.IssuedTo)
                       .SendAsync(transitionEvent.Action, transitionEvent.PreTransitionTicket, transitionEvent.PostTransitionTicket);
    }
}
