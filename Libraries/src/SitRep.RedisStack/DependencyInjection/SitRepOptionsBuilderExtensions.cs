namespace SitRep.RedisStack.DependencyInjection;

public static class SitRepOptionsBuilderExtensions
{
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