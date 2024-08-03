namespace Sitrep.SignalR.DependencyInjection;

/// <summary>
/// Provides registration of SignalR implementations.
/// </summary>
public static class SitrepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, ticket change notification using SignalR.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseSignalRNotification());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseSignalRNotification(notificationOptions => { // Do something with notificationOptions }));
    /// </code>
    /// </example>
    public static SitrepOptionsBuilder UseSignalRNotifications(this SitrepOptionsBuilder optionsBuilder,
                                                              Action<SignalRNotificationOptions>? configureOptions = null)
    {
        optionsBuilder.Services.AddSingleton<ITicketNotification, SignalRTicketNotification>();
        optionsBuilder.Services.AddSingleton<IUserIdProvider, FakeUserIdProvider>();
        optionsBuilder.Services.AddSignalR();
        optionsBuilder.Services.AddOptions<SignalRNotificationOptions>();

        if (configureOptions != null)
        {
            optionsBuilder.Services.Configure(configureOptions);
        }
        
        optionsBuilder.Services.PostConfigure<JsonHubProtocolOptions>(configure =>
        {
            // Need options to be resolved at this point, any changes after this will not be reflected.
            var jsonOptions = optionsBuilder.Services
                                            .BuildServiceProvider()
                                            .GetRequiredService<IOptions<JsonOptions>>().Value;

            configure.PayloadSerializerOptions = jsonOptions.SerializerOptions;
        });

        return optionsBuilder;
    }
}