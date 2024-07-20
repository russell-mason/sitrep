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
                                                    ITicketTracker ticketTracker)
    {
        var result = await ticketTracker.CloseTicketAsync(request.TrackingNumber, request.ToErrorState());
        var response = new TicketStatusResponse(result);

        return Results.Ok(response);
    }
}
