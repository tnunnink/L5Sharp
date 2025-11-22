using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixDecimalTests
    {
        [Test]
        public void Decimal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Decimal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        [TestCase(false, "0")]
        [TestCase(true, "1")]
        public void Format_Bool_ShouldBeExpectedFormat(bool value, string expected)
        {
            var result = Radix.Decimal.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(20, "20")]
        public void Format_Byte_ShouldBeExpectedFormat(sbyte value, string expected)
        {
            var result = Radix.Decimal.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(20, "20")]
        public void Format_Short_ShouldBeExpectedFormat(short value, string expected)
        {
            var result = Radix.Decimal.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(-1, "-1")]
        [TestCase(127, "127")]
        [TestCase(-127, "-127")]
        [TestCase(1234567, "1234567")]
        [TestCase(-1234567, "-1234567")]
        public void Format_Int_ShouldBeExpectedFormat(int value, string expected)
        {
            var result = Radix.Decimal.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(20, "20")]
        public void Format_Long_ShouldBeExpectedFormat(long value, string expected)
        {
            var result = Radix.Decimal.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse<int>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Invalid_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse<int>("null"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_FloatNumber_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse<int>("123.456"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_InvalidLength_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse<long>("92233720368547758070"))
                .Should().Throw<OverflowException>();
        }

        [Test]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("127", 127)]
        [TestCase("-127", -127)]
        [TestCase("255", 255)]
        [TestCase("32767", 32767)]
        [TestCase("-32767", -32767)]
        [TestCase("2147483647", 2147483647)]
        [TestCase("-2147483647", -2147483647)]
        public void Parse_IntValues_ShouldHaveExpectedResult(string value, int expected)
        {
            var result = Radix.Decimal.Parse<int>(value);

            result.Should().Be(expected);
        }
    }
}