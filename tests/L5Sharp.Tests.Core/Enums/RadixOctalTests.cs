using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixOctalTests
    {
        [Test]
        public void Octal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Octal;

            radix.Should().NotBeNull();
            radix.Name.Should().Be("Octal");
            radix.Value.Should().Be("Octal");
        }

        [Test]
        public void Octal_WhenCalled_ShouldBeSameAsOtherInstance()
        {
            var first = Radix.Octal;
            var second = Radix.Octal;

            first.Should().BeSameAs(second);
        }

        [Test]
        [TestCase("8#1")]
        [TestCase("8#0001")]
        [TestCase("8#0100_0110")]
        [TestCase("8#0100_0110_0100_0110")]
        [TestCase("8#0100_0110_0100_0110_0100_0110_0100_0110")]
        public void IsValid_ValidFormats_ShouldBeTrue(string value)
        {
            var radix = Radix.Octal;

            var result = radix.IsValidFormat(value);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase("10110110")]
        [TestCase("1234")]
        [TestCase("1.234")]
        [TestCase("2#1")]
        [TestCase("16#34")]
        [TestCase("'$A4$E9'")]
        [TestCase("This is a test")]
        public void IsValid_InvalidFormats_ShouldBeFalse(string value)
        {
            var radix = Radix.Octal;

            var result = radix.IsValidFormat(value);

            result.Should().BeFalse();
        }

        [Test]
        [TestCase(false, "8#0")]
        [TestCase(true, "8#1")]
        public void Format_Boolean_ShouldBeExpected(bool value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "8#000")]
        [TestCase(1, "8#001")]
        [TestCase(sbyte.MaxValue, "8#177")]
        public void Format_SByte_ShouldBeExpectedFormat(sbyte value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "8#000")]
        [TestCase(1, "8#001")]
        [TestCase(byte.MaxValue, "8#377")]
        public void Format_Byte_ShouldBeExpectedFormat(byte value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "8#000_000")]
        [TestCase(1, "8#000_001")]
        [TestCase(short.MaxValue, "8#077_777")]
        [TestCase(short.MinValue, "8#100_000")]
        public void Format_Short_ShouldBeExpectedFormat(short value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase((ushort)0, "8#000_000")]
        [TestCase((ushort)1, "8#000_001")]
        [TestCase(ushort.MaxValue, "8#177_777")]
        public void Format_UShort_ShouldBeExpectedFormat(ushort value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "8#00_000_000_000")]
        [TestCase(1, "8#00_000_000_001")]
        [TestCase(int.MaxValue, "8#17_777_777_777")]
        [TestCase(int.MinValue, "8#20_000_000_000")]
        public void Format_Int_ShouldBeExpectedFormat(int value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase((uint)0, "8#00_000_000_000")]
        [TestCase((uint)1, "8#00_000_000_001")]
        [TestCase(uint.MaxValue, "8#37_777_777_777")]
        public void Format_UInt_ShouldBeExpectedFormat(uint value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "8#0_000_000_000_000_000_000_000")]
        [TestCase(1, "8#0_000_000_000_000_000_000_001")]
        [TestCase(long.MaxValue, "8#0_777_777_777_777_777_777_777")]
        [TestCase(long.MinValue, "8#1_000_000_000_000_000_000_000")]
        public void Format_Long_ShouldBeExpectedFormat(long value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase((ulong)0, "8#0_000_000_000_000_000_000_000")]
        [TestCase((ulong)1, "8#0_000_000_000_000_000_000_001")]
        [TestCase(ulong.MaxValue, "8#1_777_777_777_777_777_777_777")]
        public void Format_ULong_ShouldBeExpectedFormat(ulong value, string expected)
        {
            var radix = Radix.Octal;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse<int>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse<int>("00_000_000_024"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse<int>("8#"))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_LengthGreaterThan68_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Octal.Parse<long>("8#700_000_000_000_000_000_000_000_024"))
                .Should().Throw<OverflowException>();
        }

        [Test]
        [TestCase("8#0", false)]
        [TestCase("8#1", true)]
        public void Parse_BooleanValue_ShouldBeFalse(string value, bool expected)
        {
            var atomic = Radix.Octal.Parse<bool>(value);

            atomic.Should().Be(expected);
        }

        [Test]
        [TestCase("8#000", 0)]
        [TestCase("8#001", 1)]
        [TestCase("8#177", 127)]
        public void Parse_SByteValue_ShouldBeExpected(string value, sbyte expected)
        {
            var result = Radix.Octal.Parse<sbyte>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("8#000_000", 0)]
        [TestCase("8#000_001", 1)]
        [TestCase("8#077_777", short.MaxValue)]
        public void Parse_ShortValue_ShouldBeExpected(string value, short expected)
        {
            var result = Radix.Octal.Parse<short>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("8#00_000_000_000", 0)]
        [TestCase("8#00_000_000_001", 1)]
        [TestCase("8#17_777_777_777", int.MaxValue)]
        public void Parse_IntValue_ShouldBeExpected(string value, int expected)
        {
            var result = Radix.Octal.Parse<int>(value);

            result.Should().Be(expected);
        }


        [Test]
        [TestCase("8#0_000_000_000_000_000_000_000", 0)]
        [TestCase("8#0_000_000_000_000_000_000_001", 1)]
        [TestCase("8#0_777_777_777_777_777_777_777", long.MaxValue)]
        public void Parse_ValidLint_ShouldBeExpected(string value, long expected)
        {
            var result = Radix.Octal.Parse<long>(value);

            result.Should().Be(expected);
        }
    }
}