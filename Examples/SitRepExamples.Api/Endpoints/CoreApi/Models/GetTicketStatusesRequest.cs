namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record GetTicketStatusesRequest([FromQuery] string? IssuedTo);
