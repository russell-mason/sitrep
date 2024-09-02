namespace SitrepUI.Blazor.Ticketing.SignalR;

/// <summary>
/// Provides a connection to the SITREP SignalR hub providing real-time notification of changes to tickets.
/// Only changes made against tickets issued to the individual user will be relayed, and only
/// while the connection is open.
/// </summary>
/// <param name="jsonOptions">
/// Json options to use when transferring data. This must match the SignalR hub options.
/// </param>
public class SignalRTicketHubConnector(IOptions<JsonSerializerOptions> jsonOptions) : ISignalRTicketHubConnector
{
    private readonly JsonSerializerOptions _jsonOptions = jsonOptions.Value;
    private HubConnection? _hubConnection;

    /// <inheritdoc/>
    public event TicketOpenedEventHandler? OnTicketOpened;

    /// <inheritdoc/>
    public event TicketTransitionedEventHandler? OnTicketProgressed;

    /// <inheritdoc/>
    public event TicketTransitionedEventHandler? OnTicketClosedWithSuccess;

    /// <inheritdoc/>
    public event TicketTransitionedEventHandler? OnTicketClosedWithValidationErrors;

    /// <inheritdoc/>
    public event TicketTransitionedEventHandler? OnTicketClosedWithError;

    /// <inheritdoc/>
    public async Task ConnectAsync(string credentials)
    {
        if (_hubConnection is { State: HubConnectionState.Connected })
        {
            return;
        }

        _hubConnection = new HubConnectionBuilder()
                         // TODO: Replace with configuration and authentication
                         .WithUrl("https://localhost:7115/sitrep/ticket-hub",
                                  options => options.Headers.Add("Fake-User-Id", credentials))
                         .AddJsonProtocol(opts => opts.PayloadSerializerOptions = _jsonOptions)
                         .WithAutomaticReconnect()
                         .Build();

        _hubConnection.On<Ticket>("ticket:create:open", TicketOpened);
        _hubConnection.On<Ticket, Ticket>("ticket:transition:progress", TicketProgressed);
        _hubConnection.On<Ticket, Ticket>("ticket:transition:success", TicketClosedWithSuccess);
        _hubConnection.On<Ticket, Ticket>("ticket:transition:validation-error", TicketClosedWithValidationErrors);
        _hubConnection.On<Ticket, Ticket>("ticket:transition:error", TicketClosedWithError);
        
        await _hubConnection.StartAsync();
    }

    /// <inheritdoc/>
    public async Task DisconnectAsync()
    {
        if (_hubConnection is { State: HubConnectionState.Connected })
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();

            _hubConnection = null;
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();

        GC.SuppressFinalize(this);
    }

    private void TicketOpened(Ticket ticket) => OnTicketOpened?.Invoke(ticket);

    private void TicketProgressed(Ticket previous, Ticket current) => OnTicketProgressed?.Invoke(previous, current);

    private void TicketClosedWithSuccess(Ticket previous, Ticket current) => OnTicketClosedWithSuccess?.Invoke(previous, current);

    private void TicketClosedWithValidationErrors(Ticket previous, Ticket current) => OnTicketClosedWithValidationErrors?.Invoke(previous, current);
    
    private void TicketClosedWithError(Ticket previous, Ticket current) => OnTicketClosedWithError?.Invoke(previous, current);
}