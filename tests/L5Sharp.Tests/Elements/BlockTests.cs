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
        var block = new Block("Test");

        block.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var block = new Block("SCL");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("SCL");
        block.Operand.Should().Be("SCL_01");
        block.VisiblePins.Should().BeEmpty();
        block.HideDesc.Should().BeFalse();
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
        var block = new Block("Test")
        {
            ID = 1, X = 100, Y = 100, 
            Operand = "TestBlock",
            VisiblePins = new Params
            {
                "Source", "Destination"
            }
        };

        block.ID.Should().Be(1);
        block.X.Should().Be(100);
        block.Y.Should().Be(100);
        block.Type.Should().Be("SCL");
        block.Operand.Should().Be("TestBlock");
        block.VisiblePins.Should().HaveCount(2);
    }


    [Test]
    public Task New_Overriden_ShouldBeVerified()
    {
        var block = new Block("SCL")
        {
            ID = 1, X = 100, Y = 100, 
            Operand = "TestBlock",
            VisiblePins = new Params
            {
                "Source", "Destination"
            }
        };

        return Verify(block.Serialize().ToString());
    }

    [Test]
    public void FactoryMethod_WhenCalled_ShouldHaveExpectedValues()
    {
        var block = Block.ADD;

        block.Type.Should().Be("ADD");
        block.Operand.Should().Be("ADD_01");
        block.VisiblePins.Should().HaveCount(3);
    }

    [Test]
    public void AddPin_ValidTagName_ShouldHaveExpectedCount()
    {
        var block = Block.ABS;

        block.VisiblePins?.Add("MyPinName");

        block.VisiblePins.Should().HaveCount(1);
    }

    [Test]
    public void References_WhenCalled_ShouldHaveExpectedCount()
    {
        var block = Block.ABS;

        var references = block.References().ToList();

        references.Should().HaveCount(3);
    }
}