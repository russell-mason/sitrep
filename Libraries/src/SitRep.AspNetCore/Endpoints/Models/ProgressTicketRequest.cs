namespace SitRep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to progress a ticket's state.
/// </summary>
/// <param name="TrackingNumber">The ticket's tracking number.</param>
/// <param name="Body">The request body containing additional details.</param>
public record ProgressTicketRequest([FromRoute] Guid TrackingNumber, 
                                    [FromBody] ProgressTicketRequestBody Body)
{
    /// <summary>
    /// Converts the request to a transition state.
    /// </summary>
    /// <returns>The transition state.</returns>
    public ProgressTransition ToTransitionState() => new(Body.Message);
}

/// <summary>
/// Represents the body of a request to progress a ticket's state.
/// </summary>
/// <param name="Message">A message that describes why the ticket was progressed.</param>
public record ProgressTicketRequestBody(string Message);
