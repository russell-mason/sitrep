namespace SitRep.AspNetCore.Endpoints.Models;

public record TicketResponse
{
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

    public Guid TrackingNumber { get; }

    public string IssuedTo { get; }

    public string IssuedOnBehalfOf { get; }

    public string ReasonForIssuing { get; }

    public DateTime DateIssued { get; }

    public DateTime ExpirationDate { get; }

    public ProcessingState ProcessingState { get; }

    public bool IsClosed { get; }

    public string? ProcessingMessage { get; }

    public DateTime? DateLastProgressed { get; }

    public DateTime? DateClosed { get; }

    public string? ResourceIdentifier { get; }

    public Dictionary<string, string[]>? ValidationErrors { get; }

    public string? ErrorCode { get; }
}