using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixDateTimeNsTests
    {
        [Test]
        public void DateTimeNs_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTimeNs;

            radix.Should().NotBeNull();
            radix.Name.Should().Be("DateTimeNs");
            radix.Value.Should().Be("Date/Time (ns)");
        }

        [Test]
        public void Format_ValidTimeExample1_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Format(1638277952000000);

            result.Should().Be("LDT#1970-01-19-23:04:37.952000000Z");
        }

        [Test]
        public void Format_ValidTimeExample2_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Format(1641016800000000000);

            result.Should().Be("LDT#2022-01-01-06:00:00.000000000Z");
        }

        [Test]
        public void Format_ValidTimeExample3_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Format(1641016800000001001);

            //loss of accuracy causes the 1 nanosecond to be lost
            result.Should().Be("LDT#2022-01-01-06:00:00.000001000Z");
        }

        [Test]
        public void Format_ValidTimeExample4_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Format(1641016800000001500);

            result.Should().Be("LDT#2022-01-01-06:00:00.000001500Z");
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Radix.DateTimeNs.Parse<long>("2021-11-30-07:12:32.000000000Z"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_ValidTimeExample1_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Parse<long>("LDT#1970-01-19-23:04:37.952000000Z");

            result.Should().Be(1638277952000000);
        }

        [Test]
        public void Parse_ValidTimeExample2_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Parse<long>("LDT#2022-01-01-06:00:00.000000000Z");

            result.Should().Be(1641016800000000000);
        }
    }
}