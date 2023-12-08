using FluentAssertions;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class SheetTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var sheet = new Sheet();

        sheet.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldNotBeAttached()
    {
        var sheet = new Sheet();

        sheet.IsAttached.Should().BeFalse();
        sheet.L5X.Should().BeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var sheet = new Sheet();

        sheet.Number.Should().Be(0);
        sheet.Description.Should().BeNull();
        sheet.Container.Should().BeEmpty();
        sheet.Blocks().Should().BeEmpty();
    }

    [Test]
    public void Add_IRef_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.Add(Block.IREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_ORef_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.Add(Block.OREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_Block_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.Add(Block.SCL());

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_MultipleBlocks_ShouldGetExpectedIds()
    {
        var sheet = new Sheet();

        var zero = sheet.Add(Block.IREF("MyTagName"));
        var one = sheet.Add(Block.IREF("MyTagName"));
        var two = sheet.Add(Block.IREF("MyTagName"));

        zero.Should().Be(0);
        one.Should().Be(1);
        two.Should().Be(2);
    }

    [Test]
    public Task Add_BlocksOutOfOrder_ShouldBeVerified()
    {
        var sheet = new Sheet();

        sheet.Add(Block.IREF("InputReference"));
        sheet.Add(Block.SCL());
        sheet.Add(Block.IREF("OutputReference"));

        return Verify(sheet.Serialize().ToString());
    }

    [Test]
    public void Add_BlockAndText_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.Add(Block.IREF("InputReference"));
        sheet.AddAt("MyBlockTag", 100, 300);

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Block_ValidId_ShouldNotBeNull()
    {
        var sheet = new Sheet();
        sheet.Add(Block.IREF("InputReference"));
        sheet.Add(Block.ADD());
        sheet.Add(Block.OREF("OutputReference"));

        var block = sheet.Block(2);

        block.Should().NotBeNull();
        block?.Type.Should().Be("ORef");
        block?.Operand.Should().Be("OutputReference");
    }

    [Test]
    public void Block_InvalidId_ShouldBeNull()
    {
        var sheet = new Sheet();
        sheet.Add(Block.IREF("InputReference"));
        sheet.Add(Block.ADD());
        sheet.Add(Block.OREF("OutputReference"));

        var block = sheet.Block(4);

        block.Should().BeNull();
    }

    [Test]
    public void Blocks_Default_ShouldBeEmpty()
    {
        var sheet = new Sheet();

        var blocks = sheet.Blocks().ToList();

        blocks.Should().BeEmpty();
    }

    [Test]
    public void Blocks_HasCollection_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        sheet.Add(Block.IREF("InputReference"));
        sheet.Add(Block.ADD());
        sheet.Add(Block.OREF("OutputReference"));

        var blocks = sheet.Blocks();

        blocks.Should().HaveCount(3);
    }

    [Test]
    public void AddAt_ValidBlock_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        var id = sheet.AddAt(100, 200, Block.ADD("TestTag"));

        sheet.Blocks().Should().HaveCount(1);
        id.Should().Be(0);
        sheet.Block(id)?.Type.Should().Be("ADD");
        sheet.Block(id)?.X.Should().Be(100);
        sheet.Block(id)?.Y.Should().Be(200);
        sheet.Block(id)?.Operand.Should().Be("TestTag");
    }

    [Test]
    public void Wire_ValidOperands_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        sheet.Add(Block.IREF("MyTag"));
        sheet.Add(Block.SCL());
        
        sheet.Wire("MyTag", "SCL_01.In");

        sheet.Wires().Should().HaveCount(1);
    }
    
    [Test]
    public Task Wire_ValidOperands_ShouldBeVerified()
    {
        var sheet = new Sheet();
        sheet.Add(Block.IREF("MyTag"));
        sheet.Add(Block.SCL());
        
        sheet.Wire("MyTag", "SCL_01.In");

        return Verify(sheet.Serialize().ToString());
    }

    [Test]
    public void Connectors_Default_ShouldBeEmpty()
    {
        var sheet = new Sheet();

        var wires = sheet.Wires().ToList();

        wires.Should().BeEmpty();
    }
    
    [Test]
    public void Connectors_HasWired_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        sheet.Add(new Wire { FromID = 0, ToID = 1 });
        sheet.Add(new Wire { FromID = 0, ToID = 2 });
        sheet.Add(new Wire { FromID = 1, ToID = 3 });

        var wires = sheet.Wires().ToList();

        wires.Should().HaveCount(3);
    }
}