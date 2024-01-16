using FluentAssertions;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class RadixDateTimeTests
    {
        [Test]
        public void DateTime_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTime;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTime);
        }

        [Test]
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.DateTime.FormatValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.DateTime.FormatValue(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_ValidTimeExample1_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.FormatValue(new LINT(1638277952000000));

            result.Should().Be("DT#2021-11-30-13:12:32.000_000Z");
        }

        [Test]
        public void Format_ValidTimeExample2_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.FormatValue(new LINT(1641016800100100));

            result.Should().Be("DT#2022-01-01-06:00:00.100_100Z");
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Radix.DateTime.ParseValue("2021-11-30-07:12:32.000_000Z")).Should()
                .Throw<FormatException>();
        }

        [Test]
        public void Parse_ValidTimeExample1_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.ParseValue("DT#2021-11-30-13:12:32.000_000Z");

            result.Should().Be(1638277952000000);
        }

        [Test]
        public void Parse_ValidTimeExample2_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.ParseValue("DT#2022-01-01-06:00:00.100_100Z");

            result.Should().Be(1641016800100100);
        }
    }
}