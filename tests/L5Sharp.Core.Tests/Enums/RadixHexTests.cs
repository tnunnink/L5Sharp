using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Core.Tests.Enums
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
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Hex.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Hex.Format(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_ValidBoolFalse_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(new BOOL());

            result.Should().Be("16#0");
        }

        [Test]
        public void Format_ValidBoolTrue_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(new BOOL(true));

            result.Should().Be("16#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new SINT(20));

            result.Should().Be("16#14");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new INT(20));

            result.Should().Be("16#0014");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new DINT(20));

            result.Should().Be("16#0000_0014");
        }

        [Test]
        public void Format_Dint1234567_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new DINT(1234567));

            result.Should().Be("16#0012_d687");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new LINT(20));

            result.Should().Be("16#0000_0000_0000_0014");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse("0000_0024")).Should().Throw<FormatException>()
                .WithMessage("Input '0000_0024' does not have expected Hex format.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse("16#")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_OutOfRangeValue_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse(
                "16#0000_0000_0000_0000_0024_0000")).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Parse_ValidBool_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse("16#0");

            value.Should().Be(false);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse("16#14");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidInt_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse("16#0014");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse("16#0000_0014");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidLint_ShouldBeExpected()
        {
            var value = Radix.Hex.Parse("16#0000_0000_0000_0014");

            value.Should().Be(20);
        }
    }
}