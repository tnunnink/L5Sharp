using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
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
            type.Description.Should().Be("Logix representation of a System.Boolean");
            type.Value.Should().BeFalse();
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Bool(true);

            type.Value.Should().BeTrue();
        }

        [Test]
        public void New_IntZeroOverload_ShouldBeFalse()
        {
            var type = new Bool(0);

            type.Value.Should().BeFalse();
        }

        [Test]
        public void New_IntPositiveOverload_ShouldBeTrue()
        {
            var type = new Bool(1);

            type.Value.Should().BeTrue();
        }

        [Test]
        public void New_IntNegativeOverload_ShouldBeFalse()
        {
            var type = new Bool(-1);

            type.Value.Should().BeFalse();
        }

        [Test]
        public void GetValue_AsAtomic_ShouldBeExpected()
        {
            var type = (IAtomicType) new Bool();

            type.Value.Should().Be(false);
        }

        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_Zero_ShouldThrowArgumentException()
        {
            var type = new Bool();

            type.SetValue(0);

            type.Value.Should().Be(false);
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

            type.SetValue((object)value);

            type.Value.Should().Be(value);
        }
        
        [Test]
        public void SetValue_boolAsObject_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue((object)value);

            type.Value.Should().Be(value);
        }
        
        [Test]
        public void SetValue_BoolAsObject_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue((object)new Bool(value));

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();
            
            type.SetValue(value ? "1" : "0");

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_ValidStringOne_ShouldBeTrue()
        {
            var type = new Bool();

            type.SetValue("1");
            
            type.Value.Should().Be(true);
        }

        [Test]
        public void SetValue_ValidStringZero_ShouldBeFalse()
        {
            var type = new Bool();

            type.SetValue("0");

            type.Value.Should().Be(false);
        }
        
        [Test]
        public void SetValue_ValidStringTrue_ShouldBeTrue()
        {
            var type = new Bool();

            type.SetValue("true");
            
            type.Value.Should().Be(true);
        }

        [Test]
        public void SetValue_ValidStringFalse_ShouldBeFalse()
        {
            var type = new Bool();

            type.SetValue("false");

            type.Value.Should().Be(false);
        }

        [Test]
        public void SetValue_InvalidString_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetValue_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<float>();
            var type = new Bool();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new Bool();

            var format = type.Format();

            format.Should().Be("0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new Bool();

            var format = type.Format(Radix.Binary);

            format.Should().Be("2#0");
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldEqualDefaultInstance()
        {
            var type = new Bool(true);

            var instance = type.Instantiate();

            instance.Should().BeEquivalentTo(new Bool());
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            Bool type = true;

            type.Value.Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            bool value = new Bool();

            value.Should().BeFalse();
        }
        
        [Test]
        public void ImplicitOperator_ValidString_ShouldBeTrue()
        {
            Bool type = "1";

            type.Value.Should().BeTrue();
        }
        
        [Test]
        public void ImplicitOperator_InvalidString_ShouldThrowArgumentException()
        {
            Bool type = false;
            
            FluentActions.Invoking(() => type = "test").Should().Throw<ArgumentException>();
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
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new Bool();

            var hash = type.GetHashCode();

            hash.Should().Be(type.Name.GetHashCode());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new Bool();

            type.ToString().Should().Be(type.Name);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new Bool();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new Bool();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
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