namespace SitRep.Abstractions;

/// <summary>
/// Handles the creation, and updating of tickets, allowing the ticket to reflect the progress of the underlying process.
/// </summary>
public interface ITicketProcessor
{
    /// <summary>
    /// Creates, and stores a new ticket using the specified ticket creator.
    /// </summary>
    /// <param name="creator">The implementation that creates the ticket.</param>
    /// <returns>The newly created ticket.</returns>
    Task<Ticket> CreateTicketAsync(ICreateTicketState creator);

    /// <summary>
    /// Updates the state of an existing ticket using the specified ticket transition.
    /// </summary>
    /// <param name="trackingNumber">A tracking number that identifies an existing ticket.</param>
    /// <param name="transition">The implementation that transitions the ticket state.</param>
    /// <returns></returns>
    Task<Ticket> TransitionTicketAsync(Guid trackingNumber, ITransitionTicketState transition);
}