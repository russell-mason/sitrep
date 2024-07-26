namespace SitRep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to get a ticket.
/// </summary>
public static class GetTicketEndpoint
{
    /// <summary>
    /// Registers the GetTicket endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterGetTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.GetTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.GetTicketName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketRequest request,
                                                    ITicketStore ticketStore)
    {
        var ticket = await ticketStore.GetTicketAsync(request.TrackingNumber);

        if (ticket == null)
        {
            return Results.NotFound();
        }

        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}