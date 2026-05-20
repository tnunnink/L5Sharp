using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class ArgumentTests
{
    [Test]
    public void New_NullValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Argument(null!)).Should().Throw<ArgumentException>();
    }

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
    public void New_SimpleNameArgument_ShouldBeExpected()
    {
        Argument argument = "MyComponent";

        argument.Should().Be("MyComponent");
        argument.Type.Should().Be(ArgumentType.Reference);
        argument.IsReference.Should().BeTrue();
    }

    [Test]
    public void New_ComplexTagArgument_ShouldBeExpected()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.Should().Be("MyTagName.Member[1].Active.1");
        argument.Type.Should().Be(ArgumentType.Reference);
        argument.IsReference.Should().BeTrue();
    }

    [Test]
    public void New_StringArgument_ShouldBeExpected()
    {
        Argument argument = "'This is a test string'";

        argument.Should().Be("'This is a test string'");
        argument.Type.Should().Be(ArgumentType.String);
        argument.IsLiteral.Should().BeTrue();
        argument.IsString.Should().BeTrue();
    }

    [Test]
    public void New_ExpressionArgument_ShouldBeExpected()
    {
        Argument argument = "SomeTag.Value > 100";

        argument.Should().Be("SomeTag.Value > 100");
        argument.Type.Should().Be(ArgumentType.Expression);
        argument.IsExpression.Should().BeTrue();
    }

    [TestCase("", "Empty")]
    [TestCase("?", "Unknown")]
    [TestCase(" ", "Unknown")]
    [TestCase("!!", "Unknown")]
    // Atomic (Numeric and Radix formats)
    [TestCase("12345", "Atomic")]
    [TestCase("2#0010_0110", "Atomic")]
    [TestCase("8#77", "Atomic")]
    [TestCase("16#ABCD", "Atomic")]
    [TestCase("1.23", "Atomic")]
    [TestCase("1.23e10", "Atomic")]
    [TestCase("T#2h_30m", "Atomic")]
    [TestCase("DT#2023-01-01-12:00:00.000000Z", "Atomic")]
    // String Literals
    [TestCase("'Test String'", "String")]
    [TestCase("''", "String")]
    [TestCase("'String with $P symbols'", "String")]
    // Reference (Tags and System Components)
    [TestCase("MyTagName.Member[1].Active.1", "Reference")]
    [TestCase("Program:MainProgram.LocalTag", "Reference")]
    [TestCase("MyArray[1,2,3]", "Reference")]
    [TestCase("MyTag[NestedTag].MemberName", "Reference")] // Indirect addressing
    [TestCase("Module:1:I.Data", "Reference")] // System/Module reference
    [TestCase("FAULTLOG", "Reference")] // System component
    // Expression
    [TestCase("ABS(MyTagName) >= 1000", "Expression")]
    [TestCase("(Value1 + Value2) * 10", "Expression")]
    [TestCase("Value1 / 2", "Expression")]
    [TestCase("Value1 < Value2", "Expression")]
    [TestCase("Value1 = 1", "Expression")]
    public void Type_WhenCalled_ShouldHaveExpectedValue(string value, string expected)
    {
        var argument = new Argument(value);

        var type = argument.Type;

        type.Should().Be(ArgumentType.Parse(expected));
    }

    [Test]
    public void Arguments_ArgumentWithSingleTag_ShouldHaveExpectedCount()
    {
        var argument = new Argument("MyTagName.Member[1].Active.1");

        var args = argument.Arguments.ToArray();

        args.Should().HaveCount(1);
    }

    [Test]
    public void Arguments_ArgumentSingleAtomic_ShouldHaveExpectedCount()
    {
        Argument argument = 100;

        var args = argument.Arguments;

        args.Should().HaveCount(1);
    }

    [Test]
    public void Arguments_ExpressionWithSingleTagAndAtomic_ShouldHaveExpectedValue()
    {
        Argument argument = "MyTag > 100";

        var args = argument.Arguments;

        args.Should().HaveCount(2);
        args[0].Should().Be("MyTag");
        args[1].Should().Be("100");
    }

    [Test]
    public void Arguments_ExpressionWithMultipleTagsAndAtomics_ShouldHaveExpectedValues()
    {
        Argument argument = "MyTag > 100 AND MyOtherTag < 16#ABCD";

        var args = argument.Arguments;

        args.Should().HaveCount(4);
        args[0].Should().Be("MyTag");
        args[1].Should().Be("100");
        args[2].Should().Be("MyOtherTag");
        args[3].Should().Be("16#ABCD");
    }

    [Test]
    public void Arguments_ExpressionWithVariousAtomicFormats_ShouldExtractAll()
    {
        Argument argument = "16#1234 + 2#1010 + 8#77 + 1.23 + 123 + 1.#QNAN";

        var args = argument.Arguments;

        args.Should().HaveCount(6);
        args.Select(v => v.ToString()).Should().BeEquivalentTo("16#1234", "2#1010", "8#77", "1.23", "123", "1.#QNAN");
    }

    [Test]
    public void Argument_ExpressionWithNestedFunctions_ShouldReturnAllNestedArguments()
    {
        Argument argument = "(ABS(MyTag.Member) + Another[1,2,3]) / (10**(SomeConstant - SystemTag[IndexReference]))";

        var args = argument.Arguments;

        args.Should().HaveCount(6);
    }
}