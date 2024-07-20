namespace SitRepExamples.Api.Endpoints.CoreApi.Models;

public record TicketStatusResponse
{
    public TicketStatusResponse(TicketStatus ticketStatus)
    {
        TrackingNumber = ticketStatus.TrackingNumber;
        IssuedTo = ticketStatus.IssuedTo;
        IssuedOnBehalfOf = ticketStatus.IssuedOnBehalfOf;
        ReasonForIssuing = ticketStatus.ReasonForIssuing;
        DateIssued = ticketStatus.DateIssued;
        ProcessingStage = ticketStatus.ProcessingStage;
        IsClosed = ticketStatus.IsClosed;
        ProcessingMessage = ticketStatus.ProcessingMessage;
        DateLastProgressed = ticketStatus.DateLastProgressed;
        DateClosed = ticketStatus.DateClosed;
        ResourceIdentifier = ticketStatus.ResourceIdentifier;
        ValidationErrors = ticketStatus.ValidationErrors;
        ErrorCode = ticketStatus.ErrorCode;
    }

    public Guid TrackingNumber { get; }

    public string IssuedTo { get; }

    public string IssuedOnBehalfOf { get; }

    public string ReasonForIssuing { get; }

    public DateTime DateIssued { get; }

    public ProcessingStage ProcessingStage { get; }

    public bool IsClosed { get; }

    public string? ProcessingMessage { get; }

    public DateTime? DateLastProgressed { get; }

    public DateTime? DateClosed { get; }

    public string? ResourceIdentifier { get; }

    public Dictionary<string, string[]>? ValidationErrors { get; }

    public string? ErrorCode { get; }
}
