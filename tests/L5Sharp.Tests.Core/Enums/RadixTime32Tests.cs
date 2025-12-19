using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class RadixTime32Tests
{
    [Test]
    public void Time32_WhenCalled_ShouldBeExpected()
    {
        var radix = Radix.Time32;

        radix.Should().NotBeNull();
        radix.Name.Should().Be("Time32");
        radix.Value.Should().Be("Time32 (us)");
    }

    [Test]
    [TestCase(0, "T32#0us")]
    [TestCase(1, "T32#1us")]
    [TestCase(-1, "T32#-1us")]
    [TestCase(2147483647, "T32#35m_47s_483ms_647us")]
    [TestCase(-2147483647, "T32#-35m_47s_483ms_647us")]
    public void Format_ValidExamples_ShouldBeExpected(int value, string expected)
    {
        var radix = Radix.Time32;

        var result = radix.Format(value);

        result.Should().Be(expected);
    }

    [Test]
    public void Format_InvalidValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => Radix.Time32.Format(97326001234))
            .Should().Throw<NotSupportedException>();
    }

    [Test]
    public void Parse_NoSpecifier_ShouldThrowFormatException()
    {
        FluentActions.Invoking(() => Radix.Time32.Parse<int>("123us"))
            .Should().Throw<FormatException>();
    }

    [Test]
    [TestCase("T32#0us", 0)]
    [TestCase("T32#1us", 1)]
    [TestCase("T32#-1us", -1)]
    [TestCase("T32#35m_47s_483ms_647us", 2147483647)]
    [TestCase("T32#-35m_47s_483ms_647us", -2147483647)]
    public void Parse_ValidTime32Example1_ShouldBeExpectedValue(string value, int expected)
    {
        var radix = Radix.Time32;

        var result = radix.Parse<int>(value);

        result.Should().Be(expected);
    }
}