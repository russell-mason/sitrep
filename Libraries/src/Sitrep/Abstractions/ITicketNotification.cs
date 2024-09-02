namespace Sitrep.Abstractions;

/// <summary>
/// Provides a mechanism for clients to be notified of ticket state changes.
/// </summary>
public interface ITicketNotification
{
    /// <summary>
    /// Notifies the user of details relating to a ticket creation event.
    /// </summary>
    /// <param name="openEvent">The event capturing the ticket details.</param>
    /// <returns></returns>
    Task NotifyAsync(OpenEvent openEvent);

    /// <summary>
    /// Notifies the user of details relating to a ticket state transition event.
    /// </summary>
    /// <param name="transitionEvent">The event capturing the ticket details.</param>
    /// <returns></returns>
    Task NotifyAsync(TransitionEvent transitionEvent);
}
