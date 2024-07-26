namespace SitRep.DependencyInjection;

/// <summary>
/// Provides registration of default implementations.
/// </summary>
public static class SitRepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, an in-memory ticket store.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep(configureOptions => configureOptions.UseInMemoryTicketStore());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep(configureOptions => configureOptions.UseInMemoryTicketStore(storeOptions => { // Do something with storeOptions }));
    /// </code>
    /// </example>
    public static SitRepOptionsBuilder UseInMemoryTicketStore(this SitRepOptionsBuilder optionsBuilder,
                                                              Action<InMemoryTicketStoreOptions>? configureOptions = null)
    {
        optionsBuilder.Services.AddSingleton<ITicketStore, InMemoryTicketStore>();
        optionsBuilder.Services.AddOptions<InMemoryTicketStoreOptions>();

        if (configureOptions != null)
        {
            optionsBuilder.Services.Configure(configureOptions);
        }

        return optionsBuilder;
    }
}
