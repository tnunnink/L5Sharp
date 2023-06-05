using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
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
            var type = (LogixType)new BOOL();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsType_ComplexAsIDataType_ShouldBeFalse()
        {
            var type = (LogixType)new STRING();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
        }
    }
}