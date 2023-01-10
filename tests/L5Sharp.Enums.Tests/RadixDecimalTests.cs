using System;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
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
            FluentActions.Invoking(() => Radix.Decimal.Format(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Decimal.Format(new BOOL());

            result.Should().Be("0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Decimal.Format(new BOOL(true));

            result.Should().Be("1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new SINT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new INT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new DINT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.Format(new LINT(20));

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
        public void Parse_InvalidLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Decimal.Parse("92233720368547758070"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Input '92233720368547758070' is out of range for the Decimal Radix. (Parameter 'Input')");
        }

        [Test]
        public void Parse_sbyte_ShouldBeExpected()
        {
            var atomic = (SINT)Radix.Decimal.Parse(sbyte.MaxValue.ToString());

            atomic.Value.Should().Be(new SINT(sbyte.MaxValue));
        }
        
        [Test]
        public void Parse_byte_ShouldBeExpected()
        {
            var atomic = (USINT)Radix.Decimal.Parse(byte.MaxValue.ToString());

            atomic.Value.Should().Be(new USINT(byte.MaxValue));
        }

        [Test]
        public void Parse_short_ShouldBeExpected()
        {
            var atomic = (INT)Radix.Decimal.Parse(short.MaxValue.ToString());

            atomic.Value.Should().Be(new INT(short.MaxValue));
        }
        
        [Test]
        public void Parse_ushort_ShouldBeExpected()
        {
            var atomic = (UINT)Radix.Decimal.Parse(ushort.MaxValue.ToString());

            atomic.Value.Should().Be(new UINT(ushort.MaxValue));
        }

        [Test]
        public void Parse_int_ShouldBeExpected()
        {
            var atomic = (DINT)Radix.Decimal.Parse(int.MaxValue.ToString());

            atomic.Value.Should().Be(int.MaxValue);
        }
        
        [Test]
        public void Parse_uint_ShouldBeExpected()
        {
            var atomic = (UDINT)Radix.Decimal.Parse(uint.MaxValue.ToString());

            atomic.Value.Should().Be(uint.MaxValue);
        }

        [Test]
        public void Parse_long_ShouldBeExpected()
        {
            var atomic = (LINT)Radix.Decimal.Parse(long.MaxValue.ToString());

            atomic.Value.Should().Be(long.MaxValue);
        }
        
        [Test]
        public void Parse_ulong_ShouldBeExpected()
        {
            var atomic = (ULINT)Radix.Decimal.Parse(ulong.MaxValue.ToString());

            atomic.Value.Should().Be(ulong.MaxValue);
        }

        [Test]
        public void SupportsType_Bool_ShouldBeTrue()
        {
            var type = new BOOL();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }

        [Test]
        public void SupportsType_Sint_ShouldBeTrue()
        {
            var type = new SINT();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }
        
        [Test]
        public void SupportsType_USint_ShouldBeTrue()
        {
            var type = new USINT();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }

        [Test]
        public void SupportsType_Int_ShouldBeTrue()
        {
            var type = new INT();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }

        [Test]
        public void SupportsType_Dint_ShouldBeTrue()
        {
            var type = new DINT();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }
        
        
        [Test]
        public void SupportsType_Lint_ShouldBeTrue()
        {
            var type = new LINT();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }

        [Test]
        public void SupportsType_Real_ShouldBeFalse()
        {
            var type = new REAL();

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeFalse();
        }
    }
}