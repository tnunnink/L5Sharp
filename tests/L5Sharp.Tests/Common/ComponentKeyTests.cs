using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;

namespace L5Sharp.Tests.Common;

[TestFixture]
public class ComponentKeyTests
{
    [Test]
    public void New_NullType_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new ComponentKey(null!, "")).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void New_NullName_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new ComponentKey("", null!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void New_EmptyTypeAndName_ShouldNotBeNull()
    {
        var key = new ComponentKey("", "");

        key.Should().NotBeNull();
    }

    [Test]
    public void New_ValidTypeNamePair_ShouldHaveExpectedValues()
    {
        var key = new ComponentKey("Type", "Name");

        key.Should().NotBeNull();
        key.Should().BeEquivalentTo(new ComponentKey("Type", "Name"));
    }

    [Test]
    public void HasName_ItDoesHaveTheName_ShouldBeTrue()
    {
        var key = new ComponentKey("Type", "Name");

        var result = key.HasName("Name");

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasName_ItDoesNotHaveTheName_ShouldBeFalse()
    {
        var key = new ComponentKey("Type", "Name");

        var result = key.HasName("Nope");

        result.Should().BeFalse();
    }
    
    [Test]
    public void IsType_ByNameItIsTheType_ShouldBeTrue()
    {
        var key = new ComponentKey("Type", "Name");

        var result = key.IsType("Type");

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsType_ByNameItIsNotTheType_ShouldBeFalse()
    {
        var key = new ComponentKey("Type", "Name");

        var result = key.IsType("Nope");

        result.Should().BeFalse();
    }
    
    [Test]
    public void IsType_ByParameterItIsTheType_ShouldBeTrue()
    {
        var key = new ComponentKey("DataType", "MyType");

        var result = key.IsType<DataType>();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsType_ByParameterItIsNotTheType_ShouldBeFalse()
    {
        var key = new ComponentKey("DataType", "MYType");

        var result = key.IsType<Tag>();

        result.Should().BeFalse();
    }
}