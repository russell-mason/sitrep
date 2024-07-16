namespace SitRep.Tracking;

/// <summary>
/// Describes each stage of an asynchronous process.
/// </summary>
public enum ProcessingStage
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
