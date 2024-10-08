﻿namespace SitrepExamples.Api.DependencyInjection;

public static class ExamplesServiceCollectionExtensions
{
    public static IServiceCollection AddExamples(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(jsonOptions =>
        {
            jsonOptions.SerializerOptions.PropertyNameCaseInsensitive = true;
            jsonOptions.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonOptions.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}