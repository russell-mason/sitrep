namespace Sitrep.Ticketing.Events;

/// <summary>
/// Represents an event that's raised when a ticket is created.
/// </summary>
/// <param name="action">A key-based description that indicates the action the state represents.</param>
/// <param name="ticket">The ticket that was created.</param>
public class CreatedEvent(string action, Ticket ticket)
{
    /// <summary>
    /// Gets a key-based description that indicates the action the state represents.
    /// <para>
    /// Keys:<br/>
    /// ticket:create:open
    /// </para>
    /// </summary>
    public string Action => action;

    /// <summary>
    /// Gets the ticket that was created.
    /// </summary>
    public Ticket Ticket => ticket;
}
