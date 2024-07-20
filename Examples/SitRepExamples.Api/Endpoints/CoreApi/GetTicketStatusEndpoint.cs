namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class GetTicketStatusEndpoint
{
    public static void RegisterGetTicketStatus(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.CoreApi.GetTicketStatus, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.GetTicketStatusName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketStatusRequest request,
                                                    ITicketTrackingStore ticketSore)
    {
        var ticketStatus = await ticketSore.GetTicketStatusAsync(request.TrackingNumber);

        if (ticketStatus == null)
        {
            return Results.NotFound();
        }

        var response = new TicketStatusResponse(ticketStatus);

        return Results.Ok(response);
    }
}
