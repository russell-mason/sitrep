namespace SitRep.AspNetCore.Endpoints;

public static class PutCloseTicketWithValidationErrorsEndpoint
{
    public static void RegisterCloseTicketWithValidationErrors(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CloseTicketWithValidationErrors, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CloseTicketWithValidationErrorsName)
                       .WithTags(Routes.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithValidationErrorsRequest request,
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
