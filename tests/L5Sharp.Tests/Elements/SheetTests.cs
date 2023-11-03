using FluentAssertions;
using L5Sharp.Elements;
using L5Sharp.Enums;

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
    public void Add_IRefBlock_ShouldHaveSingleBlock()
    {
        var sheet = new Sheet();

        sheet.Add(new ReferenceBlock { Operand = "MyTagName", X = 100, Y = 300 });

        sheet.Blocks().Should().HaveCount(1);
    }
    
    [Test]
    public void Add_MultipleBlocks_ShouldGetExpectedIds()
    {
        var sheet = new Sheet();

        var zero = sheet.Add(new ReferenceBlock { Operand = "MyTagName", X = 100, Y = 300 });
        var one = sheet.Add(new ReferenceBlock { Operand = "MyTagName", X = 100, Y = 300 });
        var two = sheet.Add(new ReferenceBlock { Operand = "MyTagName", X = 100, Y = 300 });

        zero.Should().Be(0);
        one.Should().Be(1);
        two.Should().Be(2);
    }

    [Test]
    public Task Add_BlocksOutOfOrder_ShouldBeVerified()
    {
        var sheet = new Sheet();

        sheet.Add(new ReferenceBlock { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });
        sheet.Add(new ReferenceBlock(ParameterType.Output) { Operand = "OutputReference", X = 100, Y = 300 });

        return Verify(sheet.Serialize().ToString());
    }

    [Test]
    public void Add_BlockAndText_ShouldHaveExpectedCount()
    {
        var sheet = new Sheet();

        sheet.Add(new ReferenceBlock { Operand = "InputReference", X = 100, Y = 300 });
        sheet.Add(new Block { Operand = "MyBlockTag", X = 100, Y = 300 });

        sheet.Blocks().Should().HaveCount(2);
    }
}