using System;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Format(new Real())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Decimal.Format(new Bool());

            result.Should().Be("0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Decimal.Format(new Bool(true));

            result.Should().Be("1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new Sint(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new Int(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new Dint(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new Lint(20));

            result.Should().Be("20");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_Invalid_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse("null")).Should().Throw<FormatException>()
                .WithMessage("Input 'null' does not have expected Decimal format.");
        }

        [Test]
        public void Parse_Byte_ShouldBeExpected()
        {
            var atomic = (Sint)Radix.Decimal.Parse(byte.MaxValue.ToString());

            atomic.Value.Should().Be(new Sint(byte.MaxValue));
        }

        [Test]
        public void Parse_Short_ShouldBeExpected()
        {
            var atomic = (Int)Radix.Decimal.Parse(short.MaxValue.ToString());

            atomic.Value.Should().Be(new Int(short.MaxValue));
        }

        [Test]
        public void Parse_Int_ShouldBeExpected()
        {
            var atomic = (Dint)Radix.Decimal.Parse(int.MaxValue.ToString());

            atomic.Value.Should().Be(int.MaxValue);
        }

        [Test]
        public void Parse_Long_ShouldBeExpected()
        {
            var atomic = (Lint)Radix.Decimal.Parse(long.MaxValue.ToString());

            atomic.Value.Should().Be(long.MaxValue);
        }
    }
}