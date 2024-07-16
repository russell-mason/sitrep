namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is transitioning to an in-progress state.
/// </summary>
/// <param name="ProgressMessage">A message describing the progress.</param>
public record ProgressState(string ProgressMessage);
