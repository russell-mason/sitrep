namespace SitRep.Ticketing;

/// <summary>
/// Represents an exception that's thrown when a tracking number cannot be found, either because its invalid
/// or because the ticket has expired.
/// </summary>
[Serializable]
public class TrackingNumberNotFoundException : Exception
{
    public TrackingNumberNotFoundException(Guid trackingNumber) : this(trackingNumber, $"Ticket '{trackingNumber}' not found")
    {
    }

    public TrackingNumberNotFoundException(Guid trackingNumber, string message) : base(message)
    {
        TrackingNumber = trackingNumber;
    }

    public TrackingNumberNotFoundException(Guid trackingNumber, string message, Exception inner) : base(message, inner)
    {
        TrackingNumber = trackingNumber;
    }

    /// <summary>
    /// Gets the tracking number of the ticket that couldn't be found.
    /// </summary>
    public Guid TrackingNumber { get; }
}
