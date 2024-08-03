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
    /// Return a 500 with a message when a PublicException is thrown.
    /// </para>
    /// <para>
    /// All other unhandled exceptions return a basic 500.
    /// </para>
    /// </summary>
    /// <param name="context">The current HTTP context</param>
    /// <returns></returns>
    public static async Task HandlerException(HttpContext context)
    {
        var exceptionFeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();
        var exception = exceptionFeature.Error;

        switch (exception)
        {
            case TrackingNumberNotFoundException trackingNumberNotFoundException:
            {
                var result = SitrepResults.TicketNotFound(trackingNumberNotFoundException.TrackingNumber);

                await result.ExecuteAsync(context);
                break;
            }
            case PublicException publicException:
            {
                var result = SitrepResults.PublicInternalServerError(publicException.Message);

                await result.ExecuteAsync(context);
                break;
            }
            default:
            {
                var result = Results.StatusCode((int) HttpStatusCode.InternalServerError);

                await result.ExecuteAsync(context);
                break;
            }
        }
    }
}