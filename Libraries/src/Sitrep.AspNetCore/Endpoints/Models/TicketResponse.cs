namespace Sitrep.AspNetCore.Endpoints.Models;

/// <summary>
/// Represents the response to a ticket request.
/// </summary>
public record TicketResponse
{
    /// <summary>
    /// Create a new instance of the TicketResponse class.
    /// </summary>
    /// <param name="ticket">The ticket to base the response on.</param>
    public TicketResponse(Ticket ticket)
    {
        TrackingNumber = ticket.TrackingNumber;
        IssuedTo = ticket.IssuedTo;
        IssuedOnBehalfOf = ticket.IssuedOnBehalfOf;
        ReasonForIssuing = ticket.ReasonForIssuing;
        DateIssued = ticket.DateIssued;
        ExpirationDate = ticket.ExpirationDate;
        ProcessingState = ticket.ProcessingState;
        IsClosed = ticket.IsClosed;
        ProcessingMessage = ticket.ProcessingMessage;
        DateLastProgressed = ticket.DateLastProgressed;
        DateClosed = ticket.DateClosed;
        ResourceIdentifier = ticket.ResourceIdentifier;
        ValidationErrors = ticket.ValidationErrors;
        ErrorCode = ticket.ErrorCode;
    }

    /// <summary>
    /// Gets the tracking number that can be used to track the current status of a ticket.
    /// </summary>
    public Guid TrackingNumber { get; }

    /// <summary>
    /// Gets the unique identifier that indicates who the ticket was issued to.
    /// </summary>
    public string IssuedTo { get; }

    /// <summary>
    /// Gets the unique identifier that indicates who started the process. This may be the same as IssuedTo, or
    /// may be a system account when one process spawns another.
    /// </summary>
    public string IssuedOnBehalfOf { get; }

    /// <summary>
    /// Gets a description that explains what the purpose of the process is.
    /// </summary>
    public string ReasonForIssuing { get; }

    /// <summary>
    /// Gets the date and time the ticket was issued.
    /// </summary>
    public DateTime DateIssued { get; }

    /// <summary>
    /// Gets the date and time the ticket expires.
    /// </summary>
    public DateTime ExpirationDate { get; }

    /// <summary>
    /// Gets the current processing state the ticket is in.
    /// </summary>
    public ProcessingState ProcessingState { get; }

    /// <summary>
    /// Gets whether the ticket is currently open, or has been closed.
    /// </summary>
    public bool IsClosed { get; }

    /// <summary>
    /// Gets a message that describes the current state of processing.
    /// </summary>
    public string? ProcessingMessage { get; }

    /// <summary>
    /// Gets the date and time the ticket was last progresses; null if the ticket has only been opened, but never
    /// progressed. This is not updated when the ticket is closed.
    /// </summary>
    public DateTime? DateLastProgressed { get; }

    /// <summary>
    /// Gets the date and time the ticket was closed; null if the ticket is still open.
    /// </summary>
    public DateTime? DateClosed { get; }

    /// <summary>
    /// Gets a URL to a resource if the process succeeds having created a resource.
    /// If the ticket is still open, or no resource was created, this will be null.
    /// </summary>
    public string? ResourceIdentifier { get; }

    /// <summary>
    /// Gets a list of validation errors if the process failed due to invalid data.
    /// The key should be a property name, and the values should be the validation errors associated with that property.
    /// </summary>
    public Dictionary<string, string[]>? ValidationErrors { get; }

    /// <summary>
    /// Gets a code that can be used to identify the type of error. Can be used by clients as a key to implement
    /// conditional flow.
    /// </summary>
    public string? ErrorCode { get; }
}