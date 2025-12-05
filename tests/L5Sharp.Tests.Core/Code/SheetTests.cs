using FluentAssertions;

namespace L5Sharp.Tests.Core.Code;

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

    [Test]
    public void GetBlock_ValidId_ShouldNotBeNull()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.IREF("InputReference"))
            .AddBlock(Block.ADD())
            .AddBlock(Block.OREF("OutputReference"));

        var block = sheet.GetBlock(2);

        block.Should().NotBeNull();
        block.Type.Should().Be("ORef");
        block.Operand.Should().Be("OutputReference");
    }

    [Test]
    public void GetBlock_InvalidId_ShouldThrowException()
    {
        var sheet = new Sheet()
            .AddBlock(Block.IREF("InputReference"))
            .AddBlock(Block.ADD())
            .AddBlock(Block.OREF("OutputReference"));

        FluentActions.Invoking(() => sheet.GetBlock(4)).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void AddBlock_IRef_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.IREF("MyTagName"));

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void AddBlock_ORef_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.OREF("MyTagName"));

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void AddBlock_Block_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.SCL());

        sheet.Blocks().Should().HaveCount(1);
    }

    [Test]
    public void AddBlock_MultipleBlocks_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.IREF("MyTagName"))
            .AddBlock(Block.IREF("MyTagName"))
            .AddBlock(Block.IREF("MyTagName"));

        sheet.Blocks().Should().HaveCount(3);
    }

    [Test]
    public Task AddBlock_BlocksOutOfOrder_ShouldBeVerified()
    {
        var sheet = new Sheet();

        sheet.AddBlock(Block.IREF("InputReference"))
            .AddBlock(Block.SCL())
            .AddBlock(Block.IREF("OutputReference"));

        return Verify(sheet.Serialize().ToString());
    }
}