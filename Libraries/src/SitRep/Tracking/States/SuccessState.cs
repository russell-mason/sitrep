namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is transitioning to a completed successful state.
/// </summary>
/// <param name="SuccessMessage">A message describing the final state.</param>
/// <param name="ResourceIdentifier">
/// A URL to the resource that was created. If no resource was produced, this should be null.
/// </param>
public record SuccessState(string SuccessMessage, string? ResourceIdentifier);
