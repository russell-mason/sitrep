namespace Sitrep.Tests.Configuration;

[TestFixture]
public class SitrepOptionsBuilderTests
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
        var sitrepOptionsBuilder = new SitrepOptionsBuilder(_serviceCollection);

        // Assert
        sitrepOptionsBuilder.Services.Should().BeSameAs(_serviceCollection);
    }

    [Test]
    public void Constructor_RegistersSitrepOptions()
    {
        // Arrange

        // Act
        _ = new SitrepOptionsBuilder(_serviceCollection);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IOptions<SitrepOptions>>();

        options.Should().NotBeNull();
        options.Value.Should().NotBeNull();
    }

    [Test]
    public void Constructor_SetsTicketExpirationPeriodInMinutes()
    {
        // Arrange
        var randomizer = new Randomizer();
        var periodInMinutes = randomizer.Int(0, 43200); // 30 days
        var configureOptions = (SitrepOptions options) => { options.TicketExpirationPeriodInMinutes = periodInMinutes; };

        // Act
        _ = new SitrepOptionsBuilder(_serviceCollection).Configure(configureOptions);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IConfigureOptions<SitrepOptions>>();

        var sitrepOptions = new SitrepOptions();

        var action = ((ConfigureNamedOptions<SitrepOptions>)options).Action!;
        action(sitrepOptions);

        var postOptions = serviceProvider.GetRequiredService<IPostConfigureOptions<SitrepOptions>>();

        var postAction = ((PostConfigureOptions<SitrepOptions>)postOptions).Action!;
        postAction(sitrepOptions);

        Ticket.ExpirationPeriodInMinutes.Should().Be(periodInMinutes);
    }

    [Test]
    public void Configure_WhenConfigureOptionsCallbackIsSpecified_ThenCallbackIsSet()
    {
        // Arrange
        var sitrepOptionsBuilder = new SitrepOptionsBuilder(_serviceCollection);

        var configureOptions = (SitrepOptions options) => { };

        // Act
        sitrepOptionsBuilder.Configure(configureOptions);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IConfigureOptions<SitrepOptions>>();

        var action = ((ConfigureNamedOptions<SitrepOptions>)options).Action;

        action.Should().Be(configureOptions);
    }
}