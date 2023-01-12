using System;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
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
            FluentActions.Invoking(() => Radix.Octal.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Octal.Format(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Octal.Format(new BOOL());

            result.Should().Be("8#0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Octal.Format(new BOOL(true));

            result.Should().Be("8#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Format(new SINT(20));

            result.Should().Be("8#024");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Format(new INT(20));

            result.Should().Be("8#000_024");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Format(new DINT(20));

            result.Should().Be("8#000_000_000_024");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Octal.Format(new LINT(20));

            result.Should().Be("8#000_000_000_000_000_000_000_024");
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
        public void Parse_LengthZero_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse("8#")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_OutOfRangeValue_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse(
                "8#000_000_000_000_000_000_000_000_024")).Should().Throw<ArgumentOutOfRangeException>();
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