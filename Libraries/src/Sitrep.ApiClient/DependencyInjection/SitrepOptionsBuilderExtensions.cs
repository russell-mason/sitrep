namespace Sitrep.ApiClient.DependencyInjection;

/// <summary>
/// Provides registration of AlpClient features.
/// </summary>
public static class SitrepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, ApiClient features.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseCoreApiClient());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseCoreApiClient(coreApiOptions => { // Do something with coreApiOptions }));
    /// </code>
    /// </example>
    public static SitrepOptionsBuilder UseCoreApiClient(this SitrepOptionsBuilder optionsBuilder)
    {
        optionsBuilder.Services
                      .AddRefitClient<ISitrepCoreApi>()
                      .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7115/"));

        return optionsBuilder;
    }
}