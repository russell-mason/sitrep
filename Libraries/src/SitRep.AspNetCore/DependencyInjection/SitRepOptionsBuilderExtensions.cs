namespace SitRep.AspNetCore.DependencyInjection;

public static class SitRepOptionsBuilderExtensions
{
    public static SitRepOptionsBuilder UseAspNetCore(this SitRepOptionsBuilder optionsBuilder,
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