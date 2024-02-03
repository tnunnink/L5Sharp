using FluentAssertions;

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

    [Test]
    public void IsEquivalent_AreEqual_ShouldBeTrue()
    {
        var first = new Rung("XIC(SomeTag)OTE(AnotherTag);");
        var second = new Rung("XIC(SomeTag)OTE(AnotherTag);");

        var result = first.IsEquivalent(second);

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsEquivalent_AreNotEqual_ShouldBeFalse()
    {
        var first = new Rung("XIC(SomeTag)OTL(AnotherTag);");
        var second = new Rung("XIC(SomeTag)OTE(AnotherTag);");

        var result = first.IsEquivalent(second);

        result.Should().BeFalse();
    }
}