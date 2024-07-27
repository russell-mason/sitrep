namespace Sitrep.DependencyInjection;

/// <summary>
/// Provides registration of sitrep services.
/// </summary>
public static class SitrepServiceCollectionExtensions
{
    /// <summary>
    /// Adds, and allows customization of, core sitrep services to the service collection.
    /// </summary>
    /// <param name="services">The services collection to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The services to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep();
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => { // Do something with configureOptions });
    /// </code>
    /// </example>
    public static IServiceCollection AddSitrep(this IServiceCollection services,
                                               Action<SitrepOptionsBuilder>? configureOptions = null)
    {
        services.AddSingleton<ITicketProcessor, TicketProcessor>();

        configureOptions?.Invoke(new SitrepOptionsBuilder(services));

        return services;
    }
}