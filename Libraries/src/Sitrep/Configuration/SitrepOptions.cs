namespace Sitrep.Configuration;

/// <summary>
/// Provides options for the sitrep ticketing service.
/// </summary>
public class SitrepOptions
{
    /// <summary>
    /// Gets or sets the period of time in minutes that a ticket is valid for.
    /// </summary>
    public int TicketExpirationPeriodInMinutes { get; set; } = 60 * 24 * 7; // 7 days = 10080 minutes;
}