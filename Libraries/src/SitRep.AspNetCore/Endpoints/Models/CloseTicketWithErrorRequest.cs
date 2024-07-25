namespace SitRep.AspNetCore.Endpoints.Models;

public record CloseTicketWithErrorRequest([FromRoute] Guid TrackingNumber, 
                                          [FromBody] CloseTicketWithErrorRequestBody Body)
{
    public ErrorTransition ToTransitionState() => new(Body.Message, Body.ErrorCode);
}

public record CloseTicketWithErrorRequestBody(string Message, string? ErrorCode = null);
