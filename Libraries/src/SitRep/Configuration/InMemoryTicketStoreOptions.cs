namespace SitRep.Configuration;

/// <summary>
/// Provides options for the in-memory ticket store.
/// <para>
/// You should not rely on the exact number of tickets in the store, nor when tickets are removed, or how many.
/// These settings keep the memory usage in check and follow some basic rules to remove expired/old tickets at
/// regular intervals.
/// </para>
/// </summary>
public class InMemoryTicketStoreOptions
{
    /// <summary>
    /// Gets or sets whether tickets whose expiration date has passed are automatically removed whether
    /// the threshold has been met or not.
    /// </summary>
    public bool DiscardExpired { get; set; } = true;

    /// <summary>
    /// Gets or sets the number of tickets that can be stores before a check is made to determine if the
    /// oldest tickets should be removed.
    /// <para>
    /// N.B. Only closed tickets are considered for removal. Therefore, the number of tickets in the store
    /// may exceed this value.
    /// </para>
    /// </summary>
    public int DiscardThreshold { get; set; } = 1000;

    /// <summary>
    /// Gets or sets how many tickets should be removed once the number of tickets in the store exceeds
    /// the DiscardThreshold. The oldest tickets will be removed first.
    /// <para>
    /// This is the number to remove below the threshold, not in total, e.g. if the DiscardThreshold is 100 and the
    /// DiscardCount is 50 and DiscardInterval is 10. When there are 100 tickets, the check will still miss,
    /// as 100 does not excess the threshold so no action is taken. The next check at 110 tickets, will exceed the
    /// threshold so 60 tickets will be removed, so th.
    /// </para>
    /// </summary>
    public int DiscardCount { get; set; } = 100;

    /// <summary>
    /// Gets or sets how often a check for expired tickets should be made. To prevent a check from being
    /// made every time a ticket is added/updated, the check will only occur every time this number of
    /// additions/updates have been made.
    /// <para>
    /// N.B. Because this is only checked periodically, the number of tickets in the store may exceed the
    /// value set by DiscardThreshold.
    /// </para>
    /// </summary>
    public int DiscardInterval { get; set; } = 100;
}