using System.Xml.Linq;
using FluentAssertions;


namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class WireTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var wire = new Wire();

        wire.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var wire = new Wire();

        wire.FromID.Should().Be(0);
        wire.ToID.Should().Be(0);
        wire.FromParam.Should().BeNull();
        wire.ToParam.Should().BeNull();
    }

    [Test]
    public void New_ValidElement_ShouldNotBeNull()
    {
        var wire = new Wire(new XElement(L5XName.Wire));

        wire.Should().NotBeNull();
    }

    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new Wire(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_Overloaded_ShouldHaveExpectedValues()
    {
        var wire = new Wire
        {
            FromID = 1,
            ToID = 2,
            FromParam = "In",
            ToParam = "Out"
        };

        wire.FromID.Should().Be(1);
        wire.ToID.Should().Be(2);
        wire.FromParam.Should().Be("In");
        wire.ToParam.Should().Be("Out");
    }
}