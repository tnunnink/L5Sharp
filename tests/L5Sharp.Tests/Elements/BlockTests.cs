using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class BlockTests
{
    [Test]
    public void New_IRef_ShouldNotBeNull()
    {
        var block = Block.IREF("MyTag");

        block.Should().NotBeNull();
    }
    
    [Test]
    public void New_IRef_ShouldHaveExpectedValues()
    {
        var block = Block.IREF("MyTag");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("IRef");
        block.Operand.Should().Be("MyTag");
        block.HideDesc.Should().BeNull();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.IRef);
        block.Cell.Should().Be("A1");
        block.Location.Should().NotBeNull();
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
    }
    
    [Test]
    public void New_ORef_ShouldNotBeNull()
    {
        var block = Block.OREF("MyTag");

        block.Should().NotBeNull();
    }
    
    [Test]
    public void New_ORef_ShouldHaveExpectedValues()
    {
        var block = Block.OREF("MyTag");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("ORef");
        block.Operand.Should().Be("MyTag");
        block.HideDesc.Should().BeNull();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.ORef);
        block.Cell.Should().Be("A1");
        block.Location.Should().NotBeNull();
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
    }
    
    [Test]
    public void New_ICon_ShouldNotBeNull()
    {
        var block = Block.ICON("Connector");

        block.Should().NotBeNull();
    }
    
    [Test]
    public void New_ICon_ShouldHaveExpectedValues()
    {
        var block = Block.ICON("Connector");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("ICon");
        block.Operand.Should().Be("Connector");
        block.HideDesc.Should().BeNull();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.ICon);
        block.Cell.Should().Be("A1");
        block.Location.Should().NotBeNull();
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
    }
    
    [Test]
    public void New_OCon_ShouldNotBeNull()
    {
        var block = Block.OCON("Connector");

        block.Should().NotBeNull();
    }
    
    [Test]
    public void New_OCon_ShouldHaveExpectedValues()
    {
        var block = Block.OCON("Connector");

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("OCon");
        block.Operand.Should().Be("Connector");
        block.HideDesc.Should().BeNull();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.OCon);
        block.Cell.Should().Be("A1");
        block.Location.Should().NotBeNull();
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
    }

    [Test]
    public void New_Block_ShouldHaveDefaultValues()
    {
        var block = Block.SCL();

        block.ID.Should().Be(0);
        block.X.Should().Be(0);
        block.Y.Should().Be(0);
        block.Type.Should().Be("SCL");
        block.Operand.Should().Be("SCL_01");
        block.HideDesc.Should().BeNull();
        block.Pins.Should().HaveCount(2);
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.Block);
        block.Cell.Should().Be("A1");
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
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
        block.HideDesc.Should().BeNull();
        block.Pins.Should().BeEmpty();
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.Function);
        block.Cell.Should().Be("A1");
        block.Container.Should().BeEmpty();
        block.Scope.Should().Be(Scope.Null);
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
        block.HideDesc.Should().BeNull();
        block.Pins.Should().HaveCount(3);
        block.Sheet.Should().BeNull();
        block.IsAttached.Should().BeFalse();
        block.L5X.Should().BeNull();
        block.L5XType.Should().Be(L5XName.AddOnInstruction);
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
    public void Operand_SetToTagNameValue_ShouldBeExpected()
    {
        var block = Block.IREF();

        block.Operand = "MyTag";

        block.Operand.Should().NotBeNull();
        block.Operand.Should().Be("MyTag");
        block.Operand?.IsTag.Should().BeTrue();
    }
    
    [Test]
    public void Operand_SetToAtomicValue_ShouldBeExpected()
    {
        var block = Block.IREF();

        block.Operand = 100;

        block.Operand.Should().NotBeNull();
        block.Operand.Should().Be(100);
        block.Operand?.IsAtomic.Should().BeTrue();
    }
    
    [Test]
    public void References_WhenCalled_ShouldHaveExpectedCount()
    {
        var block = Block.ABS();

        var references = block.References().ToList();

        references.Should().HaveCount(2);
    }
}