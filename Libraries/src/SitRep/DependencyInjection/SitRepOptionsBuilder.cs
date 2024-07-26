namespace SitRep.DependencyInjection;

/// <summary>
/// Allows extension methods to be chained together for configuring sitrep options.
/// </summary>
public class SitRepOptionsBuilder
{
    /// <summary>
    /// Create a new instance of the SitRepOptionsBuilder class.
    /// </summary>
    /// <param name="services">The services that extension methods can use for registration.</param>
    public SitRepOptionsBuilder(IServiceCollection services)
    {
        services.AddOptions<SitRepOptions>();

        // For internal configuration
        services.PostConfigure<SitRepOptions>(options => Ticket.ExpirationPeriodInMinutes = options.TicketExpirationPeriodInMinutes);

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
    /// builder.Services.AddSitRep(configureOptions => configureOptions.Configure(sitrepOptions => { // Do something with sitrepOptions }));
    /// </code>
    /// </example>
    public SitRepOptionsBuilder Configure(Action<SitRepOptions> configureOptions)
    {
        // For external configuration
        Services.Configure(configureOptions);

        return this;
    }
}