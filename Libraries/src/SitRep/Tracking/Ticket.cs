namespace SitRep.Tracking;

/// <summary>
/// Represents a ticket that can be used to track the progress of an asynchronous process.
/// </summary>
/// <param name="TrackingNumber">A tracking number that correlates the ticket to the current status of that ticket.</param>
public record Ticket(Guid TrackingNumber);
