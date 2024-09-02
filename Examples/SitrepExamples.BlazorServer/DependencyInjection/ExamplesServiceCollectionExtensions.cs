namespace SitrepExamples.BlazorServer.DependencyInjection;

public static class ExamplesServiceCollectionExtensions
{
    public static IServiceCollection AddExamples(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(jsonOptions =>
        {
            jsonOptions.PropertyNameCaseInsensitive = true;
            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}