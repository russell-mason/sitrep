namespace SitRep.AspNetCore.Endpoints;

public static class GetTicketsEndpoint
{
    public static void RegisterGetTickets(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet(Routes.GetTickets, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.GetTicketsName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] GetTicketsRequest request, 
                                                    ITicketStore ticketSore)
    {
        var tickets = request.IssuedTo == null ? [] : await ticketSore.GetTicketsAsync(request.IssuedTo);
        var response = new GetTicketsResponse(tickets);

        return Results.Ok(response);
    }
}
