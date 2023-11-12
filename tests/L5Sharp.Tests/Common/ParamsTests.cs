using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common;

[TestFixture]
public class ParamsTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        var test = new Params();

        test.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldBeEmpty()
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        var test = new Params();

        test.Should().BeEmpty();
    }

    [Test]
    public void New_ValidName_ShouldNotBeNull()
    {
        var pins = new Params("Pins");

        pins.Should().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new Params(((string)null)!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_EmptyName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new Params(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_Scenario_Expected()
    {
    }

    [Test]
    public void Add_ValidString_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var test = new Params("Test");

        test.Add("Parameter1");

        test.Should().HaveCount(1);
    }

    [Test]
    public void Clear_Default_ShouldBeEmpty()
    {
        var test = new Params("Test");

        test.Clear();

        test.Should().HaveCount(0);
    }

    [Test]
    public void Clear_HasItems_ShouldBeEmpty()
    {
        var attribute = new XAttribute("Test", "Pin1 Pin2 Pin3");
        // ReSharper disable once CollectionNeverUpdated.Local
        var test = new Params(attribute);

        test.Clear();

        test.Should().HaveCount(0);
    }

    [Test]
    public void Count_WithNoItems_ShouldBeZero()
    {
        var test = new Params("Test");

        var count = test.Count;

        count.Should().Be(0);
    }

    [Test]
    public void Contains_HasItem_ShouldBeTrue()
    {
        var test = new Params { "Test" };

        var result = test.Contains("Test");

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_DoesntHaveItem_ShouldBeFalse()
    {
        var test = new Params { "Test" };

        var result = test.Contains("Fake");

        result.Should().BeFalse();
    }

    [Test]
    public void Remove_NoItem_ShouldBeFalse()
    {
        var test = new Params { "Test" };

        var result = test.Remove("Item");

        result.Should().BeFalse();
    }

    [Test]
    public void Remove_HasItem_ShouldBeTrue()
    {
        var test = new Params { "Test" };

        var result = test.Remove("Test");

        result.Should().BeTrue();
    }

    [Test]
    public void Insert_ValidIndexAndItem_ShouldHaveExpectedCount()
    {
        var test = new Params { "Test" };

        test.Insert(0, "Test");

        test.Should().HaveCount(2);
    }

    [Test]
    public void Insert_InvalidIndex_ShouldThrowOutOfRangeException()
    {
        var test = new Params();
        
        test.Insert(2, "Test");
    }
}