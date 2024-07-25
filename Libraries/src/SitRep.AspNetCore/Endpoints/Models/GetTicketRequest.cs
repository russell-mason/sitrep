namespace SitRep.AspNetCore.Endpoints.Models;

public record GetTicketRequest([FromRoute] Guid TrackingNumber);