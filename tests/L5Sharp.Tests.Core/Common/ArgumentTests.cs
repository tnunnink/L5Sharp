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
        argument.IsValid.Should().BeFalse();
    }

    [Test]
    public void Unknown_WhenCalled_ShouldHaveExpectedValue()
    {
        var argument = Argument.Unknown;

        argument.Should().Be("?");
        argument.Type.Should().Be(ArgumentType.Unknown);
        argument.IsValid.Should().BeFalse();
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
    public void ToAtomic_ValidAtomicValue_ShouldBeExpected()
    {
        Argument argument = "123";

        var atomic = argument.ToAtomic();

        atomic.Should().NotBeNull();
        atomic.ToString().Should().Be("123");
    }

    [Test]
    public void ToAtomic_RealValue_ShouldBeExpected()
    {
        Argument argument = "1.23";

        var atomic = argument.ToAtomic();

        atomic.Should().NotBeNull();
        atomic.ToString().Should().Be("1.23");
    }

    [Test]
    public void ToAtomic_NonAtomicValue_ShouldThrowException()
    {
        Argument argument = "TagName";

        FluentActions.Invoking(argument.ToAtomic).Should().Throw<Exception>();
    }

    [Test]
    public void ToNeutralText_WhenCalled_ShouldBeExpected()
    {
        Argument argument = "SomeTag > 100";

        var text = argument.ToNeutralText();

        text.Should().NotBeNull();
        text.ToString().Should().Be("SomeTag > 100");
    }

    [TestCase(" Motor1.Status")]
    [TestCase(" MyTag")]
    public void New_TagWithLeadingWhitespace_ShouldBeReference(string value)
    {
        Argument argument = value;

        argument.Type.Should().Be(ArgumentType.Reference);
        argument.IsReference.Should().BeTrue();
    }
}