using System;
using System.Globalization;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class RealTests
    {
        private float _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<float>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new Real();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new Real();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(Real).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("RSLogix representation of a System.Single");
            type.DataFormat.Should().Be(TagDataFormat.Decorated);
            type.Radix.Should().Be(Radix.Decimal);
            type.Value.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Real(_random);
            
            type.Value.Should().Be(_random);
        }

        [Test]
        public void New_RadixOverload_ShouldHaveExpectedRadix()
        {
            var type = new Real(Radix.Exponential);

            type.Radix.Should().Be(Radix.Exponential);
        }

        [Test]
        public void SetValue_Null_ShouldReturnExpected()
        {
            var type = new Real();

            FluentActions.Invoking(() => type.SetValue(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_ValidValue_ShouldReturnExpected()
        {
            var type = new Real();

            type.SetValue(_random);

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_ValidObjectValue_ShouldReturnExpected()
        {
            var type = new Real();

            type.SetValue((object) _random);

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldReturnExpected()
        {
            var type = new Real();

            type.SetValue(_random.ToString(CultureInfo.InvariantCulture));

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_InvalidString_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Real(_random);

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Could not parse string '{value}' to {typeof(Real)}");
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Real();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Value type '{value.GetType()}' is not a valid for {typeof(Real)}");
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldBeExpected()
        {
            var type = new Real();
            
            type.SetRadix(Radix.Exponential);

            type.Radix.Should().Be(Radix.Exponential);
        }
        
        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var type = new Real();

            FluentActions.Invoking(() => type.SetRadix(Radix.Decimal)).Should().Throw<RadixNotSupportedException>();
        }
        
        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var type = new Real();

            FluentActions.Invoking(() => type.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Real();

            var value = type.SupportsRadix(Radix.Float);

            value.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_Float_ShouldBeFalse()
        {
            var type = new Real();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeFalse();
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            var type = new Real();

            type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            var type = new Real(_random);

            float value = type;

            value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Real();
            var second = new Real();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new Real();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new Real();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Real();
            var second = new Real();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Real();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Real();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Real();
            var second = new Real();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Real();
            var second = new Real();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new Real();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new Real();
            var second = new Real();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}