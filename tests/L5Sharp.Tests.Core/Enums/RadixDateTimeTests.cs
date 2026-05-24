using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixDateTimeTests
    {
        [Test]
        public void DateTime_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTime;

            radix.Should().NotBeNull();
            radix.Name.Should().Be("DateTime");
            radix.Value.Should().Be("Date/Time");
        }

        [Test]
        public void Format_ValidTimeExample1_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Format(1638277952000000);

            result.Should().Be("DT#2021-11-30-13:12:32.000000Z");
        }

        [Test]
        public void Format_ValidTimeExample2_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Format(1641016800100100);

            result.Should().Be("DT#2022-01-01-06:00:00.100100Z");
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Radix.DateTime.Parse<long>("2021-11-30-07:12:32.000000Z"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_ValidTimeExample1_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.Parse<long>("DT#2021-11-30-13:12:32.000_000Z");

            result.Should().Be(1638277952000000);
        }

        [Test]
        public void Parse_ValidTimeExample2_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.Parse<long>("DT#2022-01-01-06:00:00.100_100Z");

            result.Should().Be(1641016800100100);
        }

        [Test]
        [TestCase("DT#2023-05-18-11:08:00Z", true)]
        [TestCase("DT#2023-05-18-11:08:00.123456Z", true)]
        [TestCase("DT#2023-05-18-11:08:00", false)]
        [TestCase("2023-05-18-11:08:00Z", false)]
        [TestCase(null, false)]
        public void IsValid_WhenCalled_ShouldBeExpected(string? value, bool expected)
        {
            var result = Radix.DateTime.IsValid(value);

            result.Should().Be(expected);
        }
    }
}