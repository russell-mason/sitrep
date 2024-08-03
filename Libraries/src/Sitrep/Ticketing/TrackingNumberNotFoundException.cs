namespace Sitrep.Ticketing;

/// <summary>
/// Represents an exception that's thrown when a tracking number cannot be found, either because its invalid
/// or because the ticket has expired.
/// </summary>
[Serializable]
public class TrackingNumberNotFoundException : PublicException
{
    /// <summary>
    /// Creates a new instance of the TrackingNumberNotFoundException class.
    /// </summary>
    /// <param name="trackingNumber">The tracking number of the ticket that couldn't be found.</param>
    public TrackingNumberNotFoundException(Guid trackingNumber) : this(trackingNumber, $"Ticket '{trackingNumber}' not found")
    {
    }

    /// <summary>
    /// Creates a new instance of the TrackingNumberNotFoundException class.
    /// </summary>
    /// <param name="trackingNumber">The tracking number of the ticket that couldn't be found.</param>
    /// <param name="message">The message that describes the error.</param>
    public TrackingNumberNotFoundException(Guid trackingNumber, string message) : base(message)
    {
        TrackingNumber = trackingNumber;
    }

    /// <summary>
    /// Creates a new instance of the TrackingNumberNotFoundException class.
    /// </summary>
    /// <param name="trackingNumber">The tracking number of the ticket that couldn't be found.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that originated the current issue.</param>
    public TrackingNumberNotFoundException(Guid trackingNumber, string message, Exception inner) : base(message, inner)
    {
        TrackingNumber = trackingNumber;
    }

    /// <summary>
    /// Gets the tracking number of the ticket that couldn't be found.
    /// </summary>
    public Guid TrackingNumber { get; }
}