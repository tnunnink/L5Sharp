using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Default_UserDefined_ShouldBeNullRadix()
        {
            var dataType = new DataType { Name = "Test", Description = "This is a test" };

            var radix = Radix.Default(dataType);

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Default_Predefined_ShouldBeNullRadix()
        {
            var type = new STRING();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Default_Real_ShouldBeDecimalRadix()
        {
            var type = new REAL();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Default_Bool_ShouldBeDecimalRadix()
        {
            var type = new BOOL();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Sint_ShouldBeDecimalRadix()
        {
            var type = new SINT();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Int_ShouldBeDecimalRadix()
        {
            var type = new INT();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Dint_ShouldBeDecimalRadix()
        {
            var type = new DINT();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Lint_ShouldBeDecimalRadix()
        {
            var type = new LINT();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void ParseValue_InvalidString_ShouldThrowFormatException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Radix.ParseValue(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void ParseValue_ValidBinary_ShouldBeExpectedValue()
        {
            const string value = "2#0000_0101";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidOctal_ShouldBeExpectedValue()
        {
            const string value = "8#005";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidDecimal_ShouldBeExpectedValue()
        {
            const string value = "5";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidHex_ShouldBeExpectedValue()
        {
            const string value = "16#05";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidAscii_ShouldBeExpectedValue()
        {
            const string value = "'$05'";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidFloat_ShouldBeExpected()
        {
            const string value = "5.0";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5.0f);
        }

        [Test]
        public void ParseValue_ValidExponential_ShouldBeExpected()
        {
            const string value = "5.00000000e+000";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5.0f);
        }

        [Test]
        public void ParseValue_ValidDateTime_ShouldBeExpected()
        {
            const string value = "DT#2022-01-01-06:00:00.000_000Z";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(1641016800000000);
        }

        [Test]
        public void SupportsType_Null_ShouldBeFalse()
        {
            var result = Radix.Decimal.SupportsType(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsType_NonAtomic_ShouldBeFalse()
        {
            var type = new STRING();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsType_AtomicAsIDataType_ShouldBeTrue()
        {
            var type = (ILogixType)new BOOL();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsType_ComplexAsIDataType_ShouldBeFalse()
        {
            var type = (ILogixType)new STRING();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
        }
    }
}