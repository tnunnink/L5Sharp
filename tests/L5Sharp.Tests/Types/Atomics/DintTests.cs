using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class DintTests
    {
        private int _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<int>();
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new DINT();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new DINT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(DINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().HaveCount(32);
            type.Should().Be(0);
        }

        [Test]
        public void New_RadixOverload_ShouldHaveExpectedFormat()
        {
            var type = new DINT(Radix.Binary);

            var formatted = type.ToString();

            formatted.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000");
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new DINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void GetBit_ValidIndex_ShouldBeExpected()
        {
            var type = new DINT(1);

            var bit0 = type[0];
            var bit1 = type[1];

            bit0.Should().Be(true);
            bit1.Should().Be(false);
        }

        [Test]
        public void GetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new DINT(1);

            FluentActions.Invoking(() => type[32]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetBit_ValidIndex_ShouldHaveExpectedValue()
        {
            var type = new DINT();

            type[0] = true;

            type.Should().Be(1);
        }

        [Test]
        public void SetBit_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new DINT();
            
            FluentActions.Invoking(() => type[32] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            DINT.MaxValue.Should().Be(int.MaxValue);
        }

        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            DINT.MinValue.Should().Be(int.MinValue);
        }

        [Test]
        public void ToString_DefaultRadix_ShouldBeExpected()
        {
            var type = new DINT();

            var format = type.ToString();

            format.Should().Be("0");
        }

        [Test]
        public void ToString_OverloadedRadix_ShouldBeExpected()
        {
            var type = new DINT();

            var format = type.ToString(Radix.Binary);

            format.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000");
        }

        [Test]
        public void ToBits_WhenCalled_ReturnsExpected()
        {
            var type = new DINT();

            var bits = type.ToBits();

            bits.Should().NotBeNull();
            bits.Length.Should().Be(32);

            foreach (bool bit in bits)
            {
                bit.Should().BeFalse();
            }
        }

        [Test]
        public void ToBits_PositiveValue_ReturnsExpected()
        {
            var type = new DINT(1);

            var bits = type.ToBits();

            bits[0].Should().BeTrue();
        }

        [Test]
        public void ToBytes_WhenCalled_ReturnsExpected()
        {
            var expected = BitConverter.GetBytes(_random);
            var type = new DINT(_random);

            var bytes = type.ToBytes();

            CollectionAssert.AreEqual(bytes, expected);
        }

        [Test]
        public Task Serialize_Default_ShouldBeValid()
        {
            var type = new DINT();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Value_ShouldBeValid()
        {
            var type = new DINT(123);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void Update_SameType_ShouldBeExpected()
        {
            var type = new DINT();

            type.Set(new DINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_SmallerType_ShouldBeExpected()
        {
            var type = new DINT();

            type.Set(new INT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargerValueSmallerType_ShouldBeExpected()
        {
            var type = new DINT(DINT.MaxValue);

            type.Set(new INT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargerTypeWithSmallerValue_ShouldBeExpected()
        {
            var type = new DINT();

            type.Set(new LINT(123));

            type.Should().Be(123);
        }

        [Test]
        public void Update_LargerTypeLargerValue_ShouldHaveDataLoss()
        {
            var type = new DINT();

            type.Set(new LINT(LINT.MaxValue));

            type.Should().Be(-1);
        }

        [Test]
        public void Update_InvalidType_ShouldThrowArgumentException()
        {
            var type = new DINT();

            FluentActions.Invoking(() => type.Set(new ComplexType("Test"))).Should().Throw<ArgumentException>();
        }

        [Test]
        public void UpdateBitMemberDataTypeAndSeeWhatHappensToTheValue()
        {
            var type = new DINT();
            
            type.Members.First().DataType.Set(true);

            type.Should().Be(0);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        public void Set_Values_ShouldBeValue(int value)
        {
            DINT type = value;

            type.Should().Be(value);
        }

        [Test]
        public void Set_Random_ShouldBeExpected()
        {
            DINT value = _random;

            value.Should().Be(_random);
        }


        [Test]
        public void Set_Atomic_ShouldBeExpected()
        {
            int value = new DINT(_random);

            value.Should().Be(_random);
        }

        [Test]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("-1")]
        public void Set_String_ShouldBeExpected(string value)
        {
            DINT type = value;
            type.Should().Be(int.Parse(value));
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DINT();
            var second = new DINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new DINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new DINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DINT();
            var second = new DINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new DINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new DINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DINT();
            var second = new DINT();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new DINT();
            var second = new DINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new DINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new DINT();

            type.ToString().Should().Be(type.ToString());
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new DINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new DINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new DINT();
            var second = new DINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }

        [Test]
        public void CreateLargeListOfAtomicValuesToEnsurePerformance()
        {
            var array = Enumerable.Range(0, 1000000).Select(i => new DINT(i)).ToList();

            array.Should().NotBeEmpty();
            array.Should().AllBeOfType<DINT>();
        }
    }
}