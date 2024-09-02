namespace Sitrep.ApiClient.Ticketing;

/// <summary>
/// Represents a ticket allowing the progress of an asynchronous process to be tracked.
/// </summary>
public record Ticket
{
    /// <summary>
    /// Gets or sets the tracking number that can be used to track the current status of a ticket.
    /// </summary>
    public Guid TrackingNumber { get; init; }

    /// <summary>
    /// Gets or sets the unique identifier that indicates who the ticket was issued to.
    /// </summary>
    public required string IssuedTo { get; init; }

    /// <summary>
    /// Gets or sets the unique identifier that indicates who started the process. This may be the same as IssuedTo,
    /// or may be a system account when one process spawns another.
    /// </summary>
    public required string IssuedOnBehalfOf { get; init; }

    /// <summary>
    /// Gets or sets a description that explains what the purpose of the process is.
    /// </summary>
    public required string ReasonForIssuing { get; init; }

    /// <summary>
    /// Gets or sets the date and time the ticket was issued.
    /// </summary>
    public DateTime DateIssued { get; init; }

    /// <summary>
    /// Gets or sets the date and time the ticket expires.
    /// </summary>
    public DateTime ExpirationDate { get; init; }

    /// <summary>
    /// Gets or sets the current processing state the ticket is in.
    /// </summary>
    public ProcessingState ProcessingState { get; init; }

    /// <summary>
    /// Gets or sets whether the ticket is currently open, or has been closed.
    /// </summary>
    public bool IsClosed { get; init; }

    /// <summary>
    /// Gets or sets a message that describes the current state of processing.
    /// </summary>
    public string? ProcessingMessage { get; init; }

    /// <summary>
    /// Gets or sets the date and time the ticket was last progresses; null if the ticket has only been opened,
    /// but never progressed. This is not updated when the ticket is closed.
    /// </summary>
    public DateTime? DateLastProgressed { get; init; }

    /// <summary>
    /// Gets or sets the date and time the ticket was closed; null if the ticket is still open.
    /// </summary>
    public DateTime? DateClosed { get; init; }

    /// <summary>
    /// Gets or sets a URL to a resource if the process succeeds having created a resource.
    /// If the ticket is still open, or no resource was created, this will be null.
    /// </summary>
    public string? ResourceIdentifier { get; init; }

    /// <summary>
    /// Gets or sets a list of validation errors if the process failed due to invalid data.
    /// The key should be a property name, and the values should be the validation errors associated with that property.
    /// </summary>
    public Dictionary<string, string[]>? ValidationErrors { get; init; }

    /// <summary>
    /// Gets or sets a code that can be used to identify the type of error. Can be used by clients as a key to
    /// implement conditional flow.
    /// </summary>
    public string? ErrorCode { get; init; }
}