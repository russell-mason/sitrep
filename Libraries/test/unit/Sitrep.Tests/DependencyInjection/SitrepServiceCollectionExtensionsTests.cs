namespace Sitrep.Tests.DependencyInjection;

[TestFixture]
public class SitrepServiceCollectionExtensionsTests
{
    private ServiceCollection _serviceCollection;

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = [];
    }

    [Test]
    public void AddSitrep_RegistersTicketProcessor()
    {
        // Arrange

        // Act
        _serviceCollection.AddSitrep();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        serviceProvider.GetRequiredService<ITicketProcessor>().Should().BeOfType<TicketProcessor>();
    }

    [Test]
    public void AddSitrep_RegistersDefaultsTicketStore()
    {
        // Arrange

        // Act
        _serviceCollection.AddSitrep();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        serviceProvider.GetRequiredService<ITicketStore>().Should().BeOfType<InMemoryTicketStore>();
    }

    [Test]
    public void AddSitrep_RegistersDefaultTicketNotification()
    {
        // Arrange

        // Act
        _serviceCollection.AddSitrep();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        serviceProvider.GetRequiredService<ITicketNotification>().Should().BeOfType<TicketNotification>();
    }

    [Test]
    public void AddSitrep_WhenConfigureOptionsCallbackIsSpecified_ThenCallbackIsSet()
    {
        // Arrange
        var wasInvoked = false;

        var configureOptions = (SitrepOptionsBuilder options) => { wasInvoked = true; };

        // Act
        _serviceCollection.AddSitrep(configureOptions);

        // Assert
        wasInvoked.Should().Be(true);
    }
}