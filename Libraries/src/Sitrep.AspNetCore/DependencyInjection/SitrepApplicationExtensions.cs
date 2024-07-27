namespace Sitrep.AspNetCore.DependencyInjection;

/// <summary>
/// Adds sitrep API functionality to the application.
/// </summary>
public static class SitrepApplicationExtensions
{
    /// <summary>
    /// Adds sitrep functionality, such as exception handling, API endpoints, etc.
    /// </summary>
    /// <param name="app">The application to extend.</param>
    /// <returns>The application to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// app.UseSitrep();
    /// </code>
    /// </example>
    public static WebApplication UseSitrep(this WebApplication app)
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
                                        ExceptionHandler = SitrepExceptionHandler.HandlerException
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