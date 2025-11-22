using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class BlockTests
{
    [Test]
    public void New_IRef_ShouldHaveExpectedValues()
    {
        var block = Block.IREF("MyTag");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Cell.Should().Be("A1");
        block.Type.Should().Be("IRef");
        block.Operand.Should().Be("MyTag");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Arguments.Should().BeEmpty();
        block.Inputs.Should().BeEmpty();
        block.Outputs.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.IRef);
    }

    [Test]
    public void New_ORef_ShouldHaveExpectedValues()
    {
        var block = Block.OREF("MyTag");

        var another = Block.ADD("MyAddFunction");


        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Cell.Should().Be("A1");
        block.Type.Should().Be("ORef");
        block.Operand.Should().Be("MyTag");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Arguments.Should().BeEmpty();
        block.Inputs.Should().BeEmpty();
        block.Outputs.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.ORef);
    }

    [Test]
    public void New_ICon_ShouldHaveExpectedValues()
    {
        var block = Block.ICON("Connector");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Cell.Should().Be("A1");
        block.Type.Should().Be("ICon");
        block.Operand.Should().Be("Connector");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Arguments.Should().BeEmpty();
        block.Inputs.Should().BeEmpty();
        block.Outputs.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.ICon);
    }

    [Test]
    public void New_OCon_ShouldHaveExpectedValues()
    {
        var block = Block.OCON("Connector");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Cell.Should().Be("A1");
        block.Type.Should().Be("OCon");
        block.Operand.Should().Be("Connector");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.OCon);
    }

    [Test]
    public void New_Block_ShouldHaveDefaultValues()
    {
        var block = Block.SCL();

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Cell.Should().Be("A1");
        block.Type.Should().Be("SCL");
        block.Operand.Should().Be("SCL_01");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.Block);
    }

    [Test]
    public void New_Function_ShouldHaveDefaultValues()
    {
        var block = Block.ADD__F();

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("ADD__F");
        block.Operand.Should().BeNull();
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.Function);
        block.Cell.Should().Be("A1");
    }

    [Test]
    public void New_AOI_ShouldHaveExpectedValues()
    {
        var block = Block.AOI("aoiTest", "MyAoiTag", "Pin1", "Pin2", "Pin3");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("aoiTest");
        block.Operand.Should().Be("MyAoiTag");
        block.HideDesc.Should().BeFalse();
        block.Pins.Should().HaveCount(3);
        block.Sheet.Should().BeNull();
        block.Serialize().Name.Should().Be(L5XName.AddOnInstruction);
        block.Cell.Should().Be("A1");
    }

    [Test]
    public void Wire_ValidSyntax_ShouldBeExpected()
    {
        var block = Block.ABS("ABS01");
    }

    [Test]
    public void New_Element_ShouldNotBeNull()
    {
        var block = new Block(new XElement(L5XName.Block));

        block.Should().NotBeNull();
    }

    [Test]
    public void Operand_SetToTagNameValue_ShouldBeExpected()
    {
        var block = Block.IREF();

        block.Operand = "MyTag";

        block.Operand.Should().NotBeNull();
        block.Operand.Should().Be("MyTag");
    }

    [Test]
    public void Operand_SetToAtomicValue_ShouldBeExpected()
    {
        var block = Block.IREF();

        block.Operand = 100;

        block.Operand.Should().NotBeNull();
        block.Operand.Should().Be(100);
    }
}