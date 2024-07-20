namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record CloseTicketWithValidationErrorsRequest([FromRoute] Guid TrackingNumber, 
                                                     [FromBody] CloseTicketWithValidationErrorsRequestBody Body)
{
    public ValidationState ToValidationState() => new(Body.Message, Body.Errors);
}

public record CloseTicketWithValidationErrorsRequestBody(string Message, ValidationErrorDictionary Errors);
