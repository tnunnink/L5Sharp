using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class SintTests
    {
        private sbyte _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<sbyte>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new SINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new SINT();
            
            type.Name.Should().Be(nameof(SINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Should().Be(0);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new SINT(_random);
            
            type.Should().Be(_random);
        }

        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            SINT.MaxValue.Should().Be(sbyte.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            SINT.MinValue.Should().Be(sbyte.MinValue);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(127)]
        [TestCase(-127)]
        public void Set_ValidValues_ShouldBeExpectedValue(sbyte value)
        {
            SINT type = value;

            type.Should().Be(value);
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new SINT();

            var format = type.ToString();

            format.Should().Be("0");
        }
        
        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new SINT();

            var format = type.ToString(Radix.Binary);

            format.Should().Be("2#0000_0000");
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new SINT();

            // ReSharper disable once EqualExpressionComparison this is for testing purposes
            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new SINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new SINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeValue()
        {
            var type = new SINT();

            type.ToString().Should().Be("0");
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new SINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new SINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new SINT();
            var second = new SINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}