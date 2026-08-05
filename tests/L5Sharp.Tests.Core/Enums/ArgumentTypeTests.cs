using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class ArgumentTypeTests
{
    [Test]
    public void Empty_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.Empty.Should().NotBeNull();
    }

    [Test]
    public void Unknown_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.Unknown.Should().NotBeNull();
    }

    [Test]
    public void Atomic_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.Atomic.Should().NotBeNull();
    }

    [Test]
    public void String_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.String.Should().NotBeNull();
    }

    [Test]
    public void Tag_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.Reference.Should().NotBeNull();
    }

    [Test]
    public void Expression_WhenCalled_ShouldNotBeNull()
    {
        ArgumentType.Expression.Should().NotBeNull();
    }

    [Test]
    public void Of_NullString_ShouldBeEmpty()
    {
        var type = ArgumentType.Of(null!);

        type.Should().Be(ArgumentType.Empty);
    }

    [Test]
    public void Of_EmptyString_ShouldBeEmpty()
    {
        var type = ArgumentType.Of(string.Empty);

        type.Should().Be(ArgumentType.Empty);
    }

    [Test]
    public void Of_ValidInteger_ShouldBeAtomic()
    {
        var type = ArgumentType.Of("123");

        type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void Of_ValidFloat_ShouldBeAtomic()
    {
        var type = ArgumentType.Of("1.23");

        type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void Of_ValidBinary_ShouldBeAtomic()
    {
        var type = ArgumentType.Of("2#0000_1011_0010_0110");

        type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void Of_StringLiteral_ShouldBeString()
    {
        var type = ArgumentType.Of("'This is a literal string test value'");

        type.Should().Be(ArgumentType.String);
    }

    [Test]
    public void Of_StringWithoutSingleQuotes_ShouldBeUnknown()
    {
        var type = ArgumentType.Of("This is a literal string test value");

        type.Should().Be(ArgumentType.Unknown);
    }

    [Test]
    public void Of_ValidTagName_ShouldBeTag()
    {
        var type = ArgumentType.Of("MyTag.Member[0].1");

        type.Should().Be(ArgumentType.Reference);
    }

    [TestCase(" MyTag")]
    [TestCase(" Motor1.Status")]
    public void Of_TagWithLeadingSpace_ShouldBeReference(string value)
    {
        var type = ArgumentType.Of(value);

        type.Should().Be(ArgumentType.Reference);
    }
}