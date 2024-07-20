namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class PostOpenTicketEndpoint
{
    public static void RegisterOpenTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPost(Routes.CoreApi.OpenTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.OpenTicketName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] OpenTicketRequest request,
                                                    ITicketTracker ticketTracker,
                                                    LinkGenerator linkGenerator)
    {
        var result = await ticketTracker.OpenTicketAsync(request.ToOpenState());
        var response = new TicketStatusResponse(result);

        var url = linkGenerator.GetPathByName(Routes.CoreApi.GetTicketStatusName, new { trackingNumber = response.TrackingNumber });

        return Results.Created(url, response);
    }
}
