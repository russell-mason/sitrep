// ReSharper disable MemberHidesStaticFromOuterClass

namespace SitRepExamples.Api.Endpoints;

public static class Routes
{
    public const string Base = "";

    public static class SanityChecks
    {
        public const string Base = $"{Routes.Base}/sanity-checks";

        public const string Ok = $"{Base}/ok";

        public const string NotFound = $"{Base}/not-found";
    }

    public static class CoreApi
    {
        public const string Tag = "Core Ticket Processing API";

        public const string Base = $"{Routes.Base}/sitrep";

        public const string PingName = "Ping";
        public const string Ping = $"{Base}/ping";

        public const string GetTicketStatusName = "GetTicketStatus";
        public const string GetTicketStatus = $"{Base}/tickets/{{trackingNumber:guid}}";

        public const string GetTicketStatusesName = "GetTicketStatuses";
        public const string GetTicketStatuses = $"{Base}/tickets";

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
}
