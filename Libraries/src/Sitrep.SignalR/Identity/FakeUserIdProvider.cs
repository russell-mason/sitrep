namespace Sitrep.SignalR.Identity;

/// <summary>
/// Provides a fake User ID for SignalR connections via a web sockets header.
/// <para>
/// Should only be used for basic experiments.
/// </para>
/// </summary>
public class FakeUserIdProvider : IUserIdProvider
{
    /// <summary>
    /// Gets the User ID from the connection's header, using a header key of Fake-User-Id.
    /// </summary>
    /// <param name="connection">The hub connection to get the header from.</param>
    /// <returns>The value of the header if set; otherwise null.</returns>
    public string? GetUserId(HubConnectionContext connection)
    {
        var httpContext = connection.GetHttpContext();

        if (httpContext == null)
        {
            return null;
        }

        return httpContext.Request.Headers.TryGetValue("Fake-User-Id", out var userIdHeader) 
            ? userIdHeader.ToString() 
            : null;
    }
}
