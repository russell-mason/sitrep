namespace SitRep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents a request to open a ticket.
/// </summary>
/// <param name="Body">The request body containing additional details.</param>
public record OpenTicketRequest([FromBody] OpenTicketRequestBody Body)
{
    /// <summary>
    /// Converts the request to a creation state.
    /// </summary>
    /// <returns>The creation state.</returns>
    public OpenTicket ToCreateTicketState() => new(Body.IssuedTo, Body.IssuedOnBehalfOf ?? Body.IssuedTo, Body.ReasonForIssuing);
}

/// <summary>
/// Represents the body of a request to open a ticket.
/// </summary>
/// <param name="IssuedTo">Who the ticket was issued to.</param>
/// <param name="IssuedOnBehalfOf">Who the ticket was issued on behalf of, e.g. The user or a system account.</param>
/// <param name="ReasonForIssuing">A message that describes why the ticket was opened.</param>
public record OpenTicketRequestBody(string IssuedTo, string? IssuedOnBehalfOf, string ReasonForIssuing);