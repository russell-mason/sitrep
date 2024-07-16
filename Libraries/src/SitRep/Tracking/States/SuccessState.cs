namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is transitioning to a completed successful state.
/// </summary>
/// <param name="ResourceIdentifier">
/// A URL to the resource that was created. If no resource was produced, this should be null.
/// </param>
/// <param name="SuccessMessage">A message describing the final state.</param>
public record SuccessState(string? ResourceIdentifier, string SuccessMessage);
