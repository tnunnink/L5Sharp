using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class BoolTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new Bool();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new Bool();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(Bool).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("RSLogix representation of a System.Boolean");
            type.DataFormat.Should().Be(TagDataFormat.Decorated);
            type.Radix.Should().Be(Radix.Decimal);
            type.Value.Should().BeFalse();
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Bool(true);
            
            type.Value.Should().BeTrue();
        }

        [Test]
        public void New_RadixOverload_ShouldHaveExpectedRadix()
        {
            var type = new Bool(Radix.Octal);

            type.Radix.Should().Be(Radix.Octal);
        }

        [Test]
        public void SetValue_Null_ShouldReturnExpected()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetValue_Zero_ShouldReturnExpected()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(0)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue(value);

            type.Value.Should().Be(value);
        }
        
        [Test]
        public void SetValue_ValidValueAsObject_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue((object) value);

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue(value.ToString());

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_ValidValueBoolOne_ShouldBeTrue()
        {
            var type = new Bool();

            type.SetValue("1");

            type.Value.Should().BeTrue();
        }

        [Test]
        public void SetValue_ValidValueBoolYes_ShouldBeTrue()
        {
            var type = new Bool();
            
            type.SetValue("Yes");

            type.Value.Should().BeTrue();
        }

        [Test]
        public void SetValue_ValidValueBoolZero_ShouldBeFalse()
        {
            var type = new Bool();
            
            type.SetValue("0");

            type.Value.Should().BeFalse();
        }

        [Test]
        public void SetValue_ValidValueBoolNo_ShouldBeFalse()
        {
            var type = new Bool();
            
            type.SetValue("No");

            type.Value.Should().BeFalse();
        }

        [Test]
        public void SetValue_InvalidString_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Could not parse string '{value}' to {typeof(Bool)}");
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var type = new Sint();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Value type '{value.GetType()}' is not a valid for {typeof(Bool)}");
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldBeExpected()
        {
            var type = new Bool();
            
            type.SetRadix(Radix.Binary);

            type.Radix.Should().Be(Radix.Binary);
        }
        
        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.SetRadix(Radix.Float)).Should().Throw<RadixNotSupportedException>();
        }
        
        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Bool();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            var type = new Bool();

            type = true;

            type.Value.Should().BeTrue();
        }
        
        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            var type = new Bool();

            bool value = type;

            value.Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_Float_ShouldBeFalse()
        {
            var type = new Bool();

            var value = type.SupportsRadix(Radix.Float);

            value.Should().BeFalse();
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bool();
            var second = new Bool();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bool();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new Bool();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bool();
            var second = new Bool();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bool();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Bool();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bool();
            var second = new Bool();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Bool();
            var second = new Bool();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new Bool();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new Bool();
            var second = new Bool();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}