namespace SitrepUI.Blazor.Ticketing.SignalR.Events;

/// <summary>
/// Event handler for when a ticket is transformed from one state to another.
/// </summary>
/// <param name="previous">The ticket state before the change.</param>
/// <param name="current">The ticket state after the change.</param>
public delegate void TicketTransitionedEventHandler(Ticket previous, Ticket current);