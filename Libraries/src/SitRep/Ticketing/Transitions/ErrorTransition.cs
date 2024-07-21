namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Transitions a ticket to a completed and failed state, as the result of an error.
/// </summary>
/// <param name="errorMessage">A message describing the error.</param>
/// <param name="errorCode">
/// A code that can be used to identify the type of error. Can be used by clients as a key to implement conditional flow.
/// </param>
public class ErrorTransition(string errorMessage, string? errorCode) : ITransitionTicketState
{
    public Ticket TransitionState(Ticket ticket) => ticket with
                                                    {
                                                        DateClosed = DateTime.UtcNow,
                                                        ProcessingState = ProcessingState.Failed,
                                                        ProcessingMessage = errorMessage,
                                                        ErrorCode = errorCode,
                                                        ResourceIdentifier = null,
                                                        ValidationErrors = null
                                                    };
}