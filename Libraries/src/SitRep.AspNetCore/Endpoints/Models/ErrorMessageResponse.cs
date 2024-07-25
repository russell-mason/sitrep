namespace SitRep.AspNetCore.Endpoints.Models;

public record ErrorMessageResponse(Guid TrackingNumber, string Message);