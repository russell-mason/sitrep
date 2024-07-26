namespace SitRep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to update a ticket's progress.
/// </summary>
public static class PutProgressTicketEndpoint
{
    /// <summary>
    /// Registers the ProgressTicket endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterProgressTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.ProgressTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.ProgressTicketName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] ProgressTicketRequest request, 
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
