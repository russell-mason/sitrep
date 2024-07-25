namespace SitRep.AspNetCore.Endpoints;

public static class PutCloseTicketWithErrorEndpoint
{
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
