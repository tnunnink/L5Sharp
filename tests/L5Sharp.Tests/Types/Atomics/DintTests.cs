using System.Diagnostics;
using System.Globalization;
using AutoFixture;
using FluentAssertions;

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
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new DINT();

            type.Should().NotBeNull();
            type.Should().Be(0);
            type.Name.Should().Be(nameof(DINT).ToUpper());
            type.Members.Should().BeEmpty();
            type.Radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void New_Value_ShouldHaveExpectedValues()
        {
            var type = new DINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void New_ValidRadix_ShouldHaveExpectedValues()
        {
            var type = new DINT(Radix.Binary);

            type.Radix.Should().Be(Radix.Binary);
            type.ToString().Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000");
        }

        [Test]
        public void New_NullRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new DINT(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new DINT(Radix.Exponential)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValueAndRadix_ShouldHaveExpectedValues()
        {
            var type = new DINT(123, Radix.Hex);

            type.Should().Be(123);
            type.Radix.Should().Be(Radix.Hex);
        }

        [Test]
        public void New_ValueAndRadixNullRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new DINT(123, null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValueAndRadixInvalidRadix_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new DINT(123, Radix.Exponential)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void As_AtomicType_ShouldNotBeNull()
        {
            var type = new DINT();

            var atomic = type.As<AtomicType>();

            atomic.Should().NotBeNull();
        }

        [Test]
        public void As_InvalidType_ShouldThrowInvalidCastException()
        {
            var type = new DINT();

            FluentActions.Invoking(() => type.As<StructureType>()).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void Clone_WhenCalled_ReturnsDifferentInstance()
        {
            var type = new DINT();

            var clone = type.Clone();

            clone.Should().NotBeSameAs(type);
        }

        [Test]
        public void Clone_WhenCalled_ShouldHaveSameValue()
        {
            var type = new DINT(123);

            var clone = type.Clone();

            clone.Should().Be(123);
        }

        [Test]
        public void Index_ValidIndex_ShouldBeExpected()
        {
            var type = new DINT(1);

            var bit0 = type[0];
            var bit1 = type[1];

            bit0.Should().Be(true);
            bit1.Should().Be(false);
        }

        [Test]
        public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var type = new DINT(1);

            FluentActions.Invoking(() => type[32]).Should().Throw<ArgumentOutOfRangeException>();
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
        public Task Serialize_ValueAndRadix_ShouldBeValid()
        {
            var type = new DINT(123, Radix.Hex);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void ToBoolean_WhenCalled_ShouldBeExpectedValue()
        {
            var type = new DINT() as IConvertible;

            var result = type.ToBoolean(CultureInfo.InvariantCulture);

            result.Should().BeFalse();
        }

        [Test]
        public void ToByte_WhenCalled_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var expected = (byte)Convert.ChangeType(value, typeof(byte));
            var type = new DINT(value) as IConvertible;

            var result = type.ToByte(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToChar_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (char)Convert.ChangeType(_random, typeof(char));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToChar(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToDateTime_WhenCalled_ShouldThrowInvalidCastException()
        {
            var type = new DINT(_random) as IConvertible;

            FluentActions.Invoking(() => type.ToDateTime(CultureInfo.InvariantCulture)).Should()
                .Throw<InvalidCastException>();
        }

        [Test]
        public void ToDecimal_WhenCalled_ShouldThrowInvalidCastException()
        {
            var type = new DINT(_random) as IConvertible;

            FluentActions.Invoking(() => type.ToDecimal(CultureInfo.InvariantCulture)).Should()
                .Throw<InvalidCastException>();
        }

        [Test]
        public void ToDouble_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (double)Convert.ChangeType(_random, typeof(double));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToDouble(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToInt16_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (short)Convert.ChangeType(_random, typeof(short));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToInt16(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToInt32_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (int)Convert.ChangeType(_random, typeof(int));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToInt32(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToInt64_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (long)Convert.ChangeType(_random, typeof(long));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToInt64(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToSByte_WhenCalled_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<sbyte>();
            var expected = (sbyte)Convert.ChangeType(value, typeof(sbyte));
            var type = new DINT(value) as IConvertible;

            var result = type.ToSByte(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToSingle_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (float)Convert.ChangeType(_random, typeof(float));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToSingle(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = _random.ToString();
            var type = new DINT(_random) as IConvertible;

            var result = type.ToString(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToUInt16_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (ushort)Convert.ChangeType(_random, typeof(ushort));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToUInt16(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToUInt32_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (uint)Convert.ChangeType(_random, typeof(uint));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToUInt32(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToUInt64_WhenCalled_ShouldBeExpectedValue()
        {
            var expected = (ulong)Convert.ChangeType(_random, typeof(ulong));
            var type = new DINT(_random) as IConvertible;

            var result = type.ToUInt64(CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Boolean_ShouldBeExpectedValue()
        {
            var type = new DINT(1) as IConvertible;

            var result = (bool)type.ToType(typeof(bool), CultureInfo.InvariantCulture);

            result.Should().BeTrue();
        }

        [Test]
        public void ToType_Byte_ShouldBeExpectedValue()
        {
            const byte expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (byte)type.ToType(typeof(byte), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Char_ShouldBeExpectedValue()
        {
            const char expected = (char)1;
            var type = new DINT(1) as IConvertible;

            var result = (char)type.ToType(typeof(char), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_DateTime_ShouldThrowInvalidCastException()
        {
            var type = new DINT(1) as IConvertible;

            FluentActions.Invoking(() => type.ToType(typeof(DateTime), CultureInfo.InvariantCulture))
                .Should().Throw<InvalidCastException>();
        }

        [Test]
        public void ToType_Decimal_ShouldThrowInvalidCastException()
        {
            var type = new DINT(1) as IConvertible;

            FluentActions.Invoking(() => type.ToType(typeof(decimal), CultureInfo.InvariantCulture))
                .Should().Throw<InvalidCastException>();
        }

        [Test]
        public void ToType_Double_ShouldBeExpectedValue()
        {
            const double expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (double)type.ToType(typeof(double), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Int16_ShouldBeExpectedValue()
        {
            const short expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (short)type.ToType(typeof(short), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Int32_ShouldBeExpectedValue()
        {
            const int expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (int)type.ToType(typeof(int), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Int64_ShouldBeExpectedValue()
        {
            const long expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (long)type.ToType(typeof(long), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Sbyte_ShouldBeExpectedValue()
        {
            const sbyte expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (sbyte)type.ToType(typeof(sbyte), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_Float_ShouldBeExpectedValue()
        {
            const float expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (float)type.ToType(typeof(float), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_String_ShouldBeExpectedValue()
        {
            const string expected = "1";
            var type = new DINT(1) as IConvertible;

            var result = (string)type.ToType(typeof(string), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_UInt16_ShouldBeExpectedValue()
        {
            const ushort expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (ushort)type.ToType(typeof(ushort), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_UInt32_ShouldBeExpectedValue()
        {
            const uint expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (uint)type.ToType(typeof(uint), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_DbNull_ShouldBeExpectedValue()
        {
            var type = new DINT(1) as IConvertible;

            FluentActions.Invoking(() => type.ToType(typeof(DBNull), CultureInfo.InvariantCulture)).Should()
                .Throw<InvalidCastException>();
        }

        [Test]
        public void ToType_Null_ShouldBeExpectedValue()
        {
            var type = new DINT(1) as IConvertible;

            FluentActions.Invoking(() => type.ToType(null!, CultureInfo.InvariantCulture)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void ToType_Invalid_ShouldBeExpectedValue()
        {
            var type = new DINT(1) as IConvertible;

            FluentActions.Invoking(() => type.ToType(typeof(StructureType), CultureInfo.InvariantCulture)).Should()
                .Throw<InvalidCastException>();
        }

        [Test]
        public void ToType_UInt64_ShouldBeExpectedValue()
        {
            const ulong expected = 1;
            var type = new DINT(1) as IConvertible;

            var result = (ulong)type.ToType(typeof(ulong), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_BOOL_ShouldBeExpectedValue()
        {
            var type = new DINT(1) as IConvertible;

            var result = (BOOL)type.ToType(typeof(BOOL), CultureInfo.InvariantCulture);

            result.Should().Be(true);
        }

        [Test]
        public void ToType_SINT_ShouldBeExpectedValue()
        {
            var expected = new SINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (SINT)type.ToType(typeof(SINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_INT_ShouldBeExpectedValue()
        {
            var expected = new DINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (INT)type.ToType(typeof(INT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_DINT_ShouldBeExpectedValue()
        {
            var expected = new DINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (DINT)type.ToType(typeof(DINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_LINT_ShouldBeExpectedValue()
        {
            var expected = new LINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (LINT)type.ToType(typeof(LINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_REAL_ShouldBeExpectedValue()
        {
            var expected = new REAL(1);
            var type = new DINT(1) as IConvertible;

            var result = (REAL)type.ToType(typeof(REAL), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_USINT_ShouldBeExpectedValue()
        {
            var expected = new USINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (USINT)type.ToType(typeof(USINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_UINT_ShouldBeExpectedValue()
        {
            var expected = new UINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (UINT)type.ToType(typeof(UINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_UDINT_ShouldBeExpectedValue()
        {
            var expected = new UDINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (UDINT)type.ToType(typeof(UDINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_ULINT_ShouldBeExpectedValue()
        {
            var expected = new ULINT(1);
            var type = new DINT(1) as IConvertible;

            var result = (ULINT)type.ToType(typeof(ULINT), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void ToType_LREAL_ShouldBeExpectedValue()
        {
            var expected = new LREAL(1);
            var type = new DINT(1) as IConvertible;

            var result = (LREAL)type.ToType(typeof(LREAL), CultureInfo.InvariantCulture);

            result.Should().Be(expected);
        }

        [Test]
        public void Conversion_FromInt_ShouldBeExpectedValue()
        {
            DINT type = _random;

            type.Should().Be(_random);
        }

        [Test]
        public void Conversion_ToInt_ShouldBeExpectedValue()
        {
            var type = new DINT();

            int value = type;

            value.Should().Be(0);
        }

        [Test]
        public void Conversion_FromString_ShouldBeExpectedValue()
        {
            DINT value = "1";

            value.Should().Be(1);
        }

        [Test]
        public void Conversion_ToString_ShouldBeExpectedValue()
        {
            var type = new DINT(1);

            string value = type;

            value.Should().Be("1");
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
        [TestCase("123")]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0111_1011")]
        [TestCase("8#000_000_000_173")]
        [TestCase("16#0000_007b")]
        public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
        {
            var type = DINT.Parse(value);

            type.Should().Be(123);
        }
        
        [Test]
        [TestCase("123")]
        [TestCase("2#0000_0000_0000_0000_0000_0000_0111_1011")]
        [TestCase("8#000_000_000_173")]
        [TestCase("16#0000_007b")]
        public void TryParse_ValidFormat_ShouldBeExpectedValue(string value)
        {
            var atomic = DINT.TryParse(value);

            atomic.Should().NotBeNull();
            atomic.Should().Be(123);
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new DINT();
            var second = new DINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_AreNotEqual_ShouldBeFalse()
        {
            var first = new DINT(1);
            var second = new DINT(2);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new DINT();

            // ReSharper disable once EqualExpressionComparison this is the test.
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new DINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
        {
            var first = new DINT(1);
            var second = new INT(1);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
        {
            var first = new DINT(1);
            var second = new INT(2);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_InvalidType_ShouldBeFalse()
        {
            var first = new DINT(0);

            // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
            var result = first.Equals(new TIMER());

            result.Should().BeFalse();
        }

        [Test]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(1000000)]
        public void Equals_OverLargeCollection_ShouldWorkFairlyFast(int capacity)
        {
            var stopwatch = new Stopwatch();

            var range = Enumerable.Range(0, capacity).Select(_ => new DINT(123)).ToList();

            stopwatch.Start();
            var result = range.Where(v => v == new DINT(123)).ToList();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            result.Count.Should().Be(capacity);
        }
        
        [Test]
        public void IsEquivalent_AreEqual_ShouldBeTrue()
        {
            var first = new DINT(1);
            var second = new DINT(1);

            var result = first.IsEquivalent(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsEquivalent_AreNotEqual_ShouldBeFalse()
        {
            var first = new DINT(1);
            var second = new DINT(0);

            var result = first.IsEquivalent(second);

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_RandomValue_ShouldBeHashOfValue()
        {
            var type = new DINT(_random);

            var hash = type.GetHashCode();

            hash.Should().Be(_random.GetHashCode());
        }

        [Test]
        public void GetTypeCode_WhenCalled_ShouldBeObjectType()
        {
            var type = new DINT() as IConvertible;

            var code = type.GetTypeCode();

            code.Should().Be(TypeCode.Object);
        }

        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new DINT();
            var second = new DINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new DINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }

        [Test]
        public void CompareTo_ValueTypeEqual_ShouldBeZero()
        {
            var type = new DINT();

            var compare = type.CompareTo(0);

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
        public void CompareTo_AGreater_ShouldBeOne()
        {
            var a = new DINT(2);
            var b = new DINT(1);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_BGreater_ShouldBeNegativeOne()
        {
            var a = new DINT(1);
            var b = new DINT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithEqualValue_ShouldBeZero()
        {
            var a = new DINT(1);
            var b = new INT(1);

            var result = a.CompareTo(b);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithLesserValue_ShouldBeOne()
        {
            var a = new DINT(1);
            var b = new INT(0);

            var result = a.CompareTo(b);

            result.Should().Be(1);
        }

        [Test]
        public void CompareTo_DifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
        {
            var a = new DINT(1);
            var b = new INT(2);

            var result = a.CompareTo(b);

            result.Should().Be(-1);
        }

        [Test]
        public void CompareTo_InvalidType_ShouldThrowArgumentException()
        {
            var a = new DINT();
            var b = new TIMER();

            FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
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
        public void OperatorEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new DINT(1);
            var second = new DINT(2);

            var result = first == second;

            result.Should().BeFalse();
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
        public void OperatorNotEquals_AreNotEqual_ShouldBeTrue()
        {
            var first = new DINT(1);
            var second = new DINT(2);

            var result = first != second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThan_AGreaterThanB_ShouldBeTrue()
        {
            var a = new DINT(2);
            var b = new DINT(1);

            var result = a > b;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThan_ALessThanB_ShouldBeFalse()
        {
            var a = new DINT(1);
            var b = new DINT(2);

            var result = a > b;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_AGreaterThanB_ShouldBeFalse()
        {
            var a = new DINT(2);
            var b = new DINT(1);

            var result = a < b;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThan_ALessThanB_ShouldBeTrue()
        {
            var a = new DINT(1);
            var b = new DINT(2);

            var result = a < b;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_AGreaterThanB_ShouldBeTrue()
        {
            var a = new DINT(2);
            var b = new DINT(1);

            var result = a >= b;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorGreaterThanOrEqualTo_ALessThanB_ShouldBeFalse()
        {
            var a = new DINT(1);
            var b = new DINT(2);

            var result = a >= b;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_AGreaterThanB_ShouldBeFalse()
        {
            var a = new DINT(2);
            var b = new DINT(1);

            var result = a <= b;

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorLessThanOrEqualTo_ALessThanB_ShouldBeTrue()
        {
            var a = new DINT(1);
            var b = new DINT(2);

            var result = a <= b;

            result.Should().BeTrue();
        }
    }
}