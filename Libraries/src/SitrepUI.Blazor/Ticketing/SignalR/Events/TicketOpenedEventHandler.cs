namespace SitrepUI.Blazor.Ticketing.SignalR.Events;

/// <summary>
/// Event handler for when a ticket is opened.
/// </summary>
/// <param name="ticket">The ticket that was opened.</param>
public delegate void TicketOpenedEventHandler(Ticket ticket);