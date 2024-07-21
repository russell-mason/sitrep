namespace SitRep.Ticketing.Transitions;

/// <summary>
/// Creates a ticket int the initial pending state.
/// </summary>
public class OpenTicket : ICreateTicketState
{
    private readonly Guid _trackingNumber;
    private readonly string _issuedTo;
    private readonly string _issuedOnBehalfOf;
    private readonly string _reasonForIssuing;

    private OpenTicket(Guid trackingNumber, string issuedTo, string issuedOnBehalfOf, string reasonForIssuing)
    {
        _trackingNumber = trackingNumber;
        _issuedTo = issuedTo;
        _issuedOnBehalfOf = issuedOnBehalfOf;
        _reasonForIssuing = reasonForIssuing;
    }

    /// <summary>
    /// Create a new ticket in the pending state.
    /// </summary>
    /// <param name="issuedTo">A unique identifier that can indicate who the ticket was issued to.</param>
    /// <param name="issuedOnBehalfOf">
    /// A unique identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
    /// system account when one process spawns another.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenTicket(string issuedTo, string issuedOnBehalfOf, string reasonForIssuing) 
        : this(CombGuid.NewGuid(), issuedTo, issuedOnBehalfOf, reasonForIssuing)
    {
    }

    /// <summary>
    /// Specifies details when a ticket is opened in the pending state.
    /// </summary>
    /// <param name="issuedTo">
    /// A unique identifier that can indicate who the ticket was issued to.
    /// Automatically sets the IssuedOnBehalfOf property to be the same value.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenTicket(string issuedTo, string reasonForIssuing) : this(issuedTo, issuedTo, reasonForIssuing)
    {
    }

    public Ticket CreateState() => new(_trackingNumber, _issuedTo, _issuedOnBehalfOf, _reasonForIssuing);
}
