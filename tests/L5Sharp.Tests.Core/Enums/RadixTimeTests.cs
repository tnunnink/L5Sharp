using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class RadixTimeTests
{
    [Test]
    public void Time_WhenCalled_ShouldBeExpected()
    {
        var radix = Radix.Time;

        radix.Should().NotBeNull();
        radix.Name.Should().Be("Time");
        radix.Value.Should().Be("Time (us)");
    }

    [Test]
    [TestCase(0, "T#0us")]
    [TestCase(1, "T#1us")]
    [TestCase(-1, "T#-1us")]
    [TestCase(97326001234, "T#1d_3h_2m_6s_1ms_234us")]
    [TestCase(-7384000001, "T#-2h_3m_4s_1us")]
    public void Format_ValidExamples_ShouldBeExpected(long value, string expected)
    {
        var radix = Radix.Time;

        var result = radix.Format(value);

        result.Should().Be(expected);
    }

    [Test]
    public void Parse_NoSpecifier_ShouldThrowFormatException()
    {
        FluentActions.Invoking(() => Radix.Time.Parse<long>("123us"))
            .Should().Throw<FormatException>();
    }

    [Test]
    [TestCase("T#0us", 0)]
    [TestCase("T#1us", 1)]
    [TestCase("T#-1us", -1)]
    [TestCase("T#1d_3h_2m_6s_1ms_234us", 97326001234)]
    [TestCase("T#-2h_3m_4s_1us", -7384000001)]
    public void Parse_ValidTimeExample1_ShouldBeExpectedValue(string value, long expected)
    {
        var radix = Radix.Time;

        var result = radix.Parse<long>(value);

        result.Should().Be(expected);
    }
}