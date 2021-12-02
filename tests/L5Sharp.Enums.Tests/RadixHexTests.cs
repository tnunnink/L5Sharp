using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
            FluentActions.Invoking(() => Radix.Hex.Format(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowRadixNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Hex.Format(new Real())).Should().Throw<RadixNotSupportedException>();
        }
        
        [Test]
        public void Format_ValidBoolFalse_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(new Bool());

            result.Should().Be("16#0");
        }
        
        [Test]
        public void Format_ValidBoolTrue_ShouldBeExpected()
        {
            var result = Radix.Hex.Format(new Bool(true));

            result.Should().Be("16#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new Sint(20));

            result.Should().Be("16#14");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new Int(20));

            result.Should().Be("16#0014");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new Dint(20));

            result.Should().Be("16#0000_0014");
        }
        
        [Test]
        public void Format_Dint1234567_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new Dint(1234567));

            result.Should().Be("16#0012_d687");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Hex.Format(new Lint(20));

            result.Should().Be("16#0000_0000_0000_0014");
        }
        
        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse("0000_0024")).Should().Throw<ArgumentException>()
                .WithMessage("Input must start with Hex specifier '16#'.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse("16#"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value 0 is out of range for Hex Radix. (Parameter 'Length')");
        }

        [Test]
        public void Parse_LengthGreaterThan16_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Hex.Parse(
                    "16#00000_0000_0000_0024"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value 17 is out of range for Hex Radix. (Parameter 'Length')");
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