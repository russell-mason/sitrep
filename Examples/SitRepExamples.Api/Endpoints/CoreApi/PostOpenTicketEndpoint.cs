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
                                                    ITicketProcessor ticketProcessor,
                                                    LinkGenerator linkGenerator)
    {
        var ticket = await ticketProcessor.CreateTicketAsync(request.ToCreateTicketState());
        var response = new TicketResponse(ticket);

        var url = linkGenerator.GetPathByName(Routes.CoreApi.GetTicketName, new { trackingNumber = response.TrackingNumber });

        return Results.Created(url, response);
    }
}
