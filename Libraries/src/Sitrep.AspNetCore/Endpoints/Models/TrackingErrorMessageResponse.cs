namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents the response to an error where the tracking number is available.
/// </summary>
/// <param name="TrackingNumber">The tracking number of the ticket that the error occurred against.</param>
/// <param name="Message">A message that describes the error.</param>
public record TrackingErrorMessageResponse(Guid TrackingNumber, string Message);