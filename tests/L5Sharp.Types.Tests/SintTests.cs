using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class SintTests
    {
        private byte _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<byte>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new Sint();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new Sint();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(Sint).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("Logix representation of a System.Byte");
            type.Value.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Sint(_random);
            
            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void GetValue_AsAtomic_ShouldBeExpected()
        {
            var type = (IAtomicType) new Sint();

            type.Value.Should().Be(0);
        }

        [Test]
        public void SetValue_Null_ShouldBeExpected()
        {
            var type = new Sint();

            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_ValidType_ShouldBeExpected()
        {
            var type = new Sint();

            type.SetValue(_random);

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_ObjectValidValue_ShouldBeExpected()
        {
            var type = new Sint();

            type.SetValue((object) _random);

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_ObjectSint_ShouldBeExpected()
        {
            var type = new Sint();

            type.SetValue((object) new Sint(_random));

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldBeExpected()
        {
            var type = new Sint();

            type.SetValue(_random.ToString());

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Sint(_random);

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Could not parse string '{value}' to {typeof(Sint)}. Verify that the string is an accepted Radix format.");
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var type = new Sint();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Value type '{value.GetType()}' is not a valid for {typeof(Sint)}");
        }
        
        [Test]
        public void Instantiate_WhenCalled_ShouldEqualDefaultInstance()
        {
            var type = new Sint(_random);

            var instance = type.Instantiate();

            instance.Should().BeEquivalentTo(new Sint());
        }

        [Test]
        public void ImplicitOperator_Sint_ShouldBeExpected()
        {
            Sint type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_byte_ShouldBeExpected()
        {
            byte value = new Sint(_random);

            value.Should().Be(_random);
        }
        
        [Test]
        public void ImplicitOperator_ValidString_ShouldBeExpected()
        {
            Sint type = _random.ToString();

            type.Value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Sint();
            var second = new Sint();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new Sint();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new Sint();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Sint();
            var second = new Sint();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Sint();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Sint();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Sint();
            var second = new Sint();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Sint();
            var second = new Sint();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new Sint();

            var hash = type.GetHashCode();

            hash.Should().Be(type.Name.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new Sint();

            type.ToString().Should().Be(type.Name);
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new Sint();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new Sint();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new Sint();
            var second = new Sint();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}