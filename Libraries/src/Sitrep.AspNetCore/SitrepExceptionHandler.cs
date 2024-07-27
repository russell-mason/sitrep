namespace Sitrep.AspNetCore;

/// <summary>
/// Handles exceptions thrown by the sitrep API.
/// </summary>
public class SitrepExceptionHandler
{
    /// <summary>
    /// Handles exceptions thrown by the application.
    /// <para>
    /// Returns a 404 when a TrackingNumberNotFoundException is thrown.
    /// </para>
    /// <para>
    /// Return a 500 for all other unhandled exceptions.
    /// </para>
    /// </summary>
    /// <param name="context">The current HTTP context</param>
    /// <returns></returns>
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