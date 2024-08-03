namespace Sitrep.Ticketing.Events;

/// <summary>
/// Represents an event that's raised when a ticket is transitioned from one state to another.
/// </summary>
/// <param name="action">A key-based description that indicates the action the transition represents.</param>
/// <param name="preTransition">The state of the ticket before the transition was applied.</param>
/// <param name="postTransition">The state of the ticket after the transition was applied.</param>
public class TransitionEvent(string action, Ticket preTransition, Ticket postTransition)
{
    /// <summary>
    /// Gets a key-based description that indicates the action the transition represents.
    /// <para>
    /// Keys:<br/>
    /// ticket:transition:progress<br/>
    /// ticket:transition:success<br/>
    /// ticket:transition:validation-error<br/>
    /// ticket:transition:error
    /// </para>
    /// </summary>
    public string Action => action;

    /// <summary>
    /// Gets the state of the ticket before the transition was applied.
    /// </summary>
    public Ticket PreTransitionTicket => preTransition;

    /// <summary>
    /// Gets the state of the ticket after the transition was applied.
    /// </summary>
    public Ticket PostTransitionTicket => postTransition;
}
