namespace Sitrep.DependencyInjection;

/// <summary>
/// Provides registration of default implementations.
/// </summary>
public static class SitrepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, an in-memory ticket store.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseInMemoryTicketStore());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseInMemoryTicketStore(storeOptions => { // Do something with storeOptions }));
    /// </code>
    /// </example>
    public static SitrepOptionsBuilder UseInMemoryTicketStore(this SitrepOptionsBuilder optionsBuilder, 
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