namespace Sitrep;

/// <summary>
/// Represents an exception where the message doesn't contain any sensitive information and so can be
/// safely shown publicly.
/// </summary>
[Serializable]
public class PublicException: Exception
{
    /// <summary>
    /// Creates a new instance of the PublicException class.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// <para>
    /// Ensure the message does not contain any sensitive information.
    /// </para>
    /// </param>
    public PublicException(string message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new instance of the PublicException class.
    /// <para>
    /// Only the message for this exception is expected to be safe, the inner exception will not be shown so
    /// may contain sensitive information.
    /// </para>
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// <para>
    /// Ensure the message does not contain any sensitive information.
    /// </para>
    /// </param>
    /// <param name="inner">The exception that originated the current issue.</param>
    public PublicException(string message, Exception inner) : base(message, inner)
    {
    }
}