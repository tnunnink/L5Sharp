using FluentAssertions;

namespace L5Sharp.Tests.Enums
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
            FluentActions.Invoking(() => Radix.Decimal.FormatValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Decimal.FormatValue(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Decimal.FormatValue(false);

            result.Should().Be("0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Decimal.FormatValue(true);

            result.Should().Be("1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.FormatValue(new SINT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.FormatValue(new INT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.FormatValue(new DINT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Decimal.FormatValue(new LINT(20));

            result.Should().Be("20");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Decimal.ParseValue(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Invalid_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Decimal.ParseValue("null")).Should().Throw<FormatException>()
                .WithMessage("Input 'null' does not have expected Decimal format.");
        }

        [Test]
        public void Parse_InvalidLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Decimal.ParseValue("92233720368547758070"))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Input '92233720368547758070' is out of range for the Decimal Radix. (Parameter 'Input')");
        }

        [Test]
        public void Parse_sbyte_ShouldBeExpected()
        {
            var value = (sbyte)Radix.Decimal.ParseValue(sbyte.MaxValue.ToString());

            value.Should().Be(new SINT(sbyte.MaxValue));
        }
        
        [Test]
        public void Parse_byte_ShouldBeExpected()
        {
            var value = (byte)Radix.Decimal.ParseValue(byte.MaxValue.ToString());

            value.Should().Be(new USINT(byte.MaxValue));
        }

        [Test]
        public void Parse_short_ShouldBeExpected()
        {
            var value = (short)Radix.Decimal.ParseValue(short.MaxValue.ToString());

            value.Should().Be(new INT(short.MaxValue));
        }
        
        [Test]
        public void Parse_ushort_ShouldBeExpected()
        {
            var value = (ushort)Radix.Decimal.ParseValue(ushort.MaxValue.ToString());

            value.Should().Be(new UINT(ushort.MaxValue));
        }

        [Test]
        public void Parse_int_ShouldBeExpected()
        {
            var value = (int)Radix.Decimal.ParseValue(int.MaxValue.ToString());

            value.Should().Be(int.MaxValue);
        }
        
        [Test]
        public void Parse_uint_ShouldBeExpected()
        {
            var value = (uint)Radix.Decimal.ParseValue(uint.MaxValue.ToString());

            value.Should().Be(uint.MaxValue);
        }

        [Test]
        public void Parse_long_ShouldBeExpected()
        {
            var value = (long)Radix.Decimal.ParseValue(long.MaxValue.ToString());

            value.Should().Be(long.MaxValue);
        }
        
        [Test]
        public void Parse_ulong_ShouldBeExpected()
        {
            var value = (ulong)Radix.Decimal.ParseValue(ulong.MaxValue.ToString());

            value.Should().Be(ulong.MaxValue);
        }
    }
}