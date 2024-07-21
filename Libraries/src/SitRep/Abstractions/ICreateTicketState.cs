namespace SitRep.Abstractions;

/// <summary>
/// When implemented, creates the desired state of a new ticket.
/// </summary>
public interface ICreateTicketState
{
    /// <summary>
    /// Creates a new ticket in the desired initial state.
    /// </summary>
    /// <returns>A new ticket.</returns>
    Ticket CreateState();
}