namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Provides state that can be used to transition a ticket to an in-progress state.
/// </summary>
/// <param name="progressMessage">A message describing the progress.</param>
public class ProgressTransition(string progressMessage) : ITransitionTicketState
{
    /// <summary>
    /// Allows a processor to create a new ticket in the desired state.
    /// </summary>
    /// <param name="ticket">The ticket to transition.</param>
    /// <returns>A new ticket in the desired state.</returns>
    public Ticket TransitionState(Ticket ticket) => ticket with
                                                    {
                                                        ProcessingState = ProcessingState.InProgress,
                                                        ProcessingMessage = progressMessage,
                                                        DateLastProgressed = DateTime.UtcNow,
                                                        DateClosed = null,
                                                        ResourceIdentifier = null,
                                                        ValidationErrors = null,
                                                        ErrorCode = null
                                                    };
}