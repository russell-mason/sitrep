namespace Sitrep.DependencyInjection;

/// <summary>
/// Allows extension methods to be chained together for configuring sitrep options.
/// </summary>
public class SitrepOptionsBuilder
{
    /// <summary>
    /// Create a new instance of the SitrepOptionsBuilder class.
    /// </summary>
    /// <param name="services">The services that extension methods can use for registration.</param>
    public SitrepOptionsBuilder(IServiceCollection services)
    {
        services.AddOptions<SitrepOptions>();

        // For internal configuration
        services.PostConfigure<SitrepOptions>(options => Ticket.ExpirationPeriodInMinutes = options.TicketExpirationPeriodInMinutes);

        Services = services;
    }

    /// <summary>
    /// Gets the services collection allowing registration from within extensions methods.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Provides a callback to configure the sitrep options.
    /// </summary>
    /// <param name="configureOptions">The method to call when options have been configured for the first time.</param>
    /// <returns>The current builder allowing additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.Configure(sitrepOptions => { // Do something with sitrepOptions }));
    /// </code>
    /// </example>
    public SitrepOptionsBuilder Configure(Action<SitrepOptions> configureOptions)
    {
        // For external configuration
        Services.Configure(configureOptions);

        return this;
    }
}