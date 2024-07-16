namespace SitRep.Tests;

[TestFixture]
public class CombGuidTests
{
    [Test]
    public void NewGuid_CreatesGuid()
    {
        // Arrange

        // Act
        var result = CombGuid.NewGuid();

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void NewGuid_CreatesCompatibleGuid()
    {
        // Arrange
        var text = CombGuid.NewGuid().ToString();

        // Act
        var result = new Guid(text);

        // Assert
        result.Should().NotBe(Guid.Empty);
        result.ToString().Should().Be(text);
    }

    [Test]
    public void NewGuid_CreatesGuidsInOrder()
    {
        // Arrange
        const int quantity = 25;

        // Act
        var result = Enumerable.Repeat(CombGuid.NewGuid(), quantity).ToList();

        // Assert
        result.Should().BeInAscendingOrder();
    }
}
