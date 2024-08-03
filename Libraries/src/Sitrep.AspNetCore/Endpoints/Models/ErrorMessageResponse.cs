namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents the response to an error.
/// </summary>
/// <param name="Message">A message that describes the error.</param>
public record ErrorMessageResponse(string Message);