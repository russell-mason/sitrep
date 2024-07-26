namespace SitRep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to close a ticket with an error.
/// </summary>
public static class PutCloseTicketWithErrorEndpoint
{
    /// <summary>
    /// Registers the CloseTicketWithError endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterCloseTicketWithError(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CloseTicketWithError, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CloseTicketWithErrorName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithErrorRequest request,
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
