using System;
using FluentAssertions;
using L5Sharp.Exceptions;
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
            FluentActions.Invoking(() => Radix.Decimal.Format(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowRadixNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Format(new Real())).Should().Throw<RadixNotSupportedException>();
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
            FluentActions.Invoking(() => Radix.Decimal.Parse(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Parse_Invalid_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse("null")).Should().Throw<ArgumentException>()
                .WithMessage("Input value 'null' not valid for Decimal Radix.");
        }

        [Test]
        public void Parse_Zero_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse("0");

            value.Should().Be(0);
        }
        
        
        [Test]
        public void Parse_One_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse("1");

            value.Should().Be(1);
        }

        [Test]
        public void Parse_Byte_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse(byte.MaxValue.ToString());

            value.Should().Be(byte.MaxValue);
        }
        
        [Test]
        public void Parse_Short_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse(short.MaxValue.ToString());

            value.Should().Be(short.MaxValue);
        }
        
        [Test]
        public void Parse_Int_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse(int.MaxValue.ToString());

            value.Should().Be(int.MaxValue);
        }
        
        [Test]
        public void Parse_Long_ShouldBeExpected()
        {
            var value = Radix.Decimal.Parse(long.MaxValue.ToString());

            value.Should().Be(long.MaxValue);
        }
    }
}