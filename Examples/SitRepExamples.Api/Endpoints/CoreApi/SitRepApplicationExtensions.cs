namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class SitRepApplicationExtensions
{
    public static WebApplication UseSitRep(this WebApplication app)
    {
        ConfigureExceptionHandling(app);
        RegisterEndpoints(app);

        return app;
    }

    private static void ConfigureExceptionHandling(IApplicationBuilder builder)
    {
        builder.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = SitRepExceptionHandler.HandlerException
        });
    }

    private static void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.RegisterPing();
        builder.RegisterGetTicketStatus();
        builder.RegisterGetTicketStatuses();
        builder.RegisterOpenTicket();
        builder.RegisterProgressTicket();
        builder.RegisterCloseTicketWithSuccess();
        builder.RegisterCloseTicketWithValidationErrors();
        builder.RegisterCloseTicketWithError();
    }
}