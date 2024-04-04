using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class UIntTests
    {
        private ushort _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<ushort>();
        }
        
         [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new UINT();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_ValidRadix_ShouldHaveExpectedValues()
        {
            var type = new UINT(Radix.Binary);

            type.Radix.Should().Be(Radix.Binary);
            type.ToString().Should().Be("2#0000_0000_0000_0000");
        }

        [Test]
        public void New_NullRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new UINT((Radix)null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new UINT(Radix.Exponential)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new UINT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(UINT).ToUpper());
            type.Members.Should().BeEmpty();
            type.Should().Be(0);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new UINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public Task Serialize_Default_ShouldBeValid()
        {
            var type = new UINT();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Value_ShouldBeValid()
        {
            var type = new UINT(123);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
        
        [Test]
        public Task Serialize_ValueAndRadix_ShouldBeValid()
        {
            var type = new UINT(123, Radix.Hex);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void Conversion_FromUshort_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<ushort>();

            UINT type = value;

            type.Should().Be(value);
        }

        [Test]
        public void Conversion_ToUshort_ShouldBeExpectedValue()
        {
            var type = new UINT();

            ushort value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_FromString_ShouldBeExpectedValue()
        {
            UINT value = "1";

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ToString_ShouldBeExpectedValue()
        {
            var type = new UINT(1);

            string value = type;

            value.Should().Be("1");
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new UINT();

            var format = type.ToString();

            format.Should().Be("0");
        }

        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new UINT();

            var format = type.ToString(Radix.Binary);

            format.Should().Be("2#0000_0000_0000_0000");
        }

        [Test]
        [TestCase("123")]
        [TestCase("2#0000_0000_0111_1011")]
        [TestCase("8#000_173")]
        [TestCase("16#007b")]
        public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
        {
            var type = UINT.Parse(value);

            type.Should().Be(123);
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_AreNotEqual_ShouldBeFalse()
        {
            var first = new UINT(1);
            var second = new UINT(2);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new UINT();

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new UINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
        {
            var first = new UINT(1);
            var second = new DINT(1);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
        {
            var first = new UINT(1);
            var second = new DINT(2);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_InvalidType_ShouldBeFalse()
        {
            var first = new UINT(0);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(new TIMER());

            result.Should().BeFalse();
        }
        
        [Test]
        public void EquivalentTo_AreEqual_ShouldBeTrue()
        {
            var first = new UINT(1);
            var second = new UINT(1);

            var result = first.EquivalentTo(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void EquivalentTo_AreNotEqual_ShouldBeFalse()
        {
            var first = new UINT(1);
            var second = new UINT(0);

            var result = first.EquivalentTo(second);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new UINT(1);
            var second = new UINT(2);

            var result = first == second;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorNotEquals_AreNotEqual_ShouldBeTrue()
        {
            var first = new UINT(1);
            var second = new UINT(2);

            var result = first != second;

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new UINT();

            var hash = type.GetHashCode();

            hash.Should().Be(0);
        }

        [Test]
        public void GetHashCode_Value_ShouldBeHashOfName()
        {
            var type = new UINT(_random);

            var hash = type.GetHashCode();

            hash.Should().Be(_random.GetHashCode());
        }

        [Test]
        [TestCase((ushort)0)]
        [TestCase((ushort)1)]
        [TestCase((ushort)123)]
        public void CompareTo_Equal_ShouldBeZero(ushort value)
        {
            var a = new UINT(value);
            var b = new UINT(value);

            var result = a.CompareTo(b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new UINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new UINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_AGreater_ShouldBeOne()
        {
            var a = new UINT(2);
            var b = new UINT(1);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_BGreater_ShouldBeNegativeOne()
        {
            var a = new UINT(1);
            var b = new UINT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithEqualValue_ShouldBeZero()
        {
            var a = new UINT(1);
            var b = new DINT(1);

            var result = a.CompareTo(b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithLesserValue_ShouldBeOne()
        {
            var a = new UINT(1);
            var b = new DINT(0);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
        {
            var a = new UINT(1);
            var b = new DINT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_InvalidType_ShouldThrowArgumentException()
        {
            var a = new UINT();
            var b = new TIMER();

            FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void OperatorGreaterThan_AGreaterThanB_ShouldBeTrue()
        {
            var a = new UINT(2);
            var b = new UINT(1);

            var result = a > b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThan_ALessThanB_ShouldBeFalse()
        {
            var a = new UINT(1);
            var b = new UINT(2);

            var result = a > b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_AGreaterThanB_ShouldBeFalse()
        {
            var a = new UINT(2);
            var b = new UINT(1);

            var result = a < b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_ALessThanB_ShouldBeTrue()
        {
            var a = new UINT(1);
            var b = new UINT(2);

            var result = a < b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_AGreaterThanB_ShouldBeTrue()
        {
            var a = new UINT(2);
            var b = new UINT(1);

            var result = a >= b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_ALessThanB_ShouldBeFalse()
        {
            var a = new UINT(1);
            var b = new UINT(2);

            var result = a >= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_AGreaterThanB_ShouldBeFalse()
        {
            var a = new UINT(2);
            var b = new UINT(1);

            var result = a <= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_ALessThanB_ShouldBeTrue()
        {
            var a = new UINT(1);
            var b = new UINT(2);

            var result = a <= b;
            
            result.Should().BeTrue();
        }
    }
}