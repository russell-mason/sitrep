namespace Sitrep.AspNetCore.Http;

/// <summary>
/// Creates IResult values providing additional context.
/// </summary>
public static class SitrepResults
{
    /// <summary>
    /// Creates a 400 Not Found response with the tracking number and a message.
    /// </summary>
    /// <param name="trackingNumber">The tracking number for the ticket that cannot be found.</param>
    /// <returns>A Not Found result.</returns>
    public static IResult TicketNotFound(Guid trackingNumber)
    {
        var response = new TrackingErrorMessageResponse(trackingNumber, "Ticket not found");
        
        return Results.NotFound(response);
    }

    /// <summary>
    /// Creates a 500 Internal Server Error response with a message.
    /// </summary>
    /// <param name="message">The message that caused the server error.</param>
    /// <returns>An Internal Server Error result.</returns>
    public static IResult PublicInternalServerError(string message)
    {
        var error = new ErrorMessageResponse(message);
        
        return Results.Json(error, statusCode: (int) HttpStatusCode.InternalServerError);
    }
}
