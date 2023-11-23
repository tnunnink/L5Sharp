using FluentAssertions;
using L5Sharp.Elements;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class ICONTests
{
    [Test]
    public void New_ValidName_ShouldNotBeNull()
    {
        var icon = new ICON("Test");

        icon.Should().NotBeNull();
    }
    
    [Test]
    public void New_ValidName_ShouldHaveExpectedValues()
    {
        var icon = new ICON("Test");

        icon.Name.Should().Be("Test");
        icon.ID.Should().Be(0);
        icon.X.Should().Be(0);
        icon.Y.Should().Be(0);
        icon.Pair.Should().BeNull();
    }
}