using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Null_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Null;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Binary_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Binary;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void Octal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Octal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Octal);
        }

        [Test]
        public void Decimal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Decimal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Hex_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Hex;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Hex);
        }

        [Test]
        public void Exponential_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Exponential;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Exponential);
        }

        [Test]
        public void Float_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Float;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Ascii_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Ascii;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Ascii);
        }

        [Test]
        public void DateTime_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTime;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTime);
        }

        [Test]
        public void DateTimeNs_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTimeNs;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTimeNs);
        }

        [Test]
        public void Format_BinaryValidBool_ShouldBeExpected()
        {
            var radix = Radix.Binary;

            var result = radix.Format(new Bool());

            result.Should().Be("2#0");
        }

        [Test]
        public void Format_BinaryValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Binary;

            var result = radix.Format(new Sint(20));

            result.Should().Be("2#0001_0100");
        }

        [Test]
        public void Format_BinaryValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Binary;

            var result = radix.Format(new Int(20));

            result.Should().Be("2#0000_0000_0001_0100");
        }

        [Test]
        public void Format_BinaryValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Binary;

            var result = radix.Format(new Dint(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Format_BinaryValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Binary;

            var result = radix.Format(new Lint(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Format_OctalValidBool_ShouldBeExpected()
        {
            var radix = Radix.Octal;

            var result = radix.Format(new Bool());

            result.Should().Be("8#0");
        }

        [Test]
        public void Format_OctalValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Octal;

            var result = radix.Format(new Sint(20));

            result.Should().Be("8#024");
        }

        [Test]
        public void Format_OctalValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Octal;

            var result = radix.Format(new Int(20));

            result.Should().Be("8#000_024");
        }

        [Test]
        public void Format_OctalValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Octal;

            var result = radix.Format(new Dint(20));

            result.Should().Be("8#00_000_000_024");
        }

        [Test]
        public void Format_OctalValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Octal;

            var result = radix.Format(new Lint(20));

            result.Should().Be("8#0_000_000_000_000_000_000_024");
        }

        [Test]
        public void Format_HexValidBool_ShouldBeExpected()
        {
            var radix = Radix.Hex;

            var result = radix.Format(new Bool());

            result.Should().Be("16#0");
        }

        [Test]
        public void Format_HexValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Hex;

            var result = radix.Format(new Sint(20));

            result.Should().Be("16#14");
        }

        [Test]
        public void Format_HexValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Hex;

            var result = radix.Format(new Int(20));

            result.Should().Be("16#0014");
        }

        [Test]
        public void Format_HexValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Hex;

            var result = radix.Format(new Dint(20));

            result.Should().Be("16#0000_0014");
        }

        [Test]
        public void Format_HexValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Hex;

            var result = radix.Format(new Lint(20));

            result.Should().Be("16#0000_0000_0000_0014");
        }

        [Test]
        public void Format_FloatValidReal_ShouldBeExpectedFormat()
        {
            var radix = Radix.Float;

            var result = radix.Format(new Real(1.234f));

            result.Should().Be("1.234");
        }

        [Test]
        public void Format_ExponentialValidReal_ShouldBeExpectedFormat()
        {
            var radix = Radix.Exponential;

            var result = radix.Format(new Real(12345.6789f));

            result.Should().Be("1.23456787e+004");
        }

        [Test]
        public void Format_AsciiValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Sint(20));

            result.Should().Be("$14");
        }

        [Test]
        public void Format_AsciiValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Int(20));

            result.Should().Be("$00$14");
        }

        [Test]
        public void Format_AsciiValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Dint(20));

            result.Should().Be("$00$00$00$14");
        }

        [Test]
        public void Format_AsciiValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Lint(20));

            result.Should().Be("$00$00$00$00$00$00$00$14");
        }

        [Test]
        public void Format_DateTimeValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Format(new Lint(1638277952000000));

            result.Should().Be("DT#2021-11-30-07:12:32.000000(UTC-06:00)");
        }
        
        [Test]
        public void Format_DateTimeNsValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Format(new Lint(1638277952000000));

            result.Should().Be("LDT#1970-01-19-17:04:37.9520000(UTC-06:00)");
        }
    }
}