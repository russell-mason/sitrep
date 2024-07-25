namespace SitRep.AspNetCore.DependencyInjection;

public static class SitRepApplicationExtensions
{
    public static WebApplication UseSitRep(this WebApplication app)
    {
        ConfigureExceptionHandling(app);
        RegisterEndpoints(app);
        InitializeTicketStore(app);

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
        builder.RegisterGetTicket();
        builder.RegisterGetTickets();
        builder.RegisterOpenTicket();
        builder.RegisterProgressTicket();
        builder.RegisterCloseTicketWithSuccess();
        builder.RegisterCloseTicketWithValidationErrors();
        builder.RegisterCloseTicketWithError();
    }

    private static void InitializeTicketStore(IApplicationBuilder builder) =>
        builder.ApplicationServices.GetService<ITicketStore>()?.Initialize();
}