namespace SitRep.Abstractions;

/// <summary>
/// When implemented, creates the desired change in ticket state.
/// </summary>
public interface ITransitionTicketState
{
    /// <summary>
    /// Takes an existing ticket and creates the desired change in state.
    /// </summary>
    /// <param name="ticket">An existing ticket.</param>
    /// <returns>The desired state of the ticket.</returns>
    Ticket TransitionState(Ticket ticket);
}