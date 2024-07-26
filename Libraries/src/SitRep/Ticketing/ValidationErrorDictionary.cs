namespace SitRep.Ticketing;

/// <summary>
/// Represents a list of validation errors where the key is a property name, and the values are the
/// validation errors associated with that property.
/// </summary>
public class ValidationErrorDictionary : Dictionary<string, string[]>
{
    /// <summary>
    /// Creates a new instance of the ValidationErrorDictionary class.
    /// </summary>
    public ValidationErrorDictionary()
    {
    }

    /// <summary>
    /// Creates a new instance of the ValidationErrorDictionary class from an existing dictionary providing
    /// a shallow copy. N.B. The content of the dictionary is still mutable.
    /// </summary>
    /// <param name="errors">The validation errors to copy.</param>
    public ValidationErrorDictionary(ValidationErrorDictionary errors) : base(errors)
    {
    }
}