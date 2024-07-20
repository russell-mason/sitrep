namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record OpenTicketRequest([FromBody] OpenTicketRequestBody Body)
{
    public OpenState ToOpenState() => new(Body.IssuedTo, Body.IssuedOnBehalfOf, Body.ReasonForIssuing);
}

public record OpenTicketRequestBody(string IssuedTo, string IssuedOnBehalfOf, string ReasonForIssuing);
