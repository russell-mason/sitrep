namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to close a ticket when the process completes successfully.
/// </summary>
/// <param name="TrackingNumber">The ticket's tracking number.</param>
/// <param name="Body">The request body containing additional details.</param>
public record CloseTicketWithSuccessRequest([FromRoute] Guid TrackingNumber,
                                            [FromBody] CloseTicketWithSuccessRequestBody Body)
{
    /// <summary>
    /// Converts the request to a transition state.
    /// </summary>
    /// <returns>The transition state.</returns>
    public SuccessTransition ToTransitionState() => new(Body.Message, Body.ResourceIdentifier);
}

/// <summary>
/// Represents a request body to close a ticket when the process completes successfully.
/// </summary>
/// <param name="Message">A message that describes why the ticket was closed.</param>
/// <param name="ResourceIdentifier">An optional URL to the affected resource.</param>
public record CloseTicketWithSuccessRequestBody(string Message, string? ResourceIdentifier = null);