namespace SitRep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to close a ticket due to validation errors.
/// </summary>
/// <param name="TrackingNumber">The ticket's tracking number.</param>
/// <param name="Body">The request body containing additional details.</param>
public record CloseTicketWithValidationErrorsRequest([FromRoute] Guid TrackingNumber, 
                                                     [FromBody] CloseTicketWithValidationErrorsRequestBody Body)
{
    /// <summary>
    /// Converts the request to a transition state.
    /// </summary>
    /// <returns>The transition state.</returns>
    public ValidationErrorTransition ToTransitionState() => new(Body.Message, Body.Errors);
}

/// <summary>
/// Represents the body of a request to close a ticket due to validation errors.
/// </summary>
/// <param name="Message">A message that describes why the ticket was closed.</param>
/// <param name="Errors">
/// A set of validation errors where the key is a property name, and the values are the validation errors
/// associated with that property.
/// </param>
public record CloseTicketWithValidationErrorsRequestBody(string Message, ValidationErrorDictionary Errors);
