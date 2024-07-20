namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is opened in the pending state.
/// </summary>
/// <param name="TrackingNumber">A tracking number that correlates the ticket to the current status of that ticket.</param>
/// <param name="IssuedTo">A unique identifier that can indicate who the ticket was issued to.</param>
/// <param name="IssuedOnBehalfOf">
/// A unique identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
/// system account when one process spawns another.
/// </param>
/// <param name="ReasonForIssuing">A description that explains what the purpose of the process is.</param>
[method: JsonConstructor]
public record OpenState(Guid TrackingNumber, string IssuedTo, string IssuedOnBehalfOf, string ReasonForIssuing)
{
    /// <summary>
    /// Specifies details when a ticket is opened in the pending state.
    /// </summary>
    /// <param name="issuedTo">
    /// A unique identifier that can indicate who the ticket was issued to.
    /// Automatically sets the IssuedOnBehalfOf property to be the same value.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenState(string issuedTo, string reasonForIssuing) : this(issuedTo, issuedTo, reasonForIssuing)
    {
    }

    /// <summary>
    /// Specifies details when a ticket is opened in the pending state.
    /// </summary>
    /// <param name="issuedTo">A unique identifier that can indicate who the ticket was issued to.</param>
    /// <param name="issuedOnBehalfOf">
    /// A unique identifier that can indicate who started the process. This may be the same as IssuedTo, or may be a
    /// system account when one process spawns another.
    /// </param>
    /// <param name="reasonForIssuing">A description that explains what the purpose of the process is.</param>
    public OpenState(string issuedTo, string issuedOnBehalfOf, string reasonForIssuing)
        : this(CombGuid.NewGuid(), issuedTo, issuedOnBehalfOf, reasonForIssuing)
    {
    }
}
