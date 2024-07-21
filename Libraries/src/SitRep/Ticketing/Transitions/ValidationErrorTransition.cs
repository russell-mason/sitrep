namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Transitions a ticket to a completed and failed state, as the result of validation errors.
/// </summary>
/// <param name="validationMessage">A message describing the validation source.</param>
/// <param name="validationErrors">
/// A list of validation errors.
/// The key should be a property name, and the values should be the validation errors associated with that property.
/// </param>
public class ValidationErrorTransition(string validationMessage, ValidationErrorDictionary validationErrors) : ITransitionTicketState
{
    public Ticket TransitionState(Ticket ticket) => ticket with
                                                    {
                                                        DateClosed = DateTime.UtcNow,
                                                        ProcessingState = ProcessingState.Failed,
                                                        ProcessingMessage = validationMessage,
                                                        // Because the dictionary is mutable, we need to copy it to prevent reference issues.
                                                        // However, because it only contains strings, we can use a shallow copy.
                                                        ValidationErrors = new ValidationErrorDictionary(validationErrors),
                                                        ResourceIdentifier = null,
                                                        ErrorCode = null
                                                    };
}