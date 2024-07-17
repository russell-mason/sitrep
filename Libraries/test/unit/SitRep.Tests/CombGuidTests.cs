namespace SitRep.Tests;

[TestFixture]
public class CombGuidTests
{
    [SetUp]
    public void Setup()
    {
        CombGuid.ResetGuidGeneratorFunction();
    }

    [Test]
    public void NewGuid_UsesGeneratorFunction()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var wasCalled = false;

        CombGuid.GuidGeneratorFunction = GeneratorFunction;

        // Act
        var result = CombGuid.NewGuid();

        // Assert
        result.Should().Be(guid);
        wasCalled.Should().BeTrue();

        return;

        Guid GeneratorFunction()
        {
            wasCalled = true;
            return guid;
        }
    }

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
        var result = Enumerable.Range(0, quantity).Select(_ => CombGuid.NewGuid()).ToList();

        // Assert
        var ordered = CombGuid.Order(result).ToList();

        result.Should().BeEquivalentTo(ordered, options => options.WithStrictOrdering());
    }

    [Test]
    public void Order_WhenSmallArray_ThenSorts()
    {
        // Arrange
        var first = CombGuid.NewGuid();
        var second = CombGuid.NewGuid();
        var third = CombGuid.NewGuid();
        
        // Act
        var ordered = CombGuid.Order([second, third, first]).ToList();

        // Assert
        ordered[0].Should().Be(first);
        ordered[1].Should().Be(second);
        ordered[2].Should().Be(third);
    }

    [Test]
    public void Order_WhenLargeRandomizedList_ThenSorts()
    {
        // Arrange
        var randomizer = new Randomizer();
        var list = Enumerable.Range(0, 1000).Select(_ => CombGuid.NewGuid()).ToList();
        var shuffled = randomizer.Shuffle(list);

        // Act
        var ordered = CombGuid.Order(shuffled).ToList();

        // Assert
        ordered.Should().BeEquivalentTo(list, options => options.WithStrictOrdering());
    }

    [Test]
    public void ResetGuidGeneratorFunction_ClearsCustomFunction()
    {
        // Arrange
        CombGuid.GuidGeneratorFunction = GeneratorFunction;

        // Act
        CombGuid.ResetGuidGeneratorFunction();

        // Assert
        CombGuid.GuidGeneratorFunction.Should().NotBeSameAs(GeneratorFunction);

        return;

        Guid GeneratorFunction()
        {
            return Guid.NewGuid();
        }
    }
}
