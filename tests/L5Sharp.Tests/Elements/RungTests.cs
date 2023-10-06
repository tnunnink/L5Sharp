using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Elements;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class RungTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var rung = new Rung();

        rung.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var rung = new Rung();

        rung.Number.Should().Be(0);
        rung.Type.Should().Be(RungType.Normal);
        rung.Comment.Should().BeNull();
        rung.Text.Should().Be(NeutralText.Empty);
    }
}