using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class BlockTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var block = new Block();

        block.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var block = new Block();

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Operand.Should().BeNull();
        block.Type.Should().BeNull();
        block.VisiblePins.Should().BeEmpty();
        block.HideDesc.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.Block);
        block.Cell.Should().Be("A1");
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
    }

    [Test]
    public void New_Element_ShouldNotBeNull()
    {
        var block = new Block(new XElement(L5XName.Block));

        block.Should().NotBeNull();
    }

    [Test]
    public void New_Overriden_ShouldBeExpected()
    {
        var block = new Block()
        {
            ID = 1, X = 100, Y = 100, Operand = "TestBlock", Type = "SCL",
            VisiblePins = new List<TagName>()
            {
                "Source", "Destination"
            }
        };

        block.ID.Should().Be(1);
        block.X.Should().Be(100);
        block.Y.Should().Be(100);
        block.VisiblePins.Should().HaveCount(2);
    }


    [Test]
    public Task New_Overriden_ShouldBeVerified()
    {
        var block = new Block
        {
            ID = 1, X = 100, Y = 100, Operand = "TestBlock", Type = "SCL",
            VisiblePins = new List<TagName>
            {
                "Source", "Destination"
            }
        };

        return Verify(block.Serialize().ToString());
    }

    [Test]
    public void AddPin_ValidTagName_ShouldHaveExpectedCount()
    {
        var block = new Block();

        block.AddPin("MyPinName");

        block.VisiblePins.Should().HaveCount(1);
    }

    [Test]
    public void AddPins_ValidCollection_ShouldHaveExpectedCount()
    {
        var block = new Block();
        var pins = new List<TagName>()
        {
            "Pin1", "Pin2", "Pin3"
        };

        block.AddPins(pins);

        block.VisiblePins.Should().HaveCount(3);
    }

    [Test]
    public void References_WhenCalled_ShouldHaveExpectedCount()
    {
        var block = new Block
        {
            ID = 1, X = 100, Y = 100,
            Operand = "MyTagName", Type = "SCL",
            VisiblePins = new List<TagName> { "Source", "Destination" }
        };

        var references = block.References().ToList();

        references.Should().HaveCount(3);
    }
}