namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record GetTicketRequest([FromRoute] Guid TrackingNumber);