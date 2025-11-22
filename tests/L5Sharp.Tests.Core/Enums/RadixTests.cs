using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Default_Predefined_ShouldBeNullRadix()
        {
            var radix = Radix.Default(typeof(STRING));

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Default_Real_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(REAL));

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Default_Bool_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(BOOL));

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Sint_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(SINT));

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Int_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(INT));

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Dint_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(DINT));

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Lint_ShouldBeDecimalRadix()
        {
            var radix = Radix.Default(typeof(LINT));

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void ToString_WhenCalled_ReturnsTheValue()
        {
            Radix.Null.ToString().Should().Be(Radix.Null.Value);
            Radix.Binary.ToString().Should().Be(Radix.Binary.Value);
            Radix.Octal.ToString().Should().Be(Radix.Octal.Value);
            Radix.Hex.ToString().Should().Be(Radix.Hex.Value);
            Radix.Decimal.ToString().Should().Be(Radix.Decimal.Value);
            Radix.Float.ToString().Should().Be(Radix.Float.Value);
            Radix.Exponential.ToString().Should().Be(Radix.Exponential.Value);
            Radix.Ascii.ToString().Should().Be(Radix.Ascii.Value);
            Radix.DateTime.ToString().Should().Be(Radix.DateTime.Value);
            Radix.DateTimeNs.ToString().Should().Be(Radix.DateTimeNs.Value);
        }
    }
}