namespace Sitrep.SignalR.Endpoints;

/// <summary>
/// SignalR route definitions.
/// </summary>
public static class Routes
{
    /// <summary>
    /// The base URL path for all SignalR Hub routes.
    /// </summary>
    public const string Base = "/sitrep";

    /// <summary>
    /// The route for the SignalR change notification hub.
    /// </summary>
    public const string ChangeNotificationHub = $"{Base}/ticket-hub";
}
