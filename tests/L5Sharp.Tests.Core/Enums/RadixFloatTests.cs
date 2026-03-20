using System.Globalization;
using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
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
        [TestCase("0.0")]
        [TestCase("1.0")]
        [TestCase("1.23")]
        [TestCase("-1.23")]
        [TestCase("+1.23")]
        public void Infer_ValidFormats_ShouldBeExpected(string value)
        {
            var radix = Radix.Infer(value);

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Format_Zero_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(0.0);

            result.Should().Be("0.0");
        }

        [Test]
        public void Format_ValidReal_ShouldBeExpectedFormat()
        {
            var fixture = new Fixture();
            var value = fixture.Create<float>();
            var result = Radix.Float.Format(value);

            result.Should().Be(value.ToString("0.0###", CultureInfo.InvariantCulture));
        }

        [Test]
        public void Format_CustomRealSevenDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(0.1234567f);

            result.Should().Be("0.1234567");
        }

        [Test]
        public void Format_CustomRealMoreThanFourDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(0.12345678f);

            result.Should().Be("0.1234568");
        }

        [Test]
        public void Format_CustomRealOneDecimal_ShouldBeExpectedFormat()
        {
            var result = Radix.Float.Format(1234.5f);

            result.Should().Be("1234.5");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Float.Parse<float>(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Empty_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Float.Parse<float>(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Float_ShouldBeExpected()
        {
            var value = 1.23.ToString(CultureInfo.InvariantCulture);

            var result = Radix.Float.Parse<float>(value);

            result.Should().Be(1.23f);
        }

        [Test]
        [TestCase("1.#QNAN")]
        [TestCase("-1.#QNAN")]
        [TestCase("1.#IND")]
        [TestCase("-1.#IND")]
        public void Parse_Nan_ShouldBeExpected(string value)
        {
            var result = Radix.Float.Parse<float>(value);

            result.Should().Be(float.NaN);
        }
    }
}