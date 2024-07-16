namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is transitioning to a completed failed state due to an error.
/// </summary>
/// <param name="ErrorCode">
/// A code that can be used to identify the type of error. Can be used by clients as a key to implement conditional flow.
/// </param>
/// <param name="ErrorMessage">A message describing the error.</param>
public record FailureState(string ErrorCode, string ErrorMessage);
