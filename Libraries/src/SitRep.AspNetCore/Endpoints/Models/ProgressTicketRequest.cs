namespace SitRep.AspNetCore.Endpoints.Models;

public record ProgressTicketRequest([FromRoute] Guid TrackingNumber, 
                                    [FromBody] ProgressTicketRequestBody Body)
{
    public ProgressTransition ToTransitionState() => new(Body.Message);
}

public record ProgressTicketRequestBody(string Message);
