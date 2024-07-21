namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class GetTicketsEndpoint
{
    public static void RegisterGetTickets(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.CoreApi.GetTickets, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.GetTicketsName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketsRequest request, 
                                                    ITicketStore ticketSore)
    {
        var tickets = request.IssuedTo == null ? [] : await ticketSore.GetTicketsAsync(request.IssuedTo);
        var response = new GetTicketsResponse(tickets);

        return Results.Ok(response);
    }
}
