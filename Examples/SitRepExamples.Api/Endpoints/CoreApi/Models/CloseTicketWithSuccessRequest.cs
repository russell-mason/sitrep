namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record CloseTicketWithSuccessRequest([FromRoute] Guid TrackingNumber, 
                                            [FromBody] CloseTicketWithSuccessRequestBody Body)
{
    public SuccessState ToSuccessState() => new(Body.Message, Body.ResourceIdentifier);
}

public record CloseTicketWithSuccessRequestBody(string Message, string? ResourceIdentifier = null);
