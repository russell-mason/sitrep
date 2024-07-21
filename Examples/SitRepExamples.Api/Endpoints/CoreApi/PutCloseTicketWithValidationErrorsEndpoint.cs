namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class PutCloseTicketWithValidationErrorsEndpoint
{
    public static void RegisterCloseTicketWithValidationErrors(this IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut(Routes.CoreApi.CloseTicketWithValidationErrors, ExecuteAsync)
                       .WithOpenApi()
                       .WithName(Routes.CoreApi.CloseTicketWithValidationErrorsName)
                       .WithTags(Routes.CoreApi.Tag);
    }

    private static async Task<IResult> ExecuteAsync([AsParameters] CloseTicketWithValidationErrorsRequest request,
                                                    ITicketProcessor ticketProcessor)
    {
        var ticket = await ticketProcessor.TransitionTicketAsync(request.TrackingNumber, request.ToTransitionState());
        var response = new TicketResponse(ticket);

        return Results.Ok(response);
    }
}
