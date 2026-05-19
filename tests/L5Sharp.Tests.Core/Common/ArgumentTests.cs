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
        argument.IsInvalid.Should().BeTrue();
    }

    [Test]
    public void Unknown_WhenCalled_ShouldHaveExpectedValue()
    {
        var argument = Argument.Unknown;

        argument.Should().Be("?");
        argument.Type.Should().Be(ArgumentType.Unknown);
        argument.IsInvalid.Should().BeTrue();
    }

    [Test]
    public void New_AtomicArgument_ShouldBeExpected()
    {
        Argument argument = 100;

        argument.Should().Be("100");
        argument.Type.Should().Be(ArgumentType.Atomic);
        argument.IsLiteral.Should().BeTrue();
        argument.IsAtomic.Should().BeTrue();
    }

    [Test]
    public void New_TagArgument_ShouldBeExpected()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.Should().Be("MyTagName.Member[1].Active.1");
        argument.Type.Should().Be(ArgumentType.Tag);
        argument.IsTag.Should().BeTrue();
    }

    [Test]
    public void New_StringArgument_ShouldBeExpected()
    {
        Argument argument = "'This is a test string'";

        argument.Should().Be("'This is a test string'");
        argument.Type.Should().Be(ArgumentType.String);
        argument.IsLiteral.Should().BeTrue();
    }

    [Test]
    public void New_ExpressionArgument_ShouldBeExpected()
    {
        Argument argument = "SomeTag.Value > 100";

        argument.Should().Be("SomeTag.Value > 100");
        argument.Type.Should().Be(ArgumentType.Expression);
        argument.IsExpression.Should().BeTrue();
    }

    [Test]
    public void New_NullValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Argument(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_EmptyValue_ShouldBeEmpty()
    {
        var argument = new Argument(string.Empty);

        argument.Should().Be(string.Empty);
        argument.Type.Should().Be(ArgumentType.Empty);
    }

    [Test]
    public void New_UnknownValue_ShouldBeUnknown()
    {
        var argument = new Argument("?");

        argument.Should().Be("?");
        argument.Type.Should().Be(ArgumentType.Unknown);
    }

    [Test]
    public void New_AtomicDecimalValue_ShouldHaveExpectedValueAndType()
    {
        var argument = new Argument("12345");

        argument.Should().Be("12345");
        argument.Type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void New_AtomicBinaryValue_ShouldHaveExpectedValueAndType()
    {
        var argument = new Argument("2#0010_0110");

        argument.Should().Be("2#0010_0110");
        argument.Type.Should().Be(ArgumentType.Atomic);
    }

    [Test]
    public void New_StringValue_ShouldHaveExpectedValueAndType()
    {
        var argument = new Argument("'Test String'");

        argument.Should().Be("'Test String'");
        argument.Type.Should().Be(ArgumentType.String);
    }


    [Test]
    public void New_TagNameValue_ShouldHaveExpectedValueAndType()
    {
        var argument = new Argument("MyTagName.Member[1].Active.1");

        argument.Should().Be("MyTagName.Member[1].Active.1");
        argument.Type.Should().Be(ArgumentType.Tag);
    }

    [Test]
    public void New_ExpressionValue_ShouldHaveExpectedValueAndType()
    {
        var argument = new Argument("ABS(MyTagName) >= 1000");

        argument.Should().Be("ABS(MyTagName) >= 1000");
        argument.Type.Should().Be(ArgumentType.Expression);
    }

    [Test]
    public void Arguments_ArgumentWithSingleTag_ShouldHaveExpectedCount()
    {
        var argument = new Argument("MyTagName.Member[1].Active.1");

        var args = argument.Arguments.ToArray();

        args.Should().HaveCount(1);
    }

    [Test]
    public void Arguments_ExpressionArgumentMultipleArguments_ShouldHaveExpectedCount()
    {
        Argument argument = "CMP(MyTagName.Member[1].Active >= MyConstant)";

        var arguments = argument.Arguments;

        arguments.Should().HaveCount(2);
    }

    [Test]
    public void Arguments_ArgumentSingleAtomic_ShouldHaveExpectedCount()
    {
        Argument argument = 100;

        var values = argument.Arguments;

        values.Should().HaveCount(1);
    }

    [Test]
    public void Arguments_ExpressionWithSingleAtomic_ShouldHaveExpectedValue()
    {
        Argument argument = "MyTag > 100";

        var values = argument.Arguments;

        values.Should().HaveCount(2);
        values[0].Should().Be("100");
    }

    [Test]
    public void Arguments_ExpressionWithMultipleAtomics_ShouldHaveExpectedValues()
    {
        Argument argument = "MyTag > 100 AND MyOtherTag < 16#ABCD";

        var values = argument.Arguments;

        values.Should().HaveCount(2);
        values[0].Should().Be(new DINT(100));
        values[1].Should().Be(new DINT(43981)); // 16#ABCD
    }

    [Test]
    public void Arguments_ExpressionWithVariousAtomicFormats_ShouldExtractAll()
    {
        Argument argument = "16#1234 + 2#1010 + 8#77 + DT#2023-05-18-11:08:00Z + 1.23 + 123 + 1.#QNAN";

        var arguments = argument.Arguments;

        arguments.Should().HaveCount(7);

        arguments.Select(v => v.ToString()).Should().Contain([
            "16#0000_1234",
            "2#0000_0000_0000_0000_0000_0000_0000_1010",
            "8#0000_0000_077",
            "DT#2023-05-18-11:08:00Z",
            "1.23",
            "123",
            "1.#QNAN"
        ]);
    }
}