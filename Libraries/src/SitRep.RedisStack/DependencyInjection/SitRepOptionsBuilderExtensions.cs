namespace SitRep.RedisStack.DependencyInjection;

/// <summary>
/// Provides registration of Redis Stack implementations.
/// </summary>
public static class SitRepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, a ticket store that uses Redis Stack.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep(configureOptions => configureOptions.UseRedisStackTicketStore());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitRep(configureOptions => configureOptions.UseRedisStackTicketStore(storeOptions => { // Do something with storeOptions }));
    /// </code>
    /// </example>
    public static SitRepOptionsBuilder UseRedisStackTicketStore(this SitRepOptionsBuilder optionsBuilder,
                                                                Action<RedisStackTicketStoreOptions>? configureOptions = null)
    {
        optionsBuilder.Services.AddSingleton<ITicketStore, RedisStackTicketStore>();

        optionsBuilder.Services.AddOptions<RedisStackTicketStoreOptions>();

        if (configureOptions != null)
        {
            optionsBuilder.Services.Configure(configureOptions);
        }

        return optionsBuilder;
    }
}