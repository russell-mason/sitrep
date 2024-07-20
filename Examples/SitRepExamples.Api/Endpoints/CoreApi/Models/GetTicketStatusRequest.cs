namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record GetTicketStatusRequest([FromRoute] Guid TrackingNumber);
