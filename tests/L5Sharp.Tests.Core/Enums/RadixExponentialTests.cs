using System.Globalization;
using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixExponentialTests
    {
        [Test]
        public void Exponential_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Exponential;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Exponential);
        }
        
        [Test]
        [TestCase("0.00000000e+00")]
        [TestCase("1.23000000e+02")]
        [TestCase("-1.23000000e+02")]
        [TestCase("+1.23000000e+02")]
        public void Infer_ValidFormats_ShouldBeExpected(string value)
        {
            var radix = Radix.Infer(value);

            radix.Should().Be(Radix.Exponential);
        }

        [Test]
        public void Format_Zero_ShouldBeExpectedFormat()
        {
            var result = Radix.Exponential.Format(0.0f);

            result.Should().Be("0.00000000e+000");
        }

        [Test]
        public void Format_ValidReal_ShouldBeExpectedFormat()
        {
            var fixture = new Fixture();
            var value = fixture.Create<float>();
            var result = Radix.Exponential.Format(value);

            result.Should().Be(value.ToString("e8", CultureInfo.InvariantCulture));
        }

        [Test]
        public void Format_CustomRealSevenDecimal_ShouldBeExpectedFormat()
        {
            const float value = 1.123e3f;

            var result = Radix.Exponential.Format(value);

            result.Should().Be("1.12300000e+003");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Exponential.Parse<float>(null!))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Empty_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Radix.Exponential.Parse<float>(string.Empty))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_Exponential_ShouldBeExpected()
        {
            var result = Radix.Exponential.Parse<float>("1.12300000e+002");

            result.Should().Be(112.3f);
        }
        
        [Test]
        [TestCase("1.#QNAN")]
        [TestCase("-1.#QNAN")]
        [TestCase("1.#IND")]
        [TestCase("-1.#IND")]
        public void Parse_Nan_ShouldBeExpected(string value)
        {
            var result = Radix.Exponential.Parse<float>(value);

            result.Should().Be(float.NaN);
        }
    }
}