using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class RadixTimeNsTests
{
    [Test]
    public void TimeNs_WhenCalled_ShouldBeExpected()
    {
        var radix = Radix.TimeNs;

        radix.Should().NotBeNull();
        radix.Name.Should().Be("TimeNs");
        radix.Value.Should().Be("LTime (ns)");
    }

    [Test]
    [TestCase(0, "LT#0ns")]
    [TestCase(1, "LT#1ns")]
    [TestCase(-1, "LT#-1ns")]
    [TestCase(2147123456789, "LT#35m_47s_123ms_456us_789ns")]
    [TestCase(891347000621879, "LT#10d_7h_35m_47s_621us_879ns")]
    public void Format_ValidExamples_ShouldBeExpected(long value, string expected)
    {
        var radix = Radix.TimeNs;

        var result = radix.Format(value);

        result.Should().Be(expected);
    }

    [Test]
    public void Parse_NullValue_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => Radix.TimeNs.Parse<long>(null!))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Parse_EmptyValue_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => Radix.TimeNs.Parse<long>(string.Empty))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Parse_NoSpecifier_ShouldThrowFormatException()
    {
        FluentActions.Invoking(() => Radix.TimeNs.Parse<long>("123us"))
            .Should().Throw<FormatException>();
    }

    [Test]
    public void Parse_InvalidType_ShouldThrowNotSupportedException()
    {
        FluentActions.Invoking(() => Radix.TimeNs.Parse<int>("LT#1ns"))
            .Should().Throw<NotSupportedException>();
    }

    [Test]
    [TestCase("LT#0ns", 0)]
    [TestCase("LT#1ns", 1)]
    [TestCase("LT#-1ns", -1)]
    [TestCase("LT#35m_47s_123ms_456us_789ns", 2147123456789)]
    [TestCase("LT#10d_7h_35m_47s_621us_879ns", 891347000621879)]
    public void Parse_ValidExample_ShouldBeExpectedValue(string value, long expected)
    {
        var radix = Radix.TimeNs;

        var result = radix.Parse<long>(value);

        result.Should().Be(expected);
    }
}