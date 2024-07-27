namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to get a single ticket.
/// </summary>
/// <param name="TrackingNumber">The ticket's tracking number.</param>
public record GetTicketRequest([FromRoute] Guid TrackingNumber);