using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
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

            var result = radix.Format((sbyte)20);

            result.Should().Be("'$14'");
        }

        [Test]
        public void Format_AsciiValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format((short)20);

            result.Should().Be("'$00$14'");
        }

        [Test]
        public void Format_AsciiValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(20);

            result.Should().Be("'$00$00$00$14'");
        }

        [Test]
        public void Format_AsciiValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format((long)20);

            result.Should().Be("'$00$00$00$00$00$00$00$14'");
        }

        [Test]
        public void Format_LargeValue_ShouldBeExpected()
        {
            var ascii = Radix.Ascii.Format(123456);

            ascii.Should().Be("'$00$01$E2@'");
        }

        [Test]
        [TestCase(9, "'$t'")]
        [TestCase(10, "'$l'")]
        [TestCase(12, "'$p'")]
        [TestCase(13, "'$r'")]
        [TestCase(36, "'$$'")]
        public void Format_SpecialCharacter_ShouldBeExpected(sbyte number, string expected)
        {
            var radix = Radix.Ascii;

            var formatted = radix.Format(number);

            formatted.Should().Be(expected);
        }

        [Test]
        public void Format_AllValuesFrom32To126_ShouldMatchTheConvertedCharacter()
        {
            for (sbyte i = 32; i < 127; i++)
            {
                //These are special characters
                if (i is 36 or 39) continue;

                var formatted = Radix.Ascii.Format(i);
                var expected = Convert.ToChar(i).ToString();
                formatted.Should().Be($"'{expected}'");
            }
        }

        [Test]
        public void Parse_Null_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse<int>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse<byte>("00@"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse<byte>("''"))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_LengthTooLarge_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Ascii.Parse<int>("'$FF$FF$FF$FF$FF$FF$FF$FF$FF'"))
                .Should().Throw<OverflowException>();
        }

        [Test]
        public void Parse_Tab_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$t'");

            atomic.Should().Be(9);
        }

        [Test]
        public void Parse_LineFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$l'");

            atomic.Should().Be(10);
        }

        [Test]
        public void Parse_FormFeed_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$p'");

            atomic.Should().Be(12);
        }

        [Test]
        public void Parse_Return_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$r'");

            atomic.Should().Be(13);
        }

        [Test]
        public void Parse_DollarSign_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$$'");

            atomic.Should().Be(36);
        }

        [Test]
        public void Parse_SingleQuote_ShouldBeExpected()
        {
            var atomic = Radix.Ascii.Parse<byte>("'$''");

            atomic.Should().Be(39);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse<byte>("'$14'");

            value.Should().Be(20);
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse<int>("'$00$01$E2@'");

            value.Should().Be(new DINT(123456));
        }

        [Test]
        public void Parse_Valid_ShouldBeExpected()
        {
            var value = Radix.Ascii.Parse<int>("'$12D$F1A'");

            value.Should().Be(new DINT(306508097));
        }
    }
}