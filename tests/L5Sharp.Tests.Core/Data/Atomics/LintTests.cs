using System.Diagnostics;
using System.Globalization;
using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data.Atomics;

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
        var atomic = new LINT();

        atomic.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var atomic = new LINT();

        atomic.Should().NotBeNull();
        atomic.Should().Be(0);
        atomic.Name.Should().Be(nameof(LINT).ToUpper());
        atomic.Members.Should().BeEmpty();
        atomic.Radix.Should().Be(Radix.Decimal);
    }

    [Test]
    public void New_Value_ShouldHaveExpectedValues()
    {
        var atomic = new LINT(_random);

        atomic.Should().Be(_random);
    }

    [Test]
    public void As_AtomicType_ShouldNotBeNull()
    {
        var atomic = new LINT();

        var casted = atomic.As<AtomicData>();

        casted.Should().NotBeNull();
    }

    [Test]
    public void As_InvalidType_ShouldThrowInvalidCastException()
    {
        var atomic = new LINT();

        FluentActions.Invoking(() => atomic.As<StructureData>()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Clone_WhenCalled_ReturnsDifferentInstance()
    {
        var atomic = new LINT();

        var clone = atomic.Clone();

        clone.Should().NotBeSameAs(atomic);
    }

    [Test]
    public void Clone_WhenCalled_ShouldHaveSameValue()
    {
        var atomic = new LINT(123);

        var clone = atomic.Clone();

        clone.Should().Be(123);
    }

    [Test]
    public void Index_ValidIndex_ShouldBeExpected()
    {
        var atomic = new LINT(1);

        var bit0 = atomic[0];
        var bit1 = atomic[1];

        bit0.Should().Be(true);
        bit1.Should().Be(false);
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var atomic = new LINT(1);

        FluentActions.Invoking(() => atomic[100]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public Task Serialize_Default_ShouldBeValid()
    {
        var atomic = new LINT();

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_Value_ShouldBeValid()
    {
        var atomic = new LINT(123);

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ValueAndRadix_ShouldBeValid()
    {
        LINT atomic = Radix.Hex.Format(123);

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void ToBoolean_WhenCalled_ShouldBeExpectedValue()
    {
        IConvertible atomic = new LINT();

        var result = atomic.ToBoolean(CultureInfo.InvariantCulture);

        result.Should().BeFalse();
    }

    [Test]
    public void ToByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<byte>();
        var expected = (byte)Convert.ChangeType(value, typeof(byte));
        var atomic = new LINT(value) as IConvertible;

        var result = atomic.ToByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }
    
    [Test]
    public void ToBytes_WhenCalled_ReturnsExpected()
    {
        var expected = BitConverter.GetBytes(_random);
        var atomic = new LINT(_random);

        var bytes = atomic.ToBytes();

        bytes.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ToChar_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (char)Convert.ChangeType(_random, typeof(char));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToChar(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToDateTime_WhenCalled_ShouldThrowInvalidCastException()
    {
        var expected = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var atomic = new LINT() as IConvertible;

        var result = atomic.ToDateTime(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToDecimal_WhenCalled_ShouldThrowInvalidCastException()
    {
        var atomic = new LINT(_random) as IConvertible;

        FluentActions.Invoking(() => atomic.ToDecimal(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDouble_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (double)Convert.ChangeType(_random, typeof(double));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToDouble(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (short)Convert.ChangeType(_random, typeof(short));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (int)Convert.ChangeType(_random, typeof(int));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (long)Convert.ChangeType(_random, typeof(long));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<sbyte>();
        var expected = (sbyte)Convert.ChangeType(value, typeof(sbyte));
        var atomic = new LINT(value) as IConvertible;

        var result = atomic.ToSByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSingle_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (float)Convert.ChangeType(_random, typeof(float));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToSingle(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = _random.ToString();
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToString(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ushort)Convert.ChangeType(_random, typeof(ushort));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToUInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (uint)Convert.ChangeType(_random, typeof(uint));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToUInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ulong)Convert.ChangeType(_random, typeof(ulong));
        var atomic = new LINT(_random) as IConvertible;

        var result = atomic.ToUInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Boolean_ShouldBeExpectedValue()
    {
        var atomic = new LINT(1) as IConvertible;

        var result = (bool)atomic.ToType(typeof(bool), CultureInfo.InvariantCulture);

        result.Should().BeTrue();
    }

    [Test]
    public void ToType_Byte_ShouldBeExpectedValue()
    {
        const byte expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (byte)atomic.ToType(typeof(byte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Char_ShouldBeExpectedValue()
    {
        const char expected = (char)1;
        var atomic = new LINT(1) as IConvertible;

        var result = (char)atomic.ToType(typeof(char), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DateTime_ShouldThrowInvalidCastException()
    {
        var expected = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var atomic = new LINT() as IConvertible;

        var result = atomic.ToType(typeof(DateTime), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Decimal_ShouldThrowInvalidCastException()
    {
        var atomic = new LINT(1) as IConvertible;

        FluentActions.Invoking(() => atomic.ToType(typeof(decimal), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Double_ShouldBeExpectedValue()
    {
        const double expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (double)atomic.ToType(typeof(double), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int16_ShouldBeExpectedValue()
    {
        const short expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (short)atomic.ToType(typeof(short), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int32_ShouldBeExpectedValue()
    {
        const int expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (int)atomic.ToType(typeof(int), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int64_ShouldBeExpectedValue()
    {
        const long expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (long)atomic.ToType(typeof(long), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Sbyte_ShouldBeExpectedValue()
    {
        const sbyte expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (sbyte)atomic.ToType(typeof(sbyte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Float_ShouldBeExpectedValue()
    {
        const float expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (float)atomic.ToType(typeof(float), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_String_ShouldBeExpectedValue()
    {
        const string expected = "1";
        var atomic = new LINT(1) as IConvertible;

        var result = (string)atomic.ToType(typeof(string), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt16_ShouldBeExpectedValue()
    {
        const ushort expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (ushort)atomic.ToType(typeof(ushort), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt32_ShouldBeExpectedValue()
    {
        const uint expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (uint)atomic.ToType(typeof(uint), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DbNull_ShouldBeExpectedValue()
    {
        var atomic = new LINT(1) as IConvertible;

        FluentActions.Invoking(() => atomic.ToType(typeof(DBNull), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Null_ShouldBeExpectedValue()
    {
        var atomic = new LINT(1) as IConvertible;

        FluentActions.Invoking(() => atomic.ToType(null!, CultureInfo.InvariantCulture)).Should()
            .Throw<ArgumentNullException>();
    }

    [Test]
    public void ToType_Invalid_ShouldBeExpectedValue()
    {
        var atomic = new LINT(1) as IConvertible;

        FluentActions.Invoking(() => atomic.ToType(typeof(StructureData), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_UInt64_ShouldBeExpectedValue()
    {
        const ulong expected = 1;
        var atomic = new LINT(1) as IConvertible;

        var result = (ulong)atomic.ToType(typeof(ulong), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_BOOL_ShouldBeExpectedValue()
    {
        var atomic = new LINT(1) as IConvertible;

        var result = (BOOL)atomic.ToType(typeof(BOOL), CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Test]
    public void ToType_SINT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (SINT)atomic.ToType(typeof(SINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_INT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (INT)atomic.ToType(typeof(INT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DINT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (DINT)atomic.ToType(typeof(DINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LINT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (LINT)atomic.ToType(typeof(LINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_REAL_ShouldBeExpectedValue()
    {
        var expected = new REAL(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (REAL)atomic.ToType(typeof(REAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_USINT_ShouldBeExpectedValue()
    {
        var expected = new USINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (USINT)atomic.ToType(typeof(USINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UINT_ShouldBeExpectedValue()
    {
        var expected = new UINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (UINT)atomic.ToType(typeof(UINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UDINT_ShouldBeExpectedValue()
    {
        var expected = new UDINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (UDINT)atomic.ToType(typeof(UDINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_ULINT_ShouldBeExpectedValue()
    {
        var expected = new ULINT(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (ULINT)atomic.ToType(typeof(ULINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LREAL_ShouldBeExpectedValue()
    {
        var expected = new LREAL(1);
        var atomic = new LINT(1) as IConvertible;

        var result = (LREAL)atomic.ToType(typeof(LREAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void Conversion_FromLong_ShouldBeExpectedValue()
    {
        LINT atomic = _random;

        atomic.Should().Be(_random);
    }

    [Test]
    public void Conversion_ToLong_ShouldBeExpectedValue()
    {
        var atomic = new LINT();

        long value = atomic;

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
        var atomic = new LINT(1);

        string value = atomic;

        value.Should().Be("1");
    }

    [Test]
    public void ToString_DefaultRadix_ShouldBeExpected()
    {
        var atomic = new LINT();

        var format = atomic.ToString();

        format.Should().Be("0");
    }

    [Test]
    public void ToString_OverloadedRadix_ShouldBeExpected()
    {
        var atomic = new LINT();

        var format = atomic.ToString(Radix.Binary);

        format.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000");
    }

    [Test]
    [TestCase("123")]
    [TestCase("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0111_1011")]
    [TestCase("8#00_000_000_000_000_173")]
    [TestCase("16#0000_0000_0000_007b")]
    public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
    {
        var atomic = LINT.Parse(value);

        atomic.Should().Be(123);
    }

    [Test]
    public void Equals_AreEqual_ShouldBeTrue()
    {
        var first = new LINT();
        var second = new LINT();

        var result = first.Equals(second);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_AreNotEqual_ShouldBeFalse()
    {
        var first = new LINT(1);
        var second = new LINT(2);

        var result = first.Equals(second);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_AreSame_ShouldBeTrue()
    {
        var first = new LINT();

        // ReSharper disable once EqualExpressionComparison this is the test.
        var result = first.Equals(first);

        result.Should().BeTrue();
    }


    [Test]
    public void Equals_Null_ShouldBeFalse()
    {
        var first = new LINT();

        var result = first.Equals(null);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
    {
        var first = new LINT(1);
        var second = new LINT(1);

        // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
        var result = first.Equals(second);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
    {
        var first = new LINT(1);
        var second = new LINT(2);

        // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
        var result = first.Equals(second);

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
    [TestCase(1000)]
    [TestCase(10000)]
    [TestCase(100000)]
    [TestCase(1000000)]
    public void Equals_OverLargeCollection_ShouldWorkFairlyFast(int capacity)
    {
        var stopwatch = new Stopwatch();

        var range = Enumerable.Range(0, capacity).Select(_ => new LINT(123)).ToList();

        stopwatch.Start();
        var result = range.Where(v => v == new LINT(123)).ToList();
        stopwatch.Stop();

        result.Count.Should().Be(capacity);
    }

    [Test]
    public void EquivalentTo_AreEqual_ShouldBeTrue()
    {
        var first = new LINT(1);
        var second = new LINT(1);

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreNotEqual_ShouldBeFalse()
    {
        var first = new LINT(1);
        var second = new LINT(0);

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_RandomValue_ShouldBeHashOfValue()
    {
        var atomic = new LINT(_random);

        var hash = atomic.GetHashCode();

        hash.Should().Be(_random.GetHashCode());
    }

    [Test]
    public void GetTypeCode_WhenCalled_ShouldBeObjectType()
    {
        var atomic = new LINT() as IConvertible;

        var code = atomic.GetTypeCode();

        code.Should().Be(TypeCode.Object);
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
        var atomic = new LINT();

        var compare = atomic.CompareTo(atomic);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_ValueTypeEqual_ShouldBeZero()
    {
        var atomic = new LINT();

        var compare = atomic.CompareTo(0);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_Null_ShouldBeOne()
    {
        var atomic = new LINT();

        var compare = atomic.CompareTo(null);

        compare.Should().Be(1);
    }

    [Test]
    public void CompareTo_AGreater_ShouldBeOne()
    {
        var a = new LINT(2);
        var b = new LINT(1);

        var result = a.CompareTo(b);

        result.Should().Be(1);
    }

    [Test]
    public void CompareTo_BGreater_ShouldBeNegativeOne()
    {
        var a = new LINT(1);
        var b = new LINT(2);

        var result = a.CompareTo(b);

        result.Should().Be(-1);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithEqualValue_ShouldBeZero()
    {
        var a = new LINT(1);
        var b = new LINT(1);

        var result = a.CompareTo(b);

        result.Should().Be(0);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithLesserValue_ShouldBeOne()
    {
        var a = new LINT(1);
        var b = new LINT(0);

        var result = a.CompareTo(b);

        result.Should().Be(1);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
    {
        var a = new LINT(1);
        var b = new LINT(2);

        var result = a.CompareTo(b);

        result.Should().Be(-1);
    }

    [Test]
    public void CompareTo_InvalidType_ShouldThrowArgumentException()
    {
        var a = new LINT();
        var b = new TIMER();

        FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
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