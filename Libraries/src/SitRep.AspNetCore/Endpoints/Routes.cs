namespace SitRep.AspNetCore.Endpoints;

public static class Routes
{
    public const string Base = "/sitrep";

    public const string Tag = "SITREP Ticket Processing API";

    public const string PingName = "Ping";
    public const string Ping = $"{Base}/ping";

    public const string GetTicketName = "GetTicketStatus";
    public const string GetTicket = $"{Base}/tickets/{{trackingNumber:guid}}";

    public const string GetTicketsName = "GetTicketStatuses";
    public const string GetTickets = $"{Base}/tickets";

    public const string OpenTicketName = "PostOpenTicket";
    public const string OpenTicket = $"{Base}/tickets";

    public const string ProgressTicketName = "PutProgressTicket";
    public const string ProgressTicket = $"{Base}/tickets/{{trackingNumber:guid}}";

    public const string CloseTicketWithSuccessName = "PutCloseTicketWithSuccess";
    public const string CloseTicketWithSuccess = $"{Base}/tickets/closed/{{trackingNumber:guid}}";

    public const string CloseTicketWithValidationErrorsName = "PutCloseTicketWithValidationErrors";
    public const string CloseTicketWithValidationErrors = $"{Base}/tickets/invalid/{{trackingNumber:guid}}";

    public const string CloseTicketWithErrorName = "PutCloseTicketWithError";
    public const string CloseTicketWithError = $"{Base}/tickets/errored/{{trackingNumber:guid}}";
}
