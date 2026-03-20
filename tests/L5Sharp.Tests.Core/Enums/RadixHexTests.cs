using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixHexTests
    {
        [Test]
        public void Hex_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Hex;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Hex);
        }

        [Test]
        public void Format_ValidBoolFalse_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(false);

            result.Should().Be("16#0");
        }

        [Test]
        public void Format_ValidBoolTrue_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(true);

            result.Should().Be("16#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format((sbyte)20);

            result.Should().Be("16#14");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format((short)20);

            result.Should().Be("16#0014");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(1234567);

            result.Should().Be("16#0012_d687");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(20L);

            result.Should().Be("16#0000_0000_0000_0014");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse<int>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse<int>("0000_0024"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse<int>("16#"))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_OutOfRangeValue_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse<short>("16#0000_0000_0000_0000_0024_0000"))
                .Should().Throw<OverflowException>();
        }

        [Test]
        public void Parse_ValidBool_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse<bool>("16#0");

            value.Should().Be(false);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse<sbyte>("16#14");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidInt_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse<short>("16#0014");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse<int>("16#0000_0014");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidLint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse<long>("16#0000_0000_0000_0014");

            value.Should().Be(20);
        }
    }
}