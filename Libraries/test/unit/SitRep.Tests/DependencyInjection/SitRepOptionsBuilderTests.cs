namespace SitRep.Tests.DependencyInjection;

[TestFixture]
public class SitRepOptionsBuilderTests
{
    private ServiceCollection _serviceCollection;

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = [];
    }

    [Test]
    public void Constructor_SetsServicesProperty()
    {
        // Arrange
        
        // Act
        var sitRepOptionsBuilder = new SitRepOptionsBuilder(_serviceCollection);

        // Assert
        sitRepOptionsBuilder.Services.Should().BeSameAs(_serviceCollection);
    }

    [Test]
    public void Constructor_RegistersSitRepOptions()
    {
        // Arrange

        // Act
        _ = new SitRepOptionsBuilder(_serviceCollection);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IOptions<SitRepOptions>>();

        options.Should().NotBeNull();
        options.Value.Should().NotBeNull();
    }

    [Test]
    public void Constructor_SetsTicketExpirationPeriodInMinutes()
    {
        // Arrange
        var randomizer = new Randomizer();
        var periodInMinutes = randomizer.Int();
        var configureOptions = (SitRepOptions options) => { options.TicketExpirationPeriodInMinutes = periodInMinutes; };

        // Act
        _ = new SitRepOptionsBuilder(_serviceCollection).Configure(configureOptions);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IConfigureOptions<SitRepOptions>>();

        var sitRepOptions = new SitRepOptions();

        var action = ((ConfigureNamedOptions<SitRepOptions>) options).Action!;
        action(sitRepOptions);

        var postOptions = serviceProvider.GetRequiredService<IPostConfigureOptions<SitRepOptions>>();

        var postAction = ((PostConfigureOptions<SitRepOptions>) postOptions).Action!;
        postAction(sitRepOptions);

        Ticket.ExpirationPeriodInMinutes.Should().Be(periodInMinutes);
    }

    [Test]
    public void Configure_WhenConfigureOptionsCallbackIsSpecified_ThenCallbackIsSet()
    {
        // Arrange
        var sitRepOptionsBuilder = new SitRepOptionsBuilder(_serviceCollection);

        var configureOptions = (SitRepOptions options) => { };
        
        // Act
        sitRepOptionsBuilder.Configure(configureOptions);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IConfigureOptions<SitRepOptions>>();

        var action = ((ConfigureNamedOptions<SitRepOptions>) options).Action;

        action.Should().Be(configureOptions);
    }
}
