using System;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixBinaryTests
    {
        [Test]
        public void Binary_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Binary;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Binary.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Binary.Format(new REAL())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Binary.Format(new BOOL());

            result.Should().Be("2#0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Binary.Format(new BOOL(true));

            result.Should().Be("2#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new SINT(20));

            result.Should().Be("2#0001_0100");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new INT(20));

            result.Should().Be("2#0000_0000_0001_0100");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new DINT(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new LINT(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_NoSpecifier_ShouldThrowArgumentException()
        {
            const string invalid = "00010100";

            FluentActions.Invoking(() => Radix.Binary.Parse(invalid)).Should().Throw<FormatException>()
                .WithMessage($"Input '{invalid}' does not have expected Binary format.");
        }

        [Test]
        public void Parse_LengthZero_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse("2#")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_LengthGreaterThan68_ShouldThrowArgumentOutOfRangeException()
        {
            var result = Radix.Binary.Parse(
                "2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0100_0000");

            result.Should().NotBeNull();
        }

        [Test]
        public void Parse_BoolFalse_ShouldBeFalse()
        {
            var atomic = Radix.Binary.Parse("2#0");

            atomic.Value.Should().Be(false);
        }
        
        [Test]
        public void Parse_BoolTrue_ShouldBeTrue()
        {
            var atomic = Radix.Binary.Parse("2#1");

            atomic.Value.Should().Be(true);
        }

        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse("2#0001_0100");

            value.Should().Be(new SINT(20));
        }

        [Test]
        public void Parse_ValidInt_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse("2#0000_0000_0001_0100");

            value.Should().Be(new INT(20));
        }

        [Test]
        public void Parse_ValidDint_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse("2#0000_0000_0000_0000_0000_0000_0001_0100");

            value.Should().Be(new DINT(20));
        }


        [Test]
        public void Parse_ValidLint_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse(
                "2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0100");

            value.Should().Be(new LINT(20));
        }
    }
}