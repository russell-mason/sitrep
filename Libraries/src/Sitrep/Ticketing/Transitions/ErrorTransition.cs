namespace Sitrep.Ticketing.Transitions;

/// <summary>
/// Provides state that can be used to transition a ticket to a completed and failed state, as the result of an error.
/// </summary>
/// <param name="errorMessage">A message describing the error.</param>
/// <param name="errorCode">
/// A code that can be used to identify the type of error. Can be used by clients as a key to implement conditional flow.
/// </param>
public class ErrorTransition(string errorMessage, string? errorCode) : ITransitionTicketState
{
    /// <summary>
    /// Allows a processor to create a new ticket in the desired state.
    /// </summary>
    /// <param name="ticket">The ticket to transition.</param>
    /// <returns>A new ticket in the desired state.</returns>
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