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

    [Test]
    public Task New_Default_ShouldBeVerified()
    {
        var rung = new Rung();

        return VerifyXml(rung.Serialize().ToString());
    }
    
    [Test]
    public Task New_TextOverload_ShouldBeVerified()
    {
        var rung = new Rung("XIC(MyTag)[OTE(SomeOutput)TMR(TimerTag,?,?)];");

        return VerifyXml(rung.Serialize().ToString());
    }

    [Test]
    public Task SetComment_DefaultRung_ShouldBeValid()
    {
        var rung = new Rung();

        rung.Comment = "This is a test comment";

        return VerifyXml(rung.Serialize().ToString());
    }
    
    [Test]
    public Task SetCommentThenText_ShouldBeValid()
    {
        var rung = new Rung();

        rung.Comment = "This is a test comment";
        rung.Text = "AFI;";

        return VerifyXml(rung.Serialize().ToString());
    }

    [Test]
    public Task SetTextToNullThenCommentThenTextAgainShouldBeValid()
    {
        var rung = new Rung();

        rung.Text = null!;
        rung.Comment = "This is a test comment";
        rung.Text = "AFI;";

        return VerifyXml(rung.Serialize().ToString());
    }
}