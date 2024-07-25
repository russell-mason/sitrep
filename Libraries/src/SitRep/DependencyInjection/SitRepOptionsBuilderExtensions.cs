namespace SitRep.DependencyInjection;

public static class SitRepOptionsBuilderExtensions
{
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
