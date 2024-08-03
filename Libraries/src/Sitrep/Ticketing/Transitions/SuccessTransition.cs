namespace Sitrep.Ticketing.Transitions;

/// <summary>
/// Provides state that can be used to transition a ticket to a completed state, as the result of success.
/// </summary>
/// <param name="successMessage">A message describing the final state.</param>
/// <param name="resourceIdentifier">
/// A URL to the resource that was created. If no resource was produced, this should be null.
/// </param>
public class SuccessTransition(string successMessage, string? resourceIdentifier = null) : ITransitionTicketState
{
    /// <summary>
    /// Gets a key-based description that indicates the action this transition represents.
    /// </summary>
    public string Action => "ticket:transition:success";

    /// <summary>
    /// Allows a processor to create a new ticket in the desired state.
    /// </summary>
    /// <param name="ticket">The ticket to transition.</param>
    /// <returns>A new ticket in the desired state.</returns>
    public Ticket TransitionState(Ticket ticket) => ticket with
                                                    {
                                                        DateClosed = DateTime.UtcNow,
                                                        ProcessingState = ProcessingState.Succeeded,
                                                        ProcessingMessage = successMessage,
                                                        ResourceIdentifier = resourceIdentifier,
                                                        ValidationErrors = null,
                                                        ErrorCode = null
                                                    };
}