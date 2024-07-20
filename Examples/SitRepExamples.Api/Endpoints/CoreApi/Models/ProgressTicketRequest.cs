namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record ProgressTicketRequest([FromRoute] Guid TrackingNumber, 
                                    [FromBody] ProgressTicketRequestBody Body)
{
    public ProgressState ToProgressState() => new(Body.Message);
}

public record ProgressTicketRequestBody(string Message);
