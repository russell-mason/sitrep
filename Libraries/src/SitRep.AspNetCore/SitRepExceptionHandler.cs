namespace SitRep.AspNetCore;

public class SitRepExceptionHandler
{
    public static async Task HandlerException(HttpContext context)
    {
        var exceptionFeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();
        var exception = exceptionFeature.Error;

        if (exception is TrackingNumberNotFoundException trackingNumberNotFoundException)
        {
            var error = new ErrorMessageResponse(trackingNumberNotFoundException.TrackingNumber, "Ticket not found");
            var result = Results.NotFound(error);

            await result.ExecuteAsync(context);
        }
        else
        {
            var result = Results.StatusCode((int) HttpStatusCode.InternalServerError);

            await result.ExecuteAsync(context);
        }
    }
}