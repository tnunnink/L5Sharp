using System;
using System.Globalization;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
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
            type.Value.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Real(_random);
            
            type.Value.Should().Be(_random);
        }

        [Test]
        public void Update_Null_ShouldReturnExpected()
        {
            var type = new Real();

            FluentActions.Invoking(() => type.Update(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_ValidValue_ShouldReturnExpected()
        {
            var type = new Real();

            var updated = type.Update(_random);

            updated.Value.Should().Be(_random);
        }
        
        [Test]
        public void Update_ValidObjectValue_ShouldReturnExpected()
        {
            var type = new Real();

            var updated = type.Update((object) _random);

            updated.Value.Should().Be(_random);
        }

        [Test]
        public void Update_ValidStringValue_ShouldReturnExpected()
        {
            var type = new Real();

            var updated = type.Update("1.34");

            updated.Value.Should().Be(1.34f);
        }
        
        [Test]
        public void Update_InvalidString_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Real(_random);

            FluentActions.Invoking(() => type.Update(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Could not parse string '{value}' to {typeof(Real)}. Verify that the string is an accepted Radix format.");
        }
        
        [Test]
        public void Update_InvalidType_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Real();

            FluentActions.Invoking(() => type.Update(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Value type '{value.GetType()}' is not a valid for {typeof(Real)}");
        }
        
        [Test]
        public void Instantiate_WhenCalled_ShouldEqualDefaultInstance()
        {
            var type = new Real(_random);

            var instance = type.Instantiate();

            instance.Should().BeEquivalentTo(new Real());
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            Real type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            float value = new Real(_random);

            value.Should().Be(_random);
        }
        
        [Test]
        public void ImplicitOperator_ValidString_ShouldBeExpected()
        {
            Real type = "1.1";

            type.Value.Should().Be(1.1f);
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
        public void GetHashCode_DefaultValue_ShouldBeZero()
        {
            var type = new Real();

            var hash = type.GetHashCode();

            hash.Should().Be(0);
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