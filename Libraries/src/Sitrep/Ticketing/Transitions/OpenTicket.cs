namespace Sitrep.Ticketing.Transitions;

/// <summary>
/// Provides state that can be used to create a ticket in an initial pending state.
/// </summary>
public class OpenTicket : IOpenTicketState
{
    private readonly Guid _trackingNumber;
    private readonly string _issuedTo;
    private readonly string _issuedOnBehalfOf;
    private readonly string _reasonForIssuing;

    /// <summary>
    /// Gets a key-based description that indicates the action this state represents.
    /// </summary>
    public string Action => "ticket:create:open";

    /// <summary>
    /// Creates new state with basic ticket details.
    /// </summary>
    /// <param name="trackingNumber">A unique tracking number that can identify the ticket.</param>
    /// <param name="issuedTo">An identifier that can indicate who the ticket was issued to.</param>
    /// <param name="issuedOnBehalfOf">
    /// An identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
    /// system account when one process spawns another.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    private OpenTicket(Guid trackingNumber, string issuedTo, string issuedOnBehalfOf, string reasonForIssuing)
    {
        _trackingNumber = trackingNumber;
        _issuedTo = issuedTo;
        _issuedOnBehalfOf = issuedOnBehalfOf;
        _reasonForIssuing = reasonForIssuing;
    }

    /// <summary>
    /// Creates new state with basic ticket details.
    /// </summary>
    /// <param name="issuedTo">A unique identifier that can indicate who the ticket was issued to.</param>
    /// <param name="issuedOnBehalfOf">
    /// An identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
    /// system account when one process spawns another.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenTicket(string issuedTo, string issuedOnBehalfOf, string reasonForIssuing)
        : this(CombGuid.NewGuid(), issuedTo, issuedOnBehalfOf, reasonForIssuing)
    {
    }

    /// <summary>
    /// Creates new state with basic ticket details.
    /// </summary>
    /// <param name="issuedTo">
    /// An identifier that can indicate who the ticket was issued to.
    /// Automatically sets the IssuedOnBehalfOf property to be the same value.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenTicket(string issuedTo, string reasonForIssuing) : this(issuedTo, issuedTo, reasonForIssuing)
    {
    }

    /// <summary>
    /// Allows a processor to create a new ticket in the desired state.
    /// </summary>
    /// <returns>The newly created ticket.</returns>
    public Ticket CreateState() => new(_trackingNumber, _issuedTo, _issuedOnBehalfOf, _reasonForIssuing);
}