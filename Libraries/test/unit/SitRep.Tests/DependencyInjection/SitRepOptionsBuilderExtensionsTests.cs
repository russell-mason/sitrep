namespace SitRep.Tests.DependencyInjection;

[TestFixture]
public class SitRepOptionsBuilderExtensionsTests
{
    private ServiceCollection _serviceCollection;
    private SitRepOptionsBuilder _optionsBuilder;

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = [];
        _optionsBuilder = new SitRepOptionsBuilder(_serviceCollection);
    }

    [Test]
    public void UseInMemoryTicketStore_RegistersInMemoryTicketStore()
    {
        // Arrange

        // Act
        _optionsBuilder.UseInMemoryTicketStore();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        serviceProvider.GetRequiredService<ITicketStore>().Should().BeOfType<InMemoryTicketStore>();
    }

    [Test]
    public void UseInMemoryTicketStore_RegistersInMemoryTicketStoreOptions()
    {
        // Arrange

        // Act
        _optionsBuilder.UseInMemoryTicketStore();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IOptions<InMemoryTicketStoreOptions>>();

        options.Should().NotBeNull();
        options.Value.Should().NotBeNull();
    }

    [Test]
    public void UseInMemoryTicketStore_WhenConfigureOptionsCallbackIsSpecified_ThenCallbackIsSet()
    {
        // Arrange
        var configureOptions = (InMemoryTicketStoreOptions options) => { };

        // Act
        _optionsBuilder.UseInMemoryTicketStore(configureOptions);

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var options = serviceProvider.GetRequiredService<IConfigureOptions<InMemoryTicketStoreOptions>>();

        var action = ((ConfigureNamedOptions<InMemoryTicketStoreOptions>) options).Action;

        action.Should().Be(configureOptions);
    }

    [Test]
    public void UseInMemoryTicketStore_WhenConfigureOptionsCallbackIsNotSpecified_ThenNoCallbackIsSet()
    {
        // Arrange
        
        // Act
        _optionsBuilder.UseInMemoryTicketStore();

        // Assert
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var action = () => serviceProvider.GetRequiredService<IConfigureOptions<InMemoryTicketStoreOptions>>();

        action.Should().Throw<InvalidOperationException>().WithMessage("No service for type * has been registered.");
    }
}