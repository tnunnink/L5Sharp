using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class LintTests
    {
        private long _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<long>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new LINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new LINT();
            
            type.Name.Should().Be(nameof(LINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Should().Be(0);
        }
        
        [Test]
        public void New_ValidRadix_ShouldHaveExpectedValues()
        {
            var type = new LINT(Radix.Hex);

            type.Radix.Should().Be(Radix.Hex);
            type.ToString().Should().Be("16#0000_0000_0000_0000");
        }

        [Test]
        public void New_NullRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new LINT(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new LINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            LINT.MaxValue.Should().Be(long.MaxValue);
        }

        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            LINT.MinValue.Should().Be(long.MinValue);
        }

        [Test]
        public void Members_PositiveValue_ShouldHaveBitsEqualToOne()
        {
            var type = new LINT(123);

            var bitsEqualToOne = type.Members.Where(m => m.DataType > 0).ToList();

            bitsEqualToOne.Should().HaveCount(6);
        }
        
        [Test]
        public void GetBit_ValidIndex_ShouldBeExpected()
        {
            var type = new LINT(1);

            var bit = type[0];

            bit.Should().Be(true);
        }

        [Test]
        public void GetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new LINT(1);

            FluentActions.Invoking(() => type[70]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetBit_ValidIndex_ShouldHaveExpectedValue()
        {
            var type = new LINT
            {
                [0] = true
            };

            type.Should().Be(1);
        }

        [Test]
        public void SetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new LINT();
            
            FluentActions.Invoking(() => type[70] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void ToBits_WhenCalled_ReturnsExpected()
        {
            var type = new LINT();

            var bits = type.ToBits();

            bits.Should().NotBeNull();
            bits.Length.Should().Be(64);

            foreach (bool bit in bits)
            {
                bit.Should().BeFalse();
            }
        }

        [Test]
        public void ToBits_PositiveValue_ReturnsExpected()
        {
            var type = new LINT(1);

            var bits = type.ToBits();

            bits.Should().NotBeNull();
            bits[0].Should().BeTrue();
        }

        [Test]
        public void ToBytes_WhenCalled_ReturnsExpected()
        {
            var expected = BitConverter.GetBytes(_random);
            var type = new LINT(_random);

            var bytes = type.ToBytes();

            CollectionAssert.AreEqual(bytes, expected);
        }

        [Test]
        public Task Serialize_Default_ShouldBeValid()
        {
            var type = new LINT();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Value_ShouldBeValid()
        {
            var type = new LINT(123);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
        
        [Test]
        public Task Serialize_ValueAndRadix_ShouldBeValid()
        {
            var type = new LINT(123, Radix.Hex);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
        
        [Test]
        public void Set_SameType_ShouldBeExpected()
        {
            var type = new LINT();

            type.Set(new LINT(_random));

            type.Should().Be(_random);
        }

        [Test]
        public void Set_SmallerType_ShouldBeExpected()
        {
            var type = new LINT();

            type.Set(new SINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Set_LargeValueSmallerType_ShouldBeExpected()
        {
            var type = new LINT(LINT.MaxValue);

            type.Set(new SINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Set_LargerTypeValidValue_ShouldBeExpected()
        {
            var type = new LINT();

            type.Set(new ULINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Set_LargerTypeLargerValue_ShouldHaveDataLoss()
        {
            var type = new LINT();

            type.Set(new ULINT(ULINT.MaxValue));

            type.Should().NotBe(LINT.MaxValue);
        }

        [Test]
        public void Set_InvalidType_ShouldThrowArgumentException()
        {
            var type = new LINT();

            FluentActions.Invoking(() => type.Set(new ComplexType("Test"))).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Conversion_FromLong_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();

            LINT type = value;

            type.Should().Be(value);
        }

        [Test]
        public void Conversion_ToLong_ShouldBeExpectedValue()
        {
            var type = new LINT();

            long value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_FromString_ShouldBeExpectedValue()
        {
            LINT value = "1";

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ToString_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            string value = type;

            value.Should().Be("1");
        }

        [Test]
        public void Conversion_BOOL_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (BOOL)type;

            value.Should().Be(true);
        }

        [Test]
        public void Conversion_SINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (SINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_USINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (USINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_INT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (INT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_UINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (UINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_DINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (DINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_UDINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (UDINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ULINT_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            var value = (ULINT)type;

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_REAL_ShouldBeExpectedValue()
        {
            var type = new LINT(1);

            REAL value = type;

            value.Should().Be(1);
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new LINT();

            var format = type.ToString();

            format.Should().Be("0");
        }

        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new LINT();

            var format = type.ToString(Radix.DateTime);

            format.Should().Be("DT#1970-01-01-00:00:00.000_000Z");
        }

        [Test]
        [TestCase("123")]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0111_1011")]
        [TestCase("8#0_000_000_000_000_000_000_173")]
        [TestCase("16#0000_0000_0000_007b")]
        [TestCase("'$00$00$00$00$00$00$00{'")]
        public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
        {
            var type = LINT.Parse(value);

            type.Should().BeOfType<LINT>();
            type.Should().Be(123);
        }

        [Test]
        public void Parse_InvalidFormat_ShouldThrowNewFormatException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => LINT.Parse(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void Equals_TypedAreEqual_ShouldBeTrue()
        {
            var first = new LINT();
            var second = new LINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypedAreSame_ShouldBeTrue()
        {
            var first = new LINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void Equals_TypedNull_ShouldBeFalse()
        {
            var first = new LINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectAreEqual_ShouldBeTrue()
        {
            var first = new LINT();
            var second = new LINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectAreNotEqual_ShouldBeFalse()
        {
            var first = new LINT(1);
            var second = new LINT(2);

            var result = first.Equals((object)second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectAreSame_ShouldBeTrue()
        {
            var first = new LINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectNull_ShouldBeFalse()
        {
            var first = new LINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
        {
            var first = new LINT(1);
            var second = new DINT(1);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
        {
            var first = new LINT(1);
            var second = new DINT(2);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals((object)second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_InvalidType_ShouldBeFalse()
        {
            var first = new LINT(0);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(new TIMER());

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new LINT();
            var second = new LINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new LINT(1);
            var second = new LINT(2);

            var result = first == second;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new LINT();
            var second = new LINT();

            var result = first != second;

            result.Should().BeFalse();
        }
        
        [Test]
        public void OperatorNotEquals_AreNotEqual_ShouldBeTrue()
        {
            var first = new LINT(1);
            var second = new LINT(2);

            var result = first != second;

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new LINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }

        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new LINT();
            var second = new LINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new LINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new LINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ObjectEqual_ShouldBeZero()
        {
            var a = new LINT(123);
            var b = new LINT(123);

            var result = a.CompareTo((object)b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_ObjectAGreater_ShouldBeOne()
        {
            var a = new LINT(2);
            var b = new LINT(1);

            var result = a.CompareTo((object)b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_ObjectBGreater_ShouldBeNegativeOne()
        {
            var a = new LINT(1);
            var b = new LINT(2);

            var result = a.CompareTo((object)b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_ObjectSame_ShouldBeZero()
        {
            var type = new LINT(1);

            var result = type.CompareTo((object)type);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_ObjectNull_ShouldBeOne()
        {
            var type = new LINT();

            var result = type.CompareTo(((object)null)!);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_ObjectDifferentAtomicWithEqualValue_ShouldBeZero()
        {
            var a = new LINT(1);
            var b = new DINT(1);

            var result = a.CompareTo((object)b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_ObjectDifferentAtomicWithLesserValue_ShouldBeOne()
        {
            var a = new LINT(1);
            var b = new DINT(0);

            var result = a.CompareTo((object)b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_ObjectDifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
        {
            var a = new LINT(1);
            var b = new DINT(2);

            var result = a.CompareTo((object)b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_ObjectInvalidType_ShouldThrowArgumentException()
        {
            var a = new LINT();
            var b = new TIMER();

            FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void OperatorGreaterThan_AGreaterThanB_ShouldBeTrue()
        {
            var a = new LINT(2);
            var b = new LINT(1);

            var result = a > b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThan_ALessThanB_ShouldBeFalse()
        {
            var a = new LINT(1);
            var b = new LINT(2);

            var result = a > b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_AGreaterThanB_ShouldBeFalse()
        {
            var a = new LINT(2);
            var b = new LINT(1);

            var result = a < b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_ALessThanB_ShouldBeTrue()
        {
            var a = new LINT(1);
            var b = new LINT(2);

            var result = a < b;
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorGreaterThanOrEqualTo_AGreaterThanB_ShouldBeTrue()
        {
            var a = new LINT(2);
            var b = new LINT(1);

            var result = a >= b;
            
            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_ALessThanB_ShouldBeFalse()
        {
            var a = new LINT(1);
            var b = new LINT(2);

            var result = a >= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_AGreaterThanB_ShouldBeFalse()
        {
            var a = new LINT(2);
            var b = new LINT(1);

            var result = a <= b;
            
            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_ALessThanB_ShouldBeTrue()
        {
            var a = new LINT(1);
            var b = new LINT(2);

            var result = a <= b;
            
            result.Should().BeTrue();
        }
    }
}