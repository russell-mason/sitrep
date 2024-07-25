namespace SitRep.Tests.DependencyInjection;

[TestFixture]
public class SitRepServiceCollectionExtensionsTests
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
    public void AddSitRep_RegistersTicketProcessor()
    {
        // Arrange

        // Act
        _serviceCollection.AddSitRep();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        serviceProvider.GetRequiredService<ITicketProcessor>().Should().BeOfType<TicketProcessor>();
    }

    [Test]
    public void AddSitRep_WhenConfigureOptionsCallbackIsSpecified_ThenCallbackIsSet()
    {
        // Arrange
        var wasInvoked = false;

        var configureOptions = (SitRepOptionsBuilder options) => { wasInvoked = true; };

        // Act
        _serviceCollection.AddSitRep(configureOptions);

        // Assert
        wasInvoked.Should().Be(true);
    }
}
