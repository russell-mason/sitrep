namespace SitRep.AspNetCore.Endpoints.Models;

public record CloseTicketWithSuccessRequest([FromRoute] Guid TrackingNumber, 
                                            [FromBody] CloseTicketWithSuccessRequestBody Body)
{
    public SuccessTransition ToTransitionState() => new(Body.Message, Body.ResourceIdentifier);
}

public record CloseTicketWithSuccessRequestBody(string Message, string? ResourceIdentifier = null);
