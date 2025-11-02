using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class ArgumentTests
{
    [Test]
    public void Empty_WhenCalled_ShouldHaveExpectedValue()
    {
        var argument = Argument.Empty;

        argument.Should().Be(string.Empty);
        argument.Type.Should().Be(ArgumentType.Empty);
    }

    [Test]
    public void Unknown_WhenCalled_ShouldHaveExpectedValue()
    {
        var argument = Argument.Unknown;

        argument.Should().Be("?");
        argument.Type.Should().Be(ArgumentType.Unknown);
    }

    [Test]
    public void New_AtomicArgument_ShouldBeExpected()
    {
        Argument argument = 100;

        argument.Should().Be("100");
        argument.Type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void New_TagArgument_ShouldBeExpected()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.Should().Be("MyTagName.Member[1].Active.1");
        argument.Type.Should().Be(ArgumentType.Tag);
    }

    [Test]
    public void New_StringArgument_ShouldBeExpected()
    {
        Argument argument = "'This is a test string'";

        argument.Should().Be("'This is a test string'");
        argument.Type.Should().Be(ArgumentType.String);
    }

    [Test]
    public void New_ExpressionArgument_ShouldBeExpected()
    {
        Argument argument = "SomeTag.Value > 100";

        argument.Should().Be("SomeTag.Value > 100");
        argument.Type.Should().Be(ArgumentType.Expression);
    }

    [Test]
    public void Parse_NullValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => Argument.Parse(null)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Parse_EmptyValue_ShouldBeEmpty()
    {
        var argument = Argument.Parse(string.Empty);

        argument.Should().Be(string.Empty);
        argument.Type.Should().Be(ArgumentType.Empty);
    }

    [Test]
    public void Parse_UnknownValue_ShouldBeUnknown()
    {
        var argument = Argument.Parse("?");

        argument.Should().Be("?");
        argument.Type.Should().Be(ArgumentType.Unknown);
    }

    [Test]
    public void Parse_AtomicDecimalValue_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("12345");

        argument.Should().Be("12345");
        argument.Type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void Parse_AtomicBinaryValue_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("2#0010_0110");

        argument.Should().Be("2#0010_0110");
        argument.Type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void Parse_StringValue_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("'Test String'");

        argument.Should().Be("'Test String'");
        argument.Type.Should().Be(ArgumentType.String);
    }


    [Test]
    public void Parse_TagNameValue_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("MyTagName.Member[1].Active.1");

        argument.Should().Be("MyTagName.Member[1].Active.1");
        argument.Type.Should().Be(ArgumentType.Tag);
    }

    [Test]
    public void Parse_ExpressionValue_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("ABS(MyTagName) >= 1000");

        argument.Should().Be("ABS(MyTagName) >= 1000");
        argument.Type.Should().Be(ArgumentType.Expression);
    }

    [Test]
    public void Tags_ArgumentWithSingleTag_ShouldHaveExpectedCount()
    {
        var argument = Argument.Parse("MyTagName.Member[1].Active.1");

        var tags = argument.Tags.ToArray();

        tags.Should().HaveCount(1);
    }

    [Test]
    public void Tags_ExpressionArgumentMultipleTags_ShouldHaveExpectedCount()
    {
        Argument argument = "CMP(MyTagName.Member[1].Active >= MyConstant)";

        var tags = argument.Tags;

        tags.Should().HaveCount(2);
    }

    [Test]
    public void Values_ArgumentSingleAtomic_ShouldHaveExpectedCount()
    {
        Argument argument = 100;

        var values = argument.Values;

        values.Should().HaveCount(1);
    }

    [Test]
    public void ToAtomic_OnAtomicArgument_ShouldBeExpectedTypeAndValue()
    {
        Argument argument = 100;

        var atomic = argument.ToAtomic();

        atomic.Should().BeOfType<SINT>();
        atomic.Should().Be(100);
    }

    [Test]
    public void ToAtomic_WithValidFormat_ShouldBeExpectedTypeAndValue()
    {
        Argument argument = "2#0010_0010";

        var atomic = argument.ToAtomic();

        atomic.Should().BeOfType<SINT>();
        atomic.Should().Be(34);
    }

    [Test]
    public void ToAtomic_NonAtomicArgument_ShouldThrowException()
    {
        Argument argument = "SomeTagName";

        FluentActions.Invoking(() => argument.ToAtomic()).Should().Throw<FormatException>();
    }

    [Test]
    public void ToTag_ValidTagArgument_ShouldBeExpected()
    {
        Argument argument = "MyTag.Item[0].Value";

        var tag = argument.ToTag();

        tag.Should().BeOfType<TagName>();
        tag.Should().Be("MyTag.Item[0].Value");
    }

    [Test]
    public void ToTag_InvalidTagArgument_ShouldBeExpected()
    {
        Argument argument = "1234";

        var tag = argument.ToTag();

        tag.Should().BeOfType<TagName>();
        tag.IsQualified.Should().BeFalse();
    }
}