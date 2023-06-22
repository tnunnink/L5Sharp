using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

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
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new INT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(INT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().HaveCount(16);
            type.Should().Be(0);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new INT(_random);

            type.Should().Be(_random);
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

            var bitsEqualToOne = members.Where(m => m.DataType.As<BOOL>() == true).ToList();

            bitsEqualToOne.Should().NotBeEmpty();
        }
        
        [Test]
        public void GetBit_ValidIndex_ShouldBeExpected()
        {
            var type = new INT(1);

            var bit = type[0];

            bit.Should().Be(true);
        }

        [Test]
        public void GetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new INT(1);

            FluentActions.Invoking(() => type[32]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetBit_ValidIndex_ShouldHaveExpectedValue()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var type = new INT();

            type[0] = true;

            type.Should().Be(1);
        }

        [Test]
        public void SetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new INT();
            
            FluentActions.Invoking(() => type[32] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void ToBits_WhenCalled_ReturnsExpected()
        {
            var type = new INT();

            var bits = type.ToBits();

            bits.Should().NotBeNull();
            bits.Length.Should().Be(16);

            foreach (bool bit in bits)
            {
                bit.Should().BeFalse();
            }
        }

        [Test]
        public void ToBits_PositiveValue_ReturnsExpected()
        {
            var type = new INT(1);

            var bits = type.ToBits();

            bits.Should().NotBeNull();
            bits[0].Should().BeTrue();
        }

        [Test]
        public void ToBytes_WhenCalled_ReturnsExpected()
        {
            var expected = BitConverter.GetBytes(_random);
            var type = new INT(_random);

            var bytes = type.ToBytes();

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
        public void Update_SameType_ShouldBeExpected()
        {
            var type = new INT();

            type.Set(new INT(_random));

            type.Should().Be(_random);
        }

        [Test]
        public void Update_SmallerType_ShouldBeExpected()
        {
            var type = new INT();

            type.Set(new SINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargeValueSmallerType_ShouldBeExpected()
        {
            var type = new INT(INT.MaxValue);

            type.Set(new SINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargerTypeValidValue_ShouldBeExpected()
        {
            var type = new INT();

            type.Set(new LINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargerTypeLargerValue_ShouldHaveDataLoss()
        {
            var type = new INT();

            type.Set(new LINT(LINT.MaxValue));

            type.Should().NotBe(INT.MaxValue);
        }

        [Test]
        public void Update_InvalidType_ShouldThrowArgumentException()
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
            INT value = "1";

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ToString_ShouldBeExpectedValue()
        {
            var type = new INT(1);

            string value = type;

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
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new INT();
            var second = new INT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new INT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new INT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new INT();
            var second = new INT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new INT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new INT();

            var result = first.Equals((object)null);

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
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new INT();
            var second = new INT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new INT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
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
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new INT();
            var second = new INT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}