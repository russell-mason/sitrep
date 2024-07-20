namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class GetTicketStatusesEndpoint
{
    public static void RegisterGetTicketStatuses(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.CoreApi.GetTicketStatuses, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.GetTicketStatusesName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketStatusesRequest request, 
                                                    ITicketTrackingStore ticketSore)
    {
        var ticketStatuses = request.IssuedTo == null ? [] : await ticketSore.GetTicketStatusesAsync(request.IssuedTo);
        var response = new GetTicketStatusesResponse(ticketStatuses);

        return Results.Ok(response);
    }
}
