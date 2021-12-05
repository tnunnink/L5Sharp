using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class IntTests
    {
        private short _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<short>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new Int();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new Int();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(Int).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("RSLogix representation of a System.Int16");
            type.Value.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new Int(_random);
            
            type.Value.Should().Be(_random);
        }

        [Test]
        public void Update_Null_ShouldReturnExpected()
        {
            var type = new Int();

            FluentActions.Invoking(() => type.Update(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_ValidValue_ShouldReturnExpected()
        {
            var type = new Int();

            type.Update(_random);

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void Update_ValidObjectValue_ShouldReturnExpected()
        {
            var type = new Int();

            type.Update((object) _random);

            type.Value.Should().Be(_random);
        }

        [Test]
        public void Update_ValidStringValue_ShouldReturnExpected()
        {
            var type = new Int();

            type.Update(_random.ToString());

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void Update_InvalidString_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new Int(_random);

            FluentActions.Invoking(() => type.Update(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Could not parse string '{value}' to {typeof(Int)}");
        }
        
        [Test]
        public void Update_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var type = new Int();

            FluentActions.Invoking(() => type.Update(value)).Should().Throw<ArgumentException>()
                .WithMessage($"Value type '{value.GetType()}' is not a valid for {typeof(Int)}");
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            Int type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            short value = new Int(_random);

            value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Int();
            var second = new Int();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new Int();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new Int();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Int();
            var second = new Int();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Int();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Int();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Int();
            var second = new Int();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Int();
            var second = new Int();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new Int();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new Int();
            var second = new Int();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}