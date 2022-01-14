using System;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
            FluentActions.Invoking(() => Radix.DateTime.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.DateTime.Format(new Real())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_ValidTimeExample1_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Format(new Lint(1638277952000000));

            result.Should().Be("DT#2021-11-30-07:12:32.000_000(UTC-06:00)");
        }

        [Test]
        public void Format_ValidTimeExample2_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Format(new Lint(1641016800100100));

            result.Should().Be("DT#2022-01-01-00:00:00.100_100(UTC-06:00)");
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Radix.DateTime.Parse("2021-11-30-07:12:32.000_000(UTC-06:00)")).Should()
                .Throw<FormatException>();
        }

        [Test]
        public void Parse_ValidTimeExample1_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.Parse("DT#2021-11-30-07:12:32.000_000(UTC-06:00)");

            result.Value.Should().Be(1638277952000000);
        }

        [Test]
        public void Parse_ValidTimeExample2_ShouldBeExpectedValue()
        {
            var radix = Radix.DateTime;

            var result = radix.Parse("DT#2022-01-01-00:00:00.100_100(UTC-06:00)");

            result.Value.Should().Be(1641016800100100);
        }
    }
}