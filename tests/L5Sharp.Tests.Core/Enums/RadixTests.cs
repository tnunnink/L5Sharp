using System;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class RadixTests
    {
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
            var type = (LogixData)new BOOL();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsType_ComplexAsIDataType_ShouldBeFalse()
        {
            var type = (LogixData)new STRING();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
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