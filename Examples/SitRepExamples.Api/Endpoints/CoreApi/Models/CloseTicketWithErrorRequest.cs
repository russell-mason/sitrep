namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record CloseTicketWithErrorRequest([FromRoute] Guid TrackingNumber, 
                                          [FromBody] CloseTicketWithErrorRequestBody Body)
{
    public ErrorState ToErrorState() => new(Body.Message, Body.ErrorCode);
}

public record CloseTicketWithErrorRequestBody(string Message, string? ErrorCode = null);
