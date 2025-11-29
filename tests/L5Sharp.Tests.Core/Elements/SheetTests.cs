using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

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
    public void New_Default_ShouldHaveExpectedValues()
    {
        var sheet = new Sheet();

        sheet.Number.Should().Be(0);
        sheet.Description.Should().BeNull();
        sheet.Program.Should().BeNull();
        sheet.Routine.Should().BeNull();

        sheet
            .AddBlock(Block.ABS("block01"))
            .AddBlock(Block.ALMD("block02"))
            .Connect("block01.Out", "block02.HiHi");
    }

    [Test]
    public void Add_IRef_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.AddBlock(Block.IREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_ORef_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        var id = sheet.AddBlock(Block.OREF("MyTagName"));

        id.Should().Be(0);
        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_Block_ShouldHaveExpectedCountAndId()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.SCL());

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void Add_MultipleBlocks_ShouldGetExpectedIds()
    {
        var sheet = new Sheet();

        var zero = sheet.AddBlock(Block.IREF("MyTagName"));
        var one = sheet.AddBlock(Block.IREF("MyTagName"));
        var two = sheet.AddBlock(Block.IREF("MyTagName"));

        zero.Should().Be(0);
        one.Should().Be(1);
        two.Should().Be(2);
    }

    [Test]
    public Task Add_BlocksOutOfOrder_ShouldBeVerified()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.IREF("InputReference"));
        sheet.AddBlock(Block.SCL());
        sheet.AddBlock(Block.IREF("OutputReference"));

        return Verify(sheet.Serialize().ToString());
    }

    [Test]
    public void GetBlock_ValidId_ShouldNotBeNull()
    {
        var sheet = new Sheet();
        sheet.AddBlock(Block.IREF("InputReference"));
        sheet.AddBlock(Block.ADD());
        sheet.AddBlock(Block.OREF("OutputReference"));

        var block = sheet.Block(2);

        block.Should().NotBeNull();
        block?.Type.Should().Be("ORef");
        block?.Operand.Should().Be("OutputReference");
    }

    [Test]
    public void GetBlock_InvalidId_ShouldBeNull()
    {
        var sheet = new Sheet();
        sheet.AddBlock(Block.IREF("InputReference"));
        sheet.AddBlock(Block.ADD());
        sheet.AddBlock(Block.OREF("OutputReference"));

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
        sheet.AddBlock(Block.IREF("InputReference"));
        sheet.AddBlock(Block.ADD());
        sheet.AddBlock(Block.OREF("OutputReference"));

        var blocks = sheet.Blocks();

        blocks.Should().HaveCount(3);
    }
}