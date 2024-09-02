namespace Sitrep.ApiClient.Ticketing;

/// <summary>
/// Describes the current state of an asynchronous process.
/// </summary>
public enum ProcessingState
{
    /// <summary>
    /// The process has not started yet.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The process has started, but pending completion.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// The process completed with a successful outcome.
    /// </summary>
    Succeeded = 2,

    /// <summary>
    /// The process completed but failed.
    /// </summary>
    Failed = 3
}
