namespace Sitrep.Abstractions;

/// <summary>
/// When implemented, creates the desired state for a new ticket.
/// </summary>
public interface ICreateTicketState
{
    /// <summary>
    /// Gets a key-based description that indicates the action this state represents.
    /// </summary>
    public string Action { get; }

    /// <summary>
    /// Creates a new ticket in the desired initial state.
    /// </summary>
    /// <returns>A new ticket.</returns>
    Ticket CreateState();
}