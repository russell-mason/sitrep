namespace SitRep.AspNetCore.Endpoints;

/// <summary>
/// API route definitions.
/// </summary>
public static class Routes
{
    /// <summary>
    /// The Open API group tag.
    /// </summary>
    public const string Tag = "SITREP Ticket Processing API";

    /// <summary>
    /// The base URL path for all API routes.
    /// </summary>
    public const string Base = "/sitrep";

    /// <summary>
    /// The name of the route for the Ping endpoint.
    /// </summary>
    public const string PingName = "Ping";
    
    /// <summary>
    /// The route for the Ping endpoint.
    /// </summary>
    public const string Ping = $"{Base}/ping";

    /// <summary>
    /// The name of the route for the GetTicket endpoint.
    /// </summary>
    public const string GetTicketName = "GetTicket";

    /// <summary>
    /// The route for the GetTicket endpoint.
    /// </summary>
    public const string GetTicket = $"{Base}/tickets/{{trackingNumber:guid}}";

    /// <summary>
    /// The name of the route for the GetTickets endpoint.
    /// </summary>
    public const string GetTicketsName = "GetTickets";

    /// <summary>
    /// The route for the GetTickets endpoint.
    /// </summary>
    public const string GetTickets = $"{Base}/tickets";

    /// <summary>
    /// The name of the route for the OpenTicket endpoint.
    /// </summary>
    public const string OpenTicketName = "PostOpenTicket";

    /// <summary>
    /// The route for the OpenTicket endpoint.
    /// </summary>
    public const string OpenTicket = $"{Base}/tickets";

    /// <summary>
    /// The name of the route for the ProgressTicket endpoint.
    /// </summary>
    public const string ProgressTicketName = "PutProgressTicket";

    /// <summary>
    /// The route for the ProgressTicket endpoint.
    /// </summary>
    public const string ProgressTicket = $"{Base}/tickets/{{trackingNumber:guid}}";

    /// <summary>
    /// The name of the route for the CloseTicketWithSuccess endpoint.
    /// </summary>
    public const string CloseTicketWithSuccessName = "PutCloseTicketWithSuccess";

    /// <summary>
    /// The route for the CloseTicketWithSuccess endpoint.
    /// </summary>
    public const string CloseTicketWithSuccess = $"{Base}/tickets/closed/{{trackingNumber:guid}}";

    /// <summary>
    /// The name of the route for the CloseTicketWithValidationErrors endpoint.
    /// </summary>
    public const string CloseTicketWithValidationErrorsName = "PutCloseTicketWithValidationErrors";

    /// <summary>
    /// The route for the CloseTicketWithValidationErrors endpoint.
    /// </summary>
    public const string CloseTicketWithValidationErrors = $"{Base}/tickets/invalid/{{trackingNumber:guid}}";

    /// <summary>
    /// The name of the route for the CloseTicketWithError endpoint.
    /// </summary>
    public const string CloseTicketWithErrorName = "PutCloseTicketWithError";

    /// <summary>
    /// The route for the CloseTicketWithError endpoint.
    /// </summary>
    public const string CloseTicketWithError = $"{Base}/tickets/errored/{{trackingNumber:guid}}";
}
