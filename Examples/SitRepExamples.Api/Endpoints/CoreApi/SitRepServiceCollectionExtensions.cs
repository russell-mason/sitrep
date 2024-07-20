namespace SitRepExamples.Api.Endpoints.CoreApi;

public static class SitRepServiceCollectionExtensions
{
    public static IServiceCollection AddSitRep(this IServiceCollection services)
    {
        ConfigureJson(services);
        RegisterServices(services);

        return services;
    }

    private static void ConfigureJson(IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ITicketTrackingStore, InMemoryTicketTrackingStore>();
        services.AddSingleton<ITicketTracker, TicketTracker>();
    }
}