namespace SitRep.AspNetCore.Endpoints.Models;

public record CloseTicketWithValidationErrorsRequest([FromRoute] Guid TrackingNumber, 
                                                     [FromBody] CloseTicketWithValidationErrorsRequestBody Body)
{
    public ValidationErrorTransition ToTransitionState() => new(Body.Message, Body.Errors);
}

public record CloseTicketWithValidationErrorsRequestBody(string Message, ValidationErrorDictionary Errors);
