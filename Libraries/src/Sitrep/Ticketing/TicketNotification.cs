namespace Sitrep.Ticketing;

/// <summary>
/// A default implementation of an ITicketNotification that does nothing.
/// </summary>
public class TicketNotification : ITicketNotification
{
    /// <inheritdoc/>
    public Task NotifyAsync(CreatedEvent createdEvent)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task NotifyAsync(TransitionEvent transitionEvent)
    {
        return Task.CompletedTask;
    }
}
