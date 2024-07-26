namespace SitRep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to get tickets.
/// </summary>
public static class GetTicketsEndpoint
{
    /// <summary>
    /// Registers the GetTickets endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterGetTickets(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.GetTickets, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.GetTicketsName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketsRequest request, 
                                                    ITicketStore ticketSore)
    {
        var tickets = request.IssuedTo == null ? [] : await ticketSore.GetTicketsAsync(request.IssuedTo);
        var response = new GetTicketsResponse(tickets);

        return Results.Ok(response);
    }
}
