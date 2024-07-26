namespace SitRep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to get tickets issued to a specific source.
/// </summary>
/// <param name="IssuedTo">Who the ticket was issued to.</param>
public record GetTicketsRequest([FromQuery] string? IssuedTo);