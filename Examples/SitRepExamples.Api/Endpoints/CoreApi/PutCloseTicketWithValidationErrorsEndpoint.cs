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
                                                    ITicketTracker ticketTracker)
    {
        var result = await ticketTracker.CloseTicketAsync(request.TrackingNumber, request.ToValidationState());
        var response = new TicketStatusResponse(result);

        return Results.Ok(response);
    }
}
