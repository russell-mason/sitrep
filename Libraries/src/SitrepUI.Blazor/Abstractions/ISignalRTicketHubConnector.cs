namespace SitrepUI.Blazor.Abstractions;

/// <summary>
/// Represents a connection to the SITREP SignalR hub providing real-time notification of changes to tickets.
/// </summary>
public interface ISignalRTicketHubConnector : IAsyncDisposable
{
    /// <summary>
    /// Connects to the SignalR hub and starts listening for ticket based change events.
    /// </summary>
    /// <param name="credentials">
    /// The credentials used to determine the current user.
    /// <para>
    /// N.B. Temporarily this should be set to the same value as the issuedTo value and will be used for
    /// the Fake-User-Id header.</para>
    /// </param>
    /// <returns></returns>
    Task ConnectAsync(string credentials);

    /// <summary>
    /// Disconnects from the SignalR hub and stops listening for ticket based change events.
    /// </summary>
    /// <returns></returns>
    Task DisconnectAsync();

    /// <summary>
    /// Occurs when a ticket is opened.
    /// </summary>
    public event TicketOpenedEventHandler? OnTicketOpened;

    /// <summary>
    /// Occurs when the state of the ticket is set to in-progress, or changes from one state to
    /// another whilst still open.
    /// </summary>
    public event TicketTransitionedEventHandler? OnTicketProgressed;

    /// <summary>
    /// Occurs when the state of the ticket is set to closed with a success status.
    /// </summary>
    public event TicketTransitionedEventHandler? OnTicketClosedWithSuccess;

    /// <summary>
    /// Occurs when the state of the ticket is set to closed due to validation errors.
    /// </summary>
    public event TicketTransitionedEventHandler? OnTicketClosedWithValidationErrors;

    /// <summary>
    /// Occurs when the state of the ticket is set to closed due to an error.
    /// </summary>
    public event TicketTransitionedEventHandler? OnTicketClosedWithError;
}