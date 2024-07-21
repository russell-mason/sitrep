namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class GetTicketEndpoint
{
    public static void RegisterGetTicket(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.CoreApi.GetTicket, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.GetTicketName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketRequest request,
                                                    ITicketStore ticketStore)
    {
        var ticket = await ticketStore.GetTicketAsync(request.TrackingNumber);

        if (ticket == null)
        {
            return Results.NotFound();
        }

        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}