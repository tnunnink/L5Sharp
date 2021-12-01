using System;
using FluentAssertions;
using L5Sharp.Types;
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
            FluentActions.Invoking(() => Radix.Binary.Format(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_BoolFalse_ShouldBeExpected()
        {
            var result = Radix.Binary.Format(new Bool());

            result.Should().Be("2#0");
        }

        [Test]
        public void Format_BoolTrue_ShouldBeExpected()
        {
            var result = Radix.Binary.Format(new Bool(true));
            
            result.Should().Be("2#1");
        }

        [Test]
        public void Format_ValidSint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new Sint(20));

            result.Should().Be("2#0001_0100");
        }

        [Test]
        public void Format_ValidInt_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new Int(20));

            result.Should().Be("2#0000_0000_0001_0100");
        }

        [Test]
        public void Format_ValidDint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new Dint(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Format_ValidLint_ShouldBeExpectedFormat()
        {
            var result = Radix.Binary.Format(new Lint(20));

            result.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001_0100");
        }

        [Test]
        public void Parse_ValidBool_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse("2#0");

            value.Should().Be(false);
        }
        
        [Test]
        public void Parse_ValidSint_ShouldBeExpected()
        {
            var value = Radix.Binary.Parse("2#0000_0001");

            value.Should().Be(1);
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Parse_InvalidSpecifier_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse("00010100")).Should().Throw<ArgumentException>()
                .WithMessage("Input must start with Binary specifier '2#'");
        }
        
        [Test]
        public void Parse_InvalidLength_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Radix.Binary.Parse("2#0_0010")).Should().Throw<ArgumentException>()
                .WithMessage("The value length 5 is not valid for Binary Radix");
        }
    }
}