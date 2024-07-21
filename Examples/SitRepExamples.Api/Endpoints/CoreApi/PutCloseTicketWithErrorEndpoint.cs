namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class PutCloseTicketWithErrorEndpoint
{
    public static void RegisterCloseTicketWithError(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CoreApi.CloseTicketWithError, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.CloseTicketWithErrorName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithErrorRequest request,
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
