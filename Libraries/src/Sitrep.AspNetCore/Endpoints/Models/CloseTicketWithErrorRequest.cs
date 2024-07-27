namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to close a ticket due to an error.
/// </summary>
/// <param name="TrackingNumber">The ticket's tracking number.</param>
/// <param name="Body">The request body containing additional details.</param>
public record CloseTicketWithErrorRequest([FromRoute] Guid TrackingNumber,
                                          [FromBody] CloseTicketWithErrorRequestBody Body)
{
    /// <summary>
    /// Converts the request to a transition state.
    /// </summary>
    /// <returns>The transition state.</returns>
    public ErrorTransition ToTransitionState() => new(Body.Message, Body.ErrorCode);
}

/// <summary>
/// Represents the body of a request to close a ticket due to an error.
/// </summary>
/// <param name="Message">A message that describes why the ticket was closed.</param>
/// <param name="ErrorCode">An optional error code that allows the client to make decisions about the error.</param>
public record CloseTicketWithErrorRequestBody(string Message, string? ErrorCode = null);