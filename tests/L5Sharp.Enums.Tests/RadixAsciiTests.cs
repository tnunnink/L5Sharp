using System;
using FluentAssertions;
using L5Sharp.Atomics;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixAsciiTests
    {
        [Test]
        public void Ascii_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Ascii;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Ascii);
        }

        [Test]
        public void Format_AsciiValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Sint(20));

            result.Should().Be("'$14'");
        }

        [Test]
        public void Format_Ascii_ShouldBeExpected()
        {
            var ascii = Radix.Ascii.Format(new Dint(123456));

            ascii.Should().Be("'$00$01$E2@'");
        }

        [Test]
        public void Format_AsciiValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Int(20));

            result.Should().Be("'$00$14'");
        }

        [Test]
        public void Format_AsciiValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Dint(20));

            result.Should().Be("'$00$00$00$14'");
        }

        [Test]
        public void Format_AsciiValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Lint(20));

            result.Should().Be("'$00$00$00$00$00$00$00$14'");
        }

        [Test]
        public void Format_AllValuesFrom32To126_ShouldMatchTheConvertedCharacter()
        {
            for (sbyte i = 32; i < 127; i++)
            {
                var value = new Sint(i);

                var formatted = Radix.Ascii.Format(value);
                var expected = Convert.ToChar(i).ToString();

                formatted.Should().Be($"'{expected}'");
            }
        }

        [Test]
        public void Format_127ToMaxByte_ShouldBeHexValue()
        {
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            const string invalid = "00@";

            FluentActions.Invoking(() => Radix.Ascii.Parse(invalid)).Should().Throw<FormatException>()
                .WithMessage($"Input '{invalid}' does not have expected Ascii format.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse("''")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_LengthTooLarge_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse("'$00$00$00$00$00$00$00$00$00'")).Should()
                .Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void Parse_Tab_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$t'");

            atomic.Value.Should().Be(9);
        }
        
        [Test]
        public void Parse_LineFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$l'");

            atomic.Value.Should().Be(10);
        }
        
        [Test]
        public void Parse_FormFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$p'");

            atomic.Value.Should().Be(12);
        }
        
        [Test]
        public void Parse_Return_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$r'");

            atomic.Value.Should().Be(13);
        }
        
        [Test]
        public void Parse_DollarSign_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$$'");

            atomic.Value.Should().Be(36);
        }
        
        [Test]
        public void Parse_SingleQuote_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse("'$''");

            atomic.Value.Should().Be(39);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse("'$14'");

            value.Should().Be(new Sint(20));
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse("'$00$01$E2@'");

            value.Should().Be(new Dint(123456));
        }
        
        [Test]
        public void Parse_Valid_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse("'$12D$F1A'");

            value.Should().Be(new Dint(306508097));
        }
    }
}