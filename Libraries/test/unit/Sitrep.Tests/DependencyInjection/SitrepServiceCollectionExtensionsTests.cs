namespace Sitrep.Tests.DependencyInjection;

[TestFixture]
public class SitrepServiceCollectionExtensionsTests
{
    private ServiceCollection _serviceCollection;
    private Mock<ITicketStore> _ticketStoreMock;

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = [];
        _ticketStoreMock = new Mock<ITicketStore>();

        _serviceCollection.AddSingleton(_ticketStoreMock.Object);
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