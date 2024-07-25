namespace SitRep.AspNetCore.Endpoints;

public static class PostOpenTicketEndpoint
{
    public static void RegisterOpenTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPost(Routes.OpenTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.OpenTicketName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] OpenTicketRequest request,
                                                    ITicketProcessor ticketProcessor,
                                                    LinkGenerator linkGenerator)
    {
        var ticket = await ticketProcessor.CreateTicketAsync(request.ToCreateTicketState());
        var response = new TicketResponse(ticket);

        var url = linkGenerator.GetPathByName(Routes.GetTicketName, new { trackingNumber = response.TrackingNumber });

        return Results.Created(url, response);
    }
}
