namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class PutProgressTicketEndpoint
{
    public static void RegisterProgressTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CoreApi.ProgressTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.ProgressTicketName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] ProgressTicketRequest request, 
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
