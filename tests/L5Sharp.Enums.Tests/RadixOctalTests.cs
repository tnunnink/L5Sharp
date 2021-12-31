using System;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixOctalTests
    {
        [Test]
        public void Octal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Octal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Octal);
        }
        
        [Test]
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Octal.Convert(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Octal.Convert(new Real())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Octal.Convert(new Bool());

            result.Should().Be("8#0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Octal.Convert(new Bool(true));

            result.Should().Be("8#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Convert(new Sint(20));

            result.Should().Be("8#024");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Convert(new Int(20));

            result.Should().Be("8#000_024");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Convert(new Dint(20));

            result.Should().Be("8#00_000_000_024");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Convert(new Lint(20));

            result.Should().Be("8#0_000_000_000_000_000_000_024");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse("00_000_000_024")).Should().Throw<FormatException>()
                .WithMessage("Input '00_000_000_024' does not have expected Octal format.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse("8#"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value 0 is out of range for Octal Radix. (Parameter 'Length')");
        }

        [Test]
        public void Parse_LengthGreaterThan22_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse(
                        "8#00_000_000_000_000_000_000_024"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value 23 is out of range for Octal Radix. (Parameter 'Length')");
        }

        [Test]
        public void Parse_ValidBool_ShouldBeExpected()
        {
            var value = Radix.Octal.Parse("8#0");

            value.Should().Be(false);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Octal.Parse("8#024");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidInt_ShouldBeExpected()
        {
            var value = Radix.Octal.Parse("8#000_024");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Octal.Parse("8#00_000_000_024");

            value.Should().Be(20);
        }


        [Test]
        public void Parse_ValidLint_ShouldBeExpected()
        {
            var value = Radix.Octal.Parse("8#0_000_000_000_000_000_000_024");

            value.Should().Be(20);
        }
    }
}