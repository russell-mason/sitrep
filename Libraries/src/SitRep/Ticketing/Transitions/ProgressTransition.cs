namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Transitions a ticket to an in-progress state.
/// </summary>
/// <param name="progressMessage">A message describing the progress.</param>
public class ProgressTransition(string progressMessage) : ITransitionTicketState
{
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