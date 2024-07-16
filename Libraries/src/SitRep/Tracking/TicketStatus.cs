namespace SitRep.Tracking;

/// <summary>
/// Represents the current status of a ticket allowing the progress of an asynchronous process to be tracked.
/// </summary>
/// <param name="TrackingNumber">A tracking number that correlates the ticket to the current status of that ticket.</param>
/// <param name="IssuedTo">A unique identifier that can indicate who the ticket was issued to.</param>
/// <param name="IssuedOnBehalfOf">
/// A unique identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
/// system account when one process spawns another.
/// </param>
/// <param name="ReasonForIssuing">A description that explains what the purpose of the process is.</param>
public record TicketStatus(Guid TrackingNumber, string IssuedTo, string IssuedOnBehalfOf, string ReasonForIssuing)
{
    /// <summary>
    /// Gets the date and time the ticket was issued.
    /// </summary>
    public DateTime DateIssued { get; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the ticket's current processing stage.
    /// </summary>
    public ProcessingStage ProcessingStage { get; init; } = ProcessingStage.Pending;

    /// <summary>
    /// Gets whether the ticket is currently open, or has been closed.
    /// </summary>
    public bool IsClosed => ProcessingStage is ProcessingStage.Succeeded or ProcessingStage.Failed;

    /// <summary>
    /// Gets a message that describes the current state of processing.
    /// </summary>
    public string? ProcessingMessage { get; init; }

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
    public Dictionary<string, string[]>? ValidationErrors { get; init; }

    /// <summary>
    /// Gets a code that can be used to identify the type of error. Can be used by clients as a key to implement
    /// conditional flow.
    /// </summary>
    public string? ErrorCode { get; init; }
}