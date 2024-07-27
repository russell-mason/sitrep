namespace Sitrep.AspNetCore.DependencyInjection;

/// <summary>
/// Provides registration of ASP.NET features.
/// </summary>
public static class SitrepOptionsBuilderExtensions
{
    /// <summary>
    /// Registers, and allows customization of, ASP.NET features.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to extend.</param>
    /// <param name="configureOptions">An optional callback allowing custom configuration.</param>
    /// <returns>The builder to allow additional chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseAspNetCore());
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// builder.Services.AddSitrep(configureOptions => configureOptions.UseAspNetCore(aspOptions => { // Do something with aspOptions }));
    /// </code>
    /// </example>
    public static SitrepOptionsBuilder UseAspNetCore(this SitrepOptionsBuilder optionsBuilder,
                                                     Action<AspNetCoreOptions>? configureOptions = null)
    {
        optionsBuilder.Services.AddOptions<AspNetCoreOptions>();

        if (configureOptions != null)
        {
            optionsBuilder.Services.Configure(configureOptions);
        }

        return optionsBuilder;
    }
}