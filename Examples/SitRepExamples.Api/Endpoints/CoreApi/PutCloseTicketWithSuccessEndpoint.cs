namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class PutCloseTicketWithSuccessEndpoint
{
    public static void RegisterCloseTicketWithSuccess(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CoreApi.CloseTicketWithSuccess, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.CloseTicketWithSuccessName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithSuccessRequest request, 
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
