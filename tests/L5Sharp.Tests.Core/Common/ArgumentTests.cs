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
    }

    [Test]
    public void Unknown_WhenCalled_ShouldHaveExpectedValue()
    {
        var argument = Argument.Unknown;

        argument.Should().Be("?");
    }

    [Test]
    public void IsAtomic_AtomicArgument_ShouldBeTrue()
    {
        Argument argument = 100;

        argument.IsAtomic.Should().BeTrue();
    }

    [Test]
    public void IsAtomic_NonAtomicArgument_ShouldBeFalse()
    {
        Argument argument = "Test";

        argument.IsAtomic.Should().BeFalse();
    }

    [Test]
    public void IsExpression_ExpressionArgument_ShouldBeTrue()
    {
        Argument argument = "XIC(MyTagName.Member[1].Active)";

        argument.IsExpression.Should().BeTrue();
    }

    [Test]
    public void IsExpression_NonExpressionArgument_ShouldBeFalse()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.IsExpression.Should().BeFalse();
    }

    [Test]
    public void IsImmediate_AtomicArgument_ShouldBeTrue()
    {
        Argument argument = 100;

        argument.IsImmediate.Should().BeTrue();
    }

    [Test]
    public void IsImmediate_StringArgument_ShouldBeTrue()
    {
        Argument argument = "'Constant'";

        argument.IsImmediate.Should().BeTrue();
    }

    [Test]
    public void IsImmediate_TagNameArgument_ShouldBeFalse()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.IsImmediate.Should().BeFalse();
    }

    [Test]
    public void IsTag_TagArgument_ShouldBeTrue()
    {
        Argument argument = "MyTagName.Member[1].Active.1";

        argument.IsTag.Should().BeTrue();
    }

    [Test]
    public void IsTag_AtomicArgument_ShouldBeFalse()
    {
        Argument argument = true;

        argument.IsTag.Should().BeFalse();
    }

    [Test]
    public void IsString_StringArgument_ShouldBeTrue()
    {
        Argument argument = "'Constant'";

        argument.IsString.Should().BeTrue();
    }

    [Test]
    public void IsString_AtomicArgument_ShouldBeFalse()
    {
        Argument argument = 1.23;

        argument.IsString.Should().BeFalse();
    }

    [Test]
    public void Parse_Null_ShouldBeEmpty()
    {
        FluentActions.Invoking(() => Argument.Parse(null)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Parse_EmptyString_ShouldThrowException()
    {
        FluentActions.Invoking(() => Argument.Parse(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Parse_QuestionMark_ShouldBeUnknown()
    {
        var argument = Argument.Parse("?");

        argument.Should().Be("?");
    }

    [Test]
    public void Parse_StringLiteral_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("'Test String'");

        argument.Should().NotBeNull();
        argument.IsString.Should().BeTrue();
        argument.Should().Be("'Test String'");
    }

    [Test]
    public void Parse_AtomicDecimalRadix_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("100");

        argument.Should().NotBeNull();
        argument.IsAtomic.Should().BeTrue();
        argument.Should().Be(100);
    }

    [Test]
    public void Parse_AtomicBinaryRadix_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("2#0010_0110");

        argument.Should().NotBeNull();
        argument.IsAtomic.Should().BeTrue();
        argument.Should().Be(38);
    }

    [Test]
    public void TagName_ValidTagName_ShouldHaveExpectedValueAndType()
    {
        var argument = Argument.Parse("MyTagName.Member[1].Active.1");

        argument.Should().NotBeNull();
        argument.IsTag.Should().BeTrue();
        argument.Should().Be("MyTagName.Member[1].Active.1");
    }

    [Test]
    public void Tags_ArgumentWithSingleTag_ShouldHaveExpectedCount()
    {
        var argument = Argument.Parse("MyTagName.Member[1].Active.1");

        var tags = argument.Tags;

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
    public void ImplicitConversion_TagName_ShouldNotBeExpected()
    {
        Argument argument = new TagName("Test");

        argument.Should().NotBeNull();
        argument.Should().Be("Test");
    }

    [Test]
    public void ImplicitOperator_Int_ShouldNotBeBeExpected()
    {
        Argument argument = 100;

        argument.Should().Be(100);
    }
}