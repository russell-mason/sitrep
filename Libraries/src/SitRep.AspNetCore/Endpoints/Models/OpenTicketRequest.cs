namespace SitRep.AspNetCore.Endpoints.Models;

public record OpenTicketRequest([FromBody] OpenTicketRequestBody Body)
{
    public OpenTicket ToCreateTicketState() => new(Body.IssuedTo, Body.IssuedOnBehalfOf, Body.ReasonForIssuing);
}

public record OpenTicketRequestBody(string IssuedTo, string IssuedOnBehalfOf, string ReasonForIssuing);