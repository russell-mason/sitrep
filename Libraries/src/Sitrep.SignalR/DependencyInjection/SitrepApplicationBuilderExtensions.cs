namespace Sitrep.SignalR.DependencyInjection;

/// <summary>
/// Adds sitrep SignalR notification functionality to the application.
/// </summary>
public static class SitrepApplicationBuilderExtensions
{
    /// <summary>
    /// Adds sitrep SignalR functionality.
    /// </summary>
    /// <param name="applicationBuilder">The application to extend.</param>
    /// <returns>The application to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// app.UseSitrep().UseSignalR();
    /// </code>
    /// </example>
    public static SitrepApplicationBuilder UseSignalRNotifications(this SitrepApplicationBuilder applicationBuilder)
    {
        ConfigureSignalR(applicationBuilder.WebApplication);

        return applicationBuilder;
    }

    private static void ConfigureSignalR(WebApplication app)
    {
        app.MapHub<SignalRTicketHub>(Routes.ChangeNotificationHub);
    }
}