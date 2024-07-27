namespace Sitrep.Ticketing;

/// <summary>
/// Represents a ticket allowing the progress of an asynchronous process to be tracked.
/// </summary>
/// <param name="TrackingNumber">A tracking number that can be used to track the current status of a ticket.</param>
/// <param name="IssuedTo">A unique identifier that indicates who the ticket was issued to.</param>
/// <param name="IssuedOnBehalfOf">
/// A unique identifier that indicates who started the process. This may be the same as IssuedTo, or may be a
/// system account when one process spawns another.
/// </param>
/// <param name="ReasonForIssuing">A description that explains what the purpose of the process is.</param>
public record Ticket(Guid TrackingNumber, string IssuedTo, string IssuedOnBehalfOf, string ReasonForIssuing)
{
    /// <summary>
    /// Gets or sets the default expiration time for a ticket in minutes. Defaults to 7 days.
    /// </summary>
    public static int ExpirationPeriodInMinutes { get; set; } = 60 * 24 * 7; // 7 days = 10080 minutes;

    /// <summary>
    /// Gets the date and time the ticket was issued.
    /// </summary>
    public DateTime DateIssued { get; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time the ticket expires.
    /// Defaults to the number of minutes specified by DefaultExpirationInMinutes from the date issued.
    /// </summary>
    public DateTime ExpirationDate { get; init; } = DateTime.UtcNow.AddMinutes(ExpirationPeriodInMinutes);

    /// <summary>
    /// Gets the current processing state the ticket is in.
    /// </summary>
    public ProcessingState ProcessingState { get; init; } = ProcessingState.Pending;

    /// <summary>
    /// Gets whether the ticket is currently open, or has been closed.
    /// </summary>
    public bool IsClosed => ProcessingState is ProcessingState.Succeeded or ProcessingState.Failed;

    /// <summary>
    /// Gets a message that describes the current state of processing.
    /// </summary>
    public string? ProcessingMessage { get; init; }

    /// <summary>
    /// Gets the date and time the ticket was last progresses; null if the ticket has only been opened, but never
    /// progressed. This is not updated when the ticket is closed.
    /// </summary>
    public DateTime? DateLastProgressed { get; init; }

    /// <summary>
    /// Gets the date and time the ticket was closed; null if the ticket is still open.
    /// </summary>
    public DateTime? DateClosed { get; init; }

    /// <summary>
    /// Gets a URL to a resource if the process succeeds having created a resource.
    /// If the ticket is still open, or no resource was created, this will be null.
    /// </summary>
    public string? ResourceIdentifier { get; init; }

    /// <summary>
    /// Gets a list of validation errors if the process failed due to invalid data.
    /// The key should be a property name, and the values should be the validation errors associated with that property.
    /// </summary>
    public ValidationErrorDictionary? ValidationErrors { get; init; }

    /// <summary>
    /// Gets a code that can be used to identify the type of error. Can be used by clients as a key to implement
    /// conditional flow.
    /// </summary>
    public string? ErrorCode { get; init; }
}