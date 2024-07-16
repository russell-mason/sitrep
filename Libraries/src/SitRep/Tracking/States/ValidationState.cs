namespace SitRep.Tracking.States;

/// <summary>
/// Specifies details when a ticket is transitioning to a completed failed state due to validation errors.
/// </summary>
/// <param name="ValidationErrors">
/// A list of validation errors.
/// The key should be a property name, and the values should be the validation errors associated with that property.
/// </param>
/// <param name="ValidationMessage">A message describing the validation source.</param>
public record ValidationState(Dictionary<string, string[]> ValidationErrors, string ValidationMessage);
