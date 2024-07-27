namespace Sitrep.Tests.Tracking;

[TestFixture]
public class ValidationErrorDictionaryTests
{
    [Test]
    public void Constructor_IsCreatedEmpty()
    {
        // Arrange

        // Act
        var validationErrorDictionary = new ValidationErrorDictionary();

        // Assert
        validationErrorDictionary.Should().BeEmpty();
    }

    [Test]
    public void Constructor_WhenCreatedFromExistingInstance_ThenContainsCopy()
    {
        // Arrange
        const string key1 = "key1";
        const string key2 = "key2";

        string[] values1 = ["value1", "value2"];
        string[] values2 = ["value1", "value2", "value3"];

        var existingDictionary = new ValidationErrorDictionary
                                 {
                                     { key1, values1 },
                                     { key2, values2 }
                                 };

        // Act
        var validationErrorDictionary = new ValidationErrorDictionary(existingDictionary);

        // Assert
        validationErrorDictionary.Count.Should().Be(2);
        validationErrorDictionary["key1"].Length.Should().Be(2);
        validationErrorDictionary["key2"].Length.Should().Be(3);
    }

    [Test]
    public void Add_PerformsBaseFunctionAsNormal()
    {
        const string key = "key";

        string[] values = ["value1", "value2"];

        // Arrange
        var validationErrorDictionary = new ValidationErrorDictionary();

        // Act
        validationErrorDictionary.Add(key, values);

        // Assert
        validationErrorDictionary.Count.Should().Be(1);
        validationErrorDictionary["key"].Should().BeEquivalentTo(values);
    }
}