using System.Diagnostics;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Atomics
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
            var type = new INT();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new INT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(INT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().HaveCount(16);
            type.Radix.Should().Be(Radix.Decimal);
            type.Should().BeEquivalentTo(0);
        }

        [Test]
        public void New_Value_ShouldHaveExpectedValues()
        {
            var type = new INT(_random);

            type.Should().BeEquivalentTo(_random);
        }

        [Test]
        public void New_ValidRadix_ShouldHaveExpectedValues()
        {
            var type = new INT(Radix.Binary);

            type.Radix.Should().Be(Radix.Binary);
            type.ToString().Should().Be("2#0000_0000_0000_0000");
        }

        [Test]
        public void New_NullRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new INT(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new INT(Radix.Exponential)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            INT.MaxValue.Should().Be(short.MaxValue);
        }

        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            INT.MinValue.Should().Be(short.MinValue);
        }

        [Test]
        public void Members_PositiveValue_ShouldHaveBitsEqualToOne()
        {
            var type = new INT(33);

            var members = type.Members.ToList();

            var bitsEqualToOne = members.Where(m => m.DataType == true).ToList();

            bitsEqualToOne.Should().NotBeEmpty();
        }

        [Test]
        public void GetBytes_WhenCalled_ReturnsExpected()
        {
            var expected = BitConverter.GetBytes(_random);
            var type = new INT(_random);

            var bytes = type.GetBytes();

            CollectionAssert.AreEqual(bytes, expected);
        }

        [Test]
        public Task Serialize_Default_ShouldBeValid()
        {
            var type = new INT();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Value_ShouldBeValid()
        {
            var type = new INT(123);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
        
        [Test]
        public Task Serialize_ValueAndRadix_ShouldBeValid()
        {
            var type = new INT(123, Radix.Hex);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
        
        [Test]
        public void Set_SameType_ShouldBeExpected()
        {
            var type = new INT();

            var value = type.Set(new INT(_random));

            value.Should().Be(_random);
        }

        [Test]
        public void Set_SmallerType_ShouldBeExpected()
        {
            var type = new INT();

            var value = type.Set(new SINT(123));

            value.Should().Be(123);
        }

        [Test]
        public void Set_LargeValueSmallerType_ShouldBeExpected()
        {
            var type = new INT(INT.MaxValue);

            var value = type.Set(new SINT(123));

            value.Should().Be(123);
        }

        [Test]
        public void Set_LargerTypeValidValue_ShouldBeExpected()
        {
            var type = new INT();

            var value = (INT)type.Set(new LINT(123));

            value.Should().Be(123);
        }

        [Test]
        public void Set_LargerTypeLargerValue_ShouldHaveDataLoss()
        {
            var type = new INT();

            var value = type.Set(new LINT(LINT.MaxValue));

            value.Should().NotBe(INT.MaxValue);
        }

        [Test]
        public void Set_InvalidType_ShouldThrowArgumentException()
        {
            var type = new INT();

            FluentActions.Invoking(() => type.Set(new ComplexType("Test"))).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Conversion_FromShort_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<short>();

            INT type = value;

            type.Should().Be(value);
        }

        [Test]
        public void Conversion_ToShort_ShouldBeExpectedValue()
        {
            var type = new INT();

            short value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_FromString_ShouldBeExpectedValue()
        {
            var value = (INT)"1";

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ToString_ShouldBeExpectedValue()
        {
            var type = new INT(1);

            var value = (string)type;

            value.Should().Be("1");
        }

        [Test]
        public void Conversion_BOOL_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (BOOL)type;

            value.Should().Be(false);
        }

        [Test]
        public void Conversion_SINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (SINT)type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_USINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (USINT)type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_UINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (UINT)type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_DINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            DINT value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_UDINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (UDINT)type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_LINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            LINT value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_ULINT_ShouldBeExpectedValue()
        {
            var type = new INT();

            var value = (ULINT)type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_REAL_ShouldBeExpectedValue()
        {
            var type = new INT();

            REAL value = type;

            value.Should().Be(0);
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new INT();

            var format = type.ToString();

            format.Should().Be("0");
        }

        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new INT();

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
            var type = INT.Parse(value);

            type.Should().Be(123);
        }

        [Test]
        public void Parse_InvalidFormat_ShouldThrowNewFormatException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => INT.Parse(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new INT();
            var second = new INT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_AreNotEqual_ShouldBeFalse()
        {
            var first = new INT(1);
            var second = new INT(2);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new INT();

            // ReSharper disable once EqualExpressionComparison this is the test.
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new INT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
        {
            var first = new INT(1);
            var second = new DINT(1);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
        {
            var first = new INT(1);
            var second = new DINT(2);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_InvalidType_ShouldBeFalse()
        {
            var first = new INT(0);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(new TIMER());

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new INT();
            var second = new INT();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new INT(1);
            var second = new INT(2);

            var result = first == second;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new INT();
            var second = new INT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorNotEquals_AreNotEqual_ShouldBeTrue()
        {
            var first = new INT(1);
            var second = new INT(2);

            var result = first != second;

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new INT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }

        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new INT();
            var second = new INT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new INT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new INT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_AGreater_ShouldBeOne()
        {
            var a = new INT(2);
            var b = new INT(1);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_BGreater_ShouldBeNegativeOne()
        {
            var a = new INT(1);
            var b = new INT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithEqualValue_ShouldBeZero()
        {
            var a = new INT(1);
            var b = new DINT(1);

            var result = a.CompareTo(b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithLesserValue_ShouldBeOne()
        {
            var a = new INT(1);
            var b = new DINT(0);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
        {
            var a = new INT(1);
            var b = new DINT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_InvalidType_ShouldThrowArgumentException()
        {
            var a = new INT();
            var b = new TIMER();

            FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void OperatorGreaterThan_AGreaterThanB_ShouldBeTrue()
        {
            var a = new INT(2);
            var b = new INT(1);

            var result = a > b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThan_ALessThanB_ShouldBeFalse()
        {
            var a = new INT(1);
            var b = new INT(2);

            var result = a > b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_AGreaterThanB_ShouldBeFalse()
        {
            var a = new INT(2);
            var b = new INT(1);

            var result = a < b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_ALessThanB_ShouldBeTrue()
        {
            var a = new INT(1);
            var b = new INT(2);

            var result = a < b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_AGreaterThanB_ShouldBeTrue()
        {
            var a = new INT(2);
            var b = new INT(1);

            var result = a >= b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_ALessThanB_ShouldBeFalse()
        {
            var a = new INT(1);
            var b = new INT(2);

            var result = a >= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_AGreaterThanB_ShouldBeFalse()
        {
            var a = new INT(2);
            var b = new INT(1);

            var result = a <= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_ALessThanB_ShouldBeTrue()
        {
            var a = new INT(1);
            var b = new INT(2);

            var result = a <= b;
            
            result.Should().BeTrue();
        }

        [Test]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void Equals_OverLargeCollection_ShouldWorkFairlyFast(int capacity)
        {
            var stopwatch = new Stopwatch();
            
            var range = Enumerable.Range(0, capacity).Select(_ => new INT(123)).ToList();
            
            stopwatch.Start();
            var result = range.Where(d => d == new INT(123)).ToList();
            stopwatch.Stop();

            result.Count.Should().Be(capacity);
        }
    }
}