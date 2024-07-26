namespace SitRep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents the response to an error.
/// </summary>
/// <param name="TrackingNumber">The tracking number of the ticket that the error occurred against.</param>
/// <param name="Message">A message that describes the error.</param>
public record ErrorMessageResponse(Guid TrackingNumber, string Message);