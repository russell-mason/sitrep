namespace SitRep.DependencyInjection;

public class SitRepOptionsBuilder
{
    public SitRepOptionsBuilder(IServiceCollection services)
    {
        services.AddOptions<SitRepOptions>();

        // For internal configuration
        services.PostConfigure<SitRepOptions>(options => Ticket.ExpirationPeriodInMinutes = options.TicketExpirationPeriodInMinutes);

        Services = services;
    }

    public IServiceCollection Services { get; }

    public SitRepOptionsBuilder Configure(Action<SitRepOptions> configureOptions)
    {
        // For external configuration
        Services.Configure(configureOptions);

        return this;
    }
}