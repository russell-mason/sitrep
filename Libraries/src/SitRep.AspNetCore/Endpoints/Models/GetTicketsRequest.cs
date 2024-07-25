namespace SitRep.AspNetCore.Endpoints.Models;

public record GetTicketsRequest([FromQuery] string? IssuedTo);