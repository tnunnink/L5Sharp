using FluentAssertions;
using L5Sharp.Elements;

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

        var id = sheet.Add(new IREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_ORef_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.Add(new IREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_Block_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.Add(new Block { Type = "SCL", Operand = "MyTag" });

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_MultipleBlocks_ShouldGetExpectedIds()
    {
        var sheet = new Sheet();

        var zero = sheet.Add(new IREF { Operand = "MyTagName", X = 100, Y = 300 });
        var one = sheet.Add(new IREF { Operand = "MyTagName", X = 100, Y = 300 });
        var two = sheet.Add(new IREF { Operand = "MyTagName", X = 100, Y = 300 });

        zero.Should().Be(0);
        one.Should().Be(1);
        two.Should().Be(2);
    }

    [Test]
    public Task Add_BlocksOutOfOrder_ShouldBeVerified()
    {
        var sheet = new Sheet();

        sheet.Add(new IREF { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new IREF { Operand = "OutputReference", X = 100, Y = 300 });

        return Verify(sheet.Serialize().ToString());
    }

    [Test]
    public void Add_BlockAndText_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.Add(new IREF { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new TextBox { Text = "MyBlockTag", X = 100, Y = 300 });

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Block_ValidId_ShouldNotBeNull()
    {
        var sheet = new Sheet();
        sheet.Add(new IREF { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new OREF { Operand = "OutputReference", X = 100, Y = 300 });

        var block = sheet.Block(2);

        block.Should().NotBeNull();
        block.Should().BeOfType<OREF>();
    }

    [Test]
    public void Block_InvalidId_ShouldBeNull()
    {
        var sheet = new Sheet();
        sheet.Add(new IREF { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new OREF { Operand = "OutputReference", X = 100, Y = 300 });

        var block = sheet.Block(4);

        block.Should().BeNull();
    }

    [Test]
    public void BlockOfType_Exists_ShouldNotBeNull()
    {
        var sheet = new Sheet();
        sheet.Add(new IREF { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new OREF { Operand = "OutputReference", X = 100, Y = 300 });

        var block = sheet.Block<OREF>(2);

        block.Should().NotBeNull();
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
        sheet.Add(new IREF() { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new OREF() { Operand = "OutputReference", X = 100, Y = 300 });

        var blocks = sheet.Blocks();

        blocks.Should().HaveCount(3);
    }

    [Test]
    public void Connect_ValidIds_ShouldHaveExpectedConnectorsCount()
    {
        var sheet = new Sheet();
        
        sheet.Connect(0, 1);
        sheet.Connect(1, 2);
        sheet.Connect(0, 2);

        sheet.Connectors().Should().HaveCount(3);
    }

    [Test]
    public void Connectors_Default_ShouldBeEmpty()
    {
        var sheet = new Sheet();

        var wires = sheet.Connectors().ToList();

        wires.Should().BeEmpty();
    }
    
    [Test]
    public void Connectors_HasWired_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();
        sheet.Add(new Wire { FromID = 0, ToID = 1 });
        sheet.Add(new Wire { FromID = 0, ToID = 2 });
        sheet.Add(new Wire { FromID = 1, ToID = 3 });

        var wires = sheet.Connectors().ToList();

        wires.Should().HaveCount(3);
    }

    [Test]
    public void Connections_BlockWithConnections_ShouldHaveExpectedCount()
    {
        var block = new Block { Operand = "ScaleTag", Type = "SCL" };
        var sheet = new Sheet();
        sheet.Add(new IREF("MyTag"));
        sheet.Add(block);
        sheet.Add(new OREF("ResultTag"));
        sheet.Add(new Wire { FromID = 0, ToID = 1 });
        sheet.Add(new Wire { FromID = 1, ToID = 2 });

        var connections = sheet.Connections(block).ToList();

        connections.Should().HaveCount(2);
    }
}