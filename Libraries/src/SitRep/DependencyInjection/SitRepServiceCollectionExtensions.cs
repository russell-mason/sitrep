namespace SitRep.DependencyInjection;

/// <summary>
/// Provides registration of sitrep services.
/// </summary>
public static class SitRepServiceCollectionExtensions
{
    /// <summary>
    /// Adds, and allows customization of, core sitrep services to the service collection.
    /// </summary>
    /// <param name="services">The services collection to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The services to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep();
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep(configureOptions => { // Do something with configureOptions });
    /// </code>
    /// </example>
    public static IServiceCollection AddSitRep(this IServiceCollection services,
                                               Action<SitRepOptionsBuilder>? configureOptions = null)
    {
        services.AddSingleton<ITicketProcessor, TicketProcessor>();

        configureOptions?.Invoke(new SitRepOptionsBuilder(services));

        return services;
    }
}