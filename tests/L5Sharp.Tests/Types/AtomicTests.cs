using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class AtomicTests
    {
        [Test]
        public void ParseValue_InvalidString_ShouldThrowFormatException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Atomic.Parse(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void ParseValue_ValidBinary_ShouldBeExpectedValue()
        {
            const string value = "2#0000_0101";

            var parsed = Atomic.Parse(value);

            parsed.ToString(Radix.Decimal).Should().Be("5");
        }

        [Test]
        public void ParseValue_ValidOctal_ShouldBeExpectedValue()
        {
            const string value = "8#005";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidDecimal_ShouldBeExpectedValue()
        {
            const string value = "5";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidHex_ShouldBeExpectedValue()
        {
            const string value = "16#05";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidAscii_ShouldBeExpectedValue()
        {
            const string value = "'$05'";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidFloat_ShouldBeExpected()
        {
            const string value = "5.0";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5.0f);
        }

        [Test]
        public void ParseValue_ValidExponential_ShouldBeExpected()
        {
            const string value = "5.00000000e+000";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(5.0f);
        }

        [Test]
        public void ParseValue_ValidDateTime_ShouldBeExpected()
        {
            const string value = "DT#2022-01-01-06:00:00.000_000Z";

            var parsed = Atomic.Parse(value);

            parsed.Should().Be(1641016800000000);
        }
    }
}