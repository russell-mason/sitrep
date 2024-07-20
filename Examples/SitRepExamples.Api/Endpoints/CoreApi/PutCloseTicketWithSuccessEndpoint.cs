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
                                                    ITicketTracker ticketTracker)
    {
        var result = await ticketTracker.CloseTicketAsync(request.TrackingNumber, request.ToSuccessState());
        var response = new TicketStatusResponse(result);

        return Results.Ok(response);
    }
}
