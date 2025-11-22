using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixBinaryTests
    {
        [Test]
        public void Binary_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Binary;

            radix.Should().NotBeNull();
            radix.Name.Should().Be("Binary");
            radix.Value.Should().Be("Binary");
        }

        [Test]
        public void Binary_WhenCalled_ShouldBeSameAsOtherInstance()
        {
            var first = Radix.Binary;
            var second = Radix.Binary;

            first.Should().BeSameAs(second);
        }

        [Test]
        [TestCase("2#1")]
        [TestCase("2#0001")]
        [TestCase("2#0100_0110")]
        [TestCase("2#0100_0110_0100_0110")]
        [TestCase("2#0100_0110_0100_0110_0100_0110_0100_0110")]
        public void IsValid_ValidFormats_ShouldBeTrue(string value)
        {
            var radix = Radix.Binary;

            var result = radix.IsValidFormat(value);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase("10110110")]
        [TestCase("1234")]
        [TestCase("1.234")]
        [TestCase("8#1")]
        [TestCase("16#34")]
        [TestCase("'$A4$E9'")]
        [TestCase("This is a test")]
        public void IsValid_InvalidFormats_ShouldBeFalse(string value)
        {
            var radix = Radix.Binary;

            var result = radix.IsValidFormat(value);

            result.Should().BeFalse();
        }

        [Test]
        [TestCase(false, "2#0")]
        [TestCase(true, "2#1")]
        public void Format_Boolean_ShouldBeExpected(bool value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "2#0000_0000")]
        [TestCase(1, "2#0000_0001")]
        [TestCase(123, "2#0111_1011")]
        [TestCase(sbyte.MinValue, "2#1000_0000")]
        [TestCase(sbyte.MaxValue, "2#0111_1111")]
        public void Format_SByte_ShouldBeExpectedFormat(sbyte value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "2#0000_0000")]
        [TestCase(1, "2#0000_0001")]
        [TestCase(123, "2#0111_1011")]
        [TestCase(byte.MaxValue, "2#1111_1111")]
        public void Format_Byte_ShouldBeExpectedFormat(byte value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "2#0000_0000_0000_0000")]
        [TestCase(1, "2#0000_0000_0000_0001")]
        [TestCase(123, "2#0000_0000_0111_1011")]
        [TestCase(short.MinValue, "2#1000_0000_0000_0000")]
        [TestCase(short.MaxValue, "2#0111_1111_1111_1111")]
        public void Format_Short_ShouldBeExpectedFormat(short value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase((ushort)0, "2#0000_0000_0000_0000")]
        [TestCase((ushort)1, "2#0000_0000_0000_0001")]
        [TestCase((ushort)123, "2#0000_0000_0111_1011")]
        [TestCase(ushort.MaxValue, "2#1111_1111_1111_1111")]
        public void Format_UShort_ShouldBeExpectedFormat(ushort value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0, "2#0000_0000_0000_0000_0000_0000_0000_0000")]
        [TestCase(1, "2#0000_0000_0000_0000_0000_0000_0000_0001")]
        [TestCase(123, "2#0000_0000_0000_0000_0000_0000_0111_1011")]
        [TestCase(int.MinValue, "2#1000_0000_0000_0000_0000_0000_0000_0000")]
        [TestCase(int.MaxValue, "2#0111_1111_1111_1111_1111_1111_1111_1111")]
        public void Format_Int_ShouldBeExpectedFormat(int value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase(0L, "2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000")]
        [TestCase(1L, "2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001")]
        [TestCase(123L, "2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0111_1011")]
        [TestCase(long.MinValue, "2#1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000")]
        [TestCase(long.MaxValue, "2#0111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111")]
        public void Format_Long_ShouldBeExpectedFormat(long value, string expected)
        {
            var radix = Radix.Binary;

            var result = radix.Format(value);

            result.Should().Be(expected);
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse<int>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse<int>("00010100"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse<int>("2#"))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCase("2#0", false)]
        [TestCase("2#1", true)]
        public void Parse_BoolValue_ShouldBeFalse(string value, bool expected)
        {
            var atomic = Radix.Binary.Parse<bool>(value);

            atomic.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000", 0)]
        [TestCase("2#0000_0001", 1)]
        [TestCase("2#1000_0000", sbyte.MinValue)]
        [TestCase("2#0111_1111", sbyte.MaxValue)]
        public void Parse_SByteValue_ShouldBeExpected(string value, sbyte expected)
        {
            var result = Radix.Binary.Parse<sbyte>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000", 0)]
        [TestCase("2#0000_0001", 1)]
        [TestCase("2#1111_1111", byte.MaxValue)]
        public void Parse_ByteValue_ShouldBeExpected(string value, byte expected)
        {
            var result = Radix.Binary.Parse<byte>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000", 0)]
        [TestCase("2#0000_0000_0000_0001", 1)]
        [TestCase("2#1000_0000_0000_0000", short.MinValue)]
        [TestCase("2#0111_1111_1111_1111", short.MaxValue)]
        public void Parse_ShortValue_ShouldBeExpected(string value, short expected)
        {
            var result = Radix.Binary.Parse<short>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000", (ushort)0)]
        [TestCase("2#0000_0000_0000_0001", (ushort)1)]
        [TestCase("2#1111_1111_1111_1111", ushort.MaxValue)]
        public void Parse_UShortValue_ShouldBeExpected(string value, ushort expected)
        {
            var result = Radix.Binary.Parse<ushort>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000", 0)]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0001", 1)]
        [TestCase("2#1000_0000_0000_0000_0000_0000_0000_0000", int.MinValue)]
        [TestCase("2#0111_1111_1111_1111_1111_1111_1111_1111", int.MaxValue)]
        public void Parse_IntValue_ShouldBeExpected(string value, int expected)
        {
            var result = Radix.Binary.Parse<int>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000", (uint)0)]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0001", (uint)1)]
        [TestCase("2#1111_1111_1111_1111_1111_1111_1111_1111", uint.MaxValue)]
        public void Parse_IntValue_ShouldBeExpected(string value, uint expected)
        {
            var result = Radix.Binary.Parse<uint>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000", 0L)]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001", 1L)]
        [TestCase("2#1000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000", long.MinValue)]
        [TestCase("2#0111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111", long.MaxValue)]
        public void Parse_LongValue_ShouldBeExpected(string value, long expected)
        {
            var result = Radix.Binary.Parse<long>(value);

            result.Should().Be(expected);
        }

        [Test]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000", (ulong)0L)]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001", (ulong)1L)]
        [TestCase("2#1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111", ulong.MaxValue)]
        public void Parse_LongValue_ShouldBeExpected(string value, ulong expected)
        {
            var result = Radix.Binary.Parse<ulong>(value);

            result.Should().Be(expected);
        }
    }
}