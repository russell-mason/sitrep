namespace SitRep.DependencyInjection;

public static class SitRepServiceCollectionExtensions
{
    public static IServiceCollection AddSitRep(this IServiceCollection services,
                                               Action<SitRepOptionsBuilder>? configureOptions = null)
    {
        services.AddSingleton<ITicketProcessor, TicketProcessor>();

        configureOptions?.Invoke(new SitRepOptionsBuilder(services));

        return services;
    }
}