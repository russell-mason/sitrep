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
                                                    ITicketTracker ticketTracker)
    {
        var result = await ticketTracker.ProgressTicketAsync(request.TrackingNumber, request.ToProgressState());
        var response = new TicketStatusResponse(result);

        return Results.Ok(response);
    }
}
