namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Transitions a ticket to a completed state, as the result of success.
/// Specifies details when a ticket is transitioning to a completed successful state.
/// </summary>
/// <param name="successMessage">A message describing the final state.</param>
/// <param name="resourceIdentifier">
/// A URL to the resource that was created. If no resource was produced, this should be null.
/// </param>
public class SuccessTransition(string successMessage, string? resourceIdentifier) : ITransitionTicketState
{
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