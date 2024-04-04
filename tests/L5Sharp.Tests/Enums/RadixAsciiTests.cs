using FluentAssertions;

namespace L5Sharp.Tests.Enums
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

            var result = radix.FormatValue(new SINT(20));

            result.Should().Be("'$14'");
        }

        [Test]
        public void Format_AsciiValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.FormatValue(new INT(20));

            result.Should().Be("'$00$14'");
        }

        [Test]
        public void Format_AsciiValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.FormatValue(new DINT(20));

            result.Should().Be("'$00$00$00$14'");
        }

        [Test]
        public void Format_AsciiValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.FormatValue(new LINT(20));

            result.Should().Be("'$00$00$00$00$00$00$00$14'");
        }

        [Test]
        public void Format_LargeValue_ShouldBeExpected()
        {
            var ascii = Radix.Ascii.FormatValue(new DINT(123456));

            ascii.Should().Be("'$00$01$E2@'");
        }
        
        [Test]
        public void Format_SpecialCharacterTab_ShouldBeExpected()
        {
            var atomic = new SINT(9);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$t'");
        }
        
        [Test]
        public void Format_SpecialCharacterLineFeed_ShouldBeExpected()
        {
            var atomic = new SINT(10);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$l'");
        }
        
        [Test]
        public void Format_SpecialCharacterFormFeed_ShouldBeExpected()
        {
            var atomic = new SINT(12);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$p'");
        }
        
        [Test]
        public void Format_SpecialCharacterCarriageReturn_ShouldBeExpected()
        {
            var atomic = new SINT(13);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$r'");
        }
        
        [Test]
        public void Format_SpecialCharacterDollarSign_ShouldBeExpected()
        {
            var atomic = new SINT(36);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$$'");
        }
        
        [Test]
        public void Format_SpecialCharacterSingleQuote_ShouldBeExpected()
        {
            var atomic = new SINT(39);
            
            var formatted = Radix.Ascii.FormatValue(atomic);

            formatted.Should().Be("'$''");
        }

        [Test]
        public void Format_AllValuesFrom32To126_ShouldMatchTheConvertedCharacter()
        {
            for (sbyte i = 32; i < 127; i++)
            {
                //These are special characters
                if (i is 36 or 39) continue;
                
                var value = new SINT(i);

                var formatted = Radix.Ascii.FormatValue(value);
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
            FluentActions.Invoking(() => Radix.Ascii.ParseValue(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            const string invalid = "00@";

            FluentActions.Invoking(() => Radix.Ascii.ParseValue(invalid)).Should().Throw<FormatException>()
                .WithMessage($"Input '{invalid}' does not have expected Ascii format.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Ascii.ParseValue("''")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_LengthTooLarge_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => Radix.Ascii.ParseValue("'$00$00$00$00$00$00$00$00$00'")).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Parse_Tab_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$t'");

            atomic.Should().Be(9);
        }

        [Test]
        public void Parse_LineFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$l'");

            atomic.Should().Be(10);
        }

        [Test]
        public void Parse_FormFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$p'");

            atomic.Should().Be(12);
        }

        [Test]
        public void Parse_Return_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$r'");

            atomic.Should().Be(13);
        }

        [Test]
        public void Parse_DollarSign_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$$'");

            atomic.Should().Be(36);
        }

        [Test]
        public void Parse_SingleQuote_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.ParseValue("'$''");

            atomic.Should().Be(39);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Ascii.ParseValue("'$14'");

            value.Should().Be(new SINT(20));
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Ascii.ParseValue("'$00$01$E2@'");

            value.Should().Be(new DINT(123456));
        }

        [Test]
        public void Parse_Valid_ShouldBeExpected()
        {
            var value = Radix.Ascii.ParseValue("'$12D$F1A'");

            value.Should().Be(new DINT(306508097));
        }
    }
}