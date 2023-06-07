using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class UDintTests
    {
        private uint _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<uint>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new UDINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new UDINT();
            
            type.Name.Should().Be(nameof(UDINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new UDINT(_random);
            
            type.Should().Be(_random);
        }
        
        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            UDINT.MaxValue.Should().Be(uint.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            UDINT.MinValue.Should().Be(uint.MinValue);
        }

        [Test]
        public void SetValue_ValidShort_ShouldBeExpected()
        {
            UDINT type = _random;

            type.Should().Be(_random);
        }

        [Test]
        public void SetValue_SameType_ShouldBeExpected()
        {
            var type = new UDINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new UDINT();

            var format = type.ToString();

            format.Should().Be("0");
        }
        
        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new UDINT();

            var format = type.ToString(Radix.Binary);

            format.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000");
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UDINT();
            var second = new UDINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new UDINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new UDINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UDINT();
            var second = new UDINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new UDINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new UDINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UDINT();
            var second = new UDINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new UDINT();
            var second = new UDINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new UDINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new UDINT();

            type.ToString().Should().Be(type.ToString());
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new UDINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new UDINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new UDINT();
            var second = new UDINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}