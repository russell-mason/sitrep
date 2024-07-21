namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record GetTicketsRequest([FromQuery] string? IssuedTo);