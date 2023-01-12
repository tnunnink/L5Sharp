using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
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
            var type = new REAL();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new REAL();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(REAL).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            //type.Description.Should().Be("Logix representation of a System.Single");
            type.Should().Be(0);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new REAL(_random);
            
            type.Should().Be(_random);
        }
        
        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            REAL.MaxValue.Should().Be(float.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            REAL.MinValue.Should().Be(float.MinValue);
        }

        [Test]
        public void SetValue_ValidValue_ShouldReturnExpected()
        {
            REAL type = _random;

            type.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_SameType_ShouldReturnExpected()
        {
            var type = new REAL(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new REAL();

            var format = type.Format();

            format.Should().Be("0.0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new REAL();

            var format = type.Format(Radix.Exponential);

            format.Should().Be("0.00000000e+000");
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            REAL type = _random;

            type.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            float value = new REAL(_random);

            value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new REAL();
            var second = new REAL();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new REAL();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new REAL();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new REAL();
            var second = new REAL();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new REAL();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new REAL();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new REAL();
            var second = new REAL();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new REAL();
            var second = new REAL();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new REAL();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new REAL();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new REAL();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }
        
        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new REAL();
            var second = new REAL();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}