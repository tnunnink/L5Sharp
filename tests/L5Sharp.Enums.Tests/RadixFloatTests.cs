using System;
using System.Globalization;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Atomics;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixFloatTests
    {
        [Test]
        public void Float_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Float;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Float);
        }
        
        [Test]
        public void Format_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Float.Format(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Format_NonSupportedAtomic_ShouldThrowRadixNotSupportedException()
        {
            FluentActions.Invoking(() => Radix.Float.Format(new Dint())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Format_Zero_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(new Real());

            result.Should().Be("0.0");
        }
        
        [Test]
        public void Format_ValidReal_ShouldBeExpectedFormat()
        {
            var fixture = new Fixture();
            var value = fixture.Create<float>();
            var result = Radix.Float.Format(new Real(value));

            result.Should().Be(value.ToString("0.0###", CultureInfo.InvariantCulture));
        }
        
        [Test]
        public void Format_CustomRealSevenDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(new Real(0.1234567f));

            result.Should().Be("0.1234567");
        }
        
        [Test]
        public void Format_CustomRealMoreThanFourDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(new Real(0.12345678f));

            result.Should().Be("0.1234568");
        }

        [Test]
        public void Format_CustomRealOneDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(new Real(1234.5f));

            result.Should().Be("1234.5");
        }
        
        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Float.Parse(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Parse_Empty_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Float.Parse(string.Empty)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_Float_ShouldBeExpected()
        {
            var result = Radix.Float.Parse(1.23.ToString(CultureInfo.InvariantCulture));

            result.Value.Should().Be(1.23f);
        }
    }
}