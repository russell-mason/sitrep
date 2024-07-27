namespace Sitrep.AspNetCore.Endpoints;

/// <summary>
/// API endpoint to close a ticket with success.
/// </summary>
public static class PutCloseTicketWithSuccessEndpoint
{
    /// <summary>
    /// Registers the CloseTicketWithSuccess endpoint.
    /// </summary>
    /// <param name="endpointBuilder">The endpoint builder to extend.</param>
    public static void RegisterCloseTicketWithSuccess(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CloseTicketWithSuccess, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CloseTicketWithSuccessName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithSuccessRequest request,
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}