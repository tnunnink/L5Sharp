using System.Diagnostics;
using System.Globalization;
using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data.Atomics;

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
        var atomic = new DINT();

        atomic.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var atomic = new DINT();

        atomic.Name.Should().Be(nameof(DINT).ToUpper());
        atomic.Members.Should().BeEmpty();
        atomic.Value.Should().Be(0);
        atomic.Radix.Should().Be(Radix.Decimal);
    }

    [Test]
    public void New_TypedValue_ShouldHaveExpectedValues()
    {
        var atomic = new DINT(_random);

        atomic.Should().Be(_random);
    }

    [Test]
    public void Update_Null_ShouldThrowException()
    {
        var atomic = new DINT();

        FluentActions.Invoking(() => atomic.Update(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Update_InvalidType_ShouldThrowException()
    {
        var atomic = new DINT();

        FluentActions.Invoking(() => atomic.Update(new TIMER())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Update_MatchingValueType_ShouldBeUpdated()
    {
        var atomic = new DINT();

        atomic.Update(new DINT(123));

        atomic.Value.Should().Be(123);
    }

    [Test]
    public void Update_DifferentTypeValidSize_ShouldBeUpdated()
    {
        var atomic = new DINT();

        atomic.Update(new INT(short.MaxValue));

        atomic.Value.Should().Be(short.MaxValue);
    }

    [Test]
    public void Update_DifferentTypeTooLarge_ShouldCauseOverflow()
    {
        var atomic = new DINT();

        atomic.Update(new LINT(long.MaxValue));

        atomic.Value.Should().NotBe(int.MinValue);
    }

    [Test]
    public void GetBits_WhenCalled_ShouldHaveExpectedCount()
    {
        var atomic = new DINT(123);

        var bits = atomic.ToBitArray();

        bits.Count.Should().Be(32);
    }

    [Test]
    public void Bit_ValidIndex_ShouldBeExpectedValue()
    {
        var atomic = new DINT(123);

        var bit = atomic[1];

        bit.Should().Be(true);
    }

    [Test]
    public void Bit_InvalidIndex_ShouldBeThrowException()
    {
        var atomic = new DINT(123);

        FluentActions.Invoking(() => atomic[100]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void As_AtomicType_ShouldNotBeNull()
    {
        var atomic = new DINT();

        var casted = atomic.As<AtomicData>();

        casted.Should().NotBeNull();
    }

    [Test]
    public void As_InvalidType_ShouldThrowInvalidCastException()
    {
        var atomic = new DINT();

        FluentActions.Invoking(() => atomic.As<StructureData>()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Clone_WhenCalled_ReturnsDifferentInstance()
    {
        var atomic = new DINT();

        var clone = atomic.Clone();

        clone.Should().NotBeSameAs(atomic);
    }

    [Test]
    public void Clone_WhenCalled_ShouldHaveSameValue()
    {
        var atomic = new DINT(123);

        var clone = atomic.Clone();

        clone.Should().Be(123);
    }

    [Test]
    public void Index_ValidIndex_ShouldBeExpected()
    {
        var atomic = new DINT(1);

        var bit0 = atomic[0];
        var bit1 = atomic[1];

        bit0.Should().Be(true);
        bit1.Should().Be(false);
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var atomic = new DINT(1);

        FluentActions.Invoking(() => atomic[32]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void GetBytes_WhenCalled_ReturnsExpected()
    {
        var expected = BitConverter.GetBytes(_random);
        var atomic = new DINT(_random);

        var bytes = atomic.ToBytes();

        bytes.Should().BeEquivalentTo(expected);
    }

    [Test]
    public Task Serialize_Default_ShouldBeValid()
    {
        var atomic = new DINT();

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_Value_ShouldBeValid()
    {
        DINT atomic = 123;

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ValueAndRadix_ShouldBeValid()
    {
        DINT atomic = Radix.Hex.Format(123);

        var xml = atomic.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void ToBoolean_WhenCalled_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT();

        var result = atomic.ToBoolean(CultureInfo.InvariantCulture);

        result.Should().BeFalse();
    }

    [Test]
    public void ToByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<byte>();
        var expected = (byte)Convert.ChangeType(value, typeof(byte));
        IConvertible atomic = new DINT(value);

        var result = atomic.ToByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToChar_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (char)Convert.ChangeType(_random, typeof(char));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToChar(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToDateTime_WhenCalled_ShouldThrowInvalidCastException()
    {
        IConvertible atomic = new DINT(_random);

        FluentActions.Invoking(() => atomic.ToDateTime(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDecimal_WhenCalled_ShouldThrowInvalidCastException()
    {
        IConvertible atomic = new DINT(_random);

        FluentActions.Invoking(() => atomic.ToDecimal(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDouble_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (double)Convert.ChangeType(_random, typeof(double));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToDouble(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (short)Convert.ChangeType(_random, typeof(short));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (int)Convert.ChangeType(_random, typeof(int));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (long)Convert.ChangeType(_random, typeof(long));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<sbyte>();
        var expected = (sbyte)Convert.ChangeType(value, typeof(sbyte));
        var atomic = new DINT(value) as IConvertible;

        var result = atomic.ToSByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSingle_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (float)Convert.ChangeType(_random, typeof(float));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToSingle(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = _random.ToString();
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToString(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ushort)Convert.ChangeType(_random, typeof(ushort));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToUInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (uint)Convert.ChangeType(_random, typeof(uint));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToUInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ulong)Convert.ChangeType(_random, typeof(ulong));
        IConvertible atomic = new DINT(_random);

        var result = atomic.ToUInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Boolean_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT(1);

        var result = (bool)atomic.ToType(typeof(bool), CultureInfo.InvariantCulture);

        result.Should().BeTrue();
    }

    [Test]
    public void ToType_Byte_ShouldBeExpectedValue()
    {
        const byte expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (byte)atomic.ToType(typeof(byte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Char_ShouldBeExpectedValue()
    {
        const char expected = (char)1;
        IConvertible atomic = new DINT(1);

        var result = (char)atomic.ToType(typeof(char), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DateTime_ShouldThrowInvalidCastException()
    {
        IConvertible atomic = new DINT(1);

        FluentActions.Invoking(() => atomic.ToType(typeof(DateTime), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Decimal_ShouldThrowInvalidCastException()
    {
        IConvertible atomic = new DINT(1);

        FluentActions.Invoking(() => atomic.ToType(typeof(decimal), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Double_ShouldBeExpectedValue()
    {
        const double expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (double)atomic.ToType(typeof(double), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int16_ShouldBeExpectedValue()
    {
        const short expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (short)atomic.ToType(typeof(short), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int32_ShouldBeExpectedValue()
    {
        const int expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (int)atomic.ToType(typeof(int), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int64_ShouldBeExpectedValue()
    {
        const long expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (long)atomic.ToType(typeof(long), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Sbyte_ShouldBeExpectedValue()
    {
        const sbyte expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (sbyte)atomic.ToType(typeof(sbyte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Float_ShouldBeExpectedValue()
    {
        const float expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (float)atomic.ToType(typeof(float), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_String_ShouldBeExpectedValue()
    {
        const string expected = "1";
        IConvertible atomic = new DINT(1);

        var result = (string)atomic.ToType(typeof(string), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt16_ShouldBeExpectedValue()
    {
        const ushort expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (ushort)atomic.ToType(typeof(ushort), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt32_ShouldBeExpectedValue()
    {
        const uint expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (uint)atomic.ToType(typeof(uint), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DbNull_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT(1);

        FluentActions.Invoking(() => atomic.ToType(typeof(DBNull), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Null_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT(1);

        FluentActions.Invoking(() => atomic.ToType(null!, CultureInfo.InvariantCulture)).Should()
            .Throw<ArgumentNullException>();
    }

    [Test]
    public void ToType_Invalid_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT(1);

        FluentActions.Invoking(() => atomic.ToType(typeof(StructureData), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_UInt64_ShouldBeExpectedValue()
    {
        const ulong expected = 1;
        IConvertible atomic = new DINT(1);

        var result = (ulong)atomic.ToType(typeof(ulong), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_BOOL_ShouldBeExpectedValue()
    {
        IConvertible atomic = new DINT(1);

        var result = (BOOL)atomic.ToType(typeof(BOOL), CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Test]
    public void ToType_SINT_ShouldBeExpectedValue()
    {
        var expected = new SINT(1);
        IConvertible atomic = new DINT(1);

        var result = (SINT)atomic.ToType(typeof(SINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_INT_ShouldBeExpectedValue()
    {
        var expected = new DINT(1);
        IConvertible atomic = new DINT(1);

        var result = (INT)atomic.ToType(typeof(INT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DINT_ShouldBeExpectedValue()
    {
        var expected = new DINT(1);
        IConvertible atomic = new DINT(1);

        var result = (DINT)atomic.ToType(typeof(DINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LINT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        IConvertible atomic = new DINT(1);

        var result = (LINT)atomic.ToType(typeof(LINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_REAL_ShouldBeExpectedValue()
    {
        var expected = new REAL(1);
        IConvertible atomic = new DINT(1);

        var result = (REAL)atomic.ToType(typeof(REAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_USINT_ShouldBeExpectedValue()
    {
        var expected = new USINT(1);
        IConvertible atomic = new DINT(1);

        var result = (USINT)atomic.ToType(typeof(USINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UINT_ShouldBeExpectedValue()
    {
        var expected = new UINT(1);
        IConvertible atomic = new DINT(1);

        var result = (UINT)atomic.ToType(typeof(UINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UDINT_ShouldBeExpectedValue()
    {
        var expected = new UDINT(1);
        IConvertible atomic = new DINT(1);

        var result = (UDINT)atomic.ToType(typeof(UDINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_ULINT_ShouldBeExpectedValue()
    {
        var expected = new ULINT(1);
        IConvertible atomic = new DINT(1);

        var result = (ULINT)atomic.ToType(typeof(ULINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LREAL_ShouldBeExpectedValue()
    {
        var expected = new LREAL(1);
        IConvertible atomic = new DINT(1);

        var result = (LREAL)atomic.ToType(typeof(LREAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void Conversion_FromInt_ShouldBeExpectedValue()
    {
        DINT atomic = _random;

        atomic.Should().Be(_random);
    }

    [Test]
    public void Conversion_ToInt_ShouldBeExpectedValue()
    {
        var atomic = new DINT();

        int value = atomic;

        value.Should().Be(0);
    }

    [Test]
    public void Conversion_FromString_ShouldBeExpectedValue()
    {
        var value = (DINT)"1";

        value.Should().Be(1);
    }

    [Test]
    public void Conversion_ToString_ShouldBeExpectedValue()
    {
        var atomic = new DINT(1);

        var value = (string)atomic;

        value.Should().Be("1");
    }

    [Test]
    public void ToString_DefaultRadix_ShouldBeExpected()
    {
        var atomic = new DINT();

        var format = atomic.ToString();

        format.Should().Be("0");
    }

    [Test]
    public void ToString_OverloadedRadix_ShouldBeExpected()
    {
        var atomic = new DINT();

        var format = atomic.ToString(Radix.Binary);

        format.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000");
    }

    [Test]
    [TestCase("123")]
    [TestCase("2#0000_0000_0000_0000_0000_0000_0111_1011")]
    [TestCase("8#000_000_000_173")]
    [TestCase("16#0000_007b")]
    public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
    {
        var atomic = DINT.Parse(value);

        atomic.Should().Be(123);
    }

    [Test]
    [TestCase("123")]
    [TestCase("2#0000_0000_0000_0000_0000_0000_0111_1011")]
    [TestCase("8#000_000_000_173")]
    [TestCase("16#0000_007b")]
    public void TryParse_ValidFormat_ShouldBeExpectedValue(string value)
    {
        var result = DINT.TryParse(value, out var atomic);

        result.Should().BeTrue();
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
    public void EquivalentTo_AreEqual_ShouldBeTrue()
    {
        var first = new DINT(1);
        var second = new DINT(1);

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreNotEqual_ShouldBeFalse()
    {
        var first = new DINT(1);
        var second = new DINT(0);

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_RandomValue_ShouldBeHashOfValue()
    {
        var atomic = new DINT(_random);

        var hash = atomic.GetHashCode();

        hash.Should().Be(_random.GetHashCode());
    }

    [Test]
    public void GetTypeCode_WhenCalled_ShouldBeObjectType()
    {
        IConvertible atomic = new DINT();

        var code = atomic.GetTypeCode();

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
        var atomic = new DINT();

        var compare = atomic.CompareTo(atomic);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_ValueTypeEqual_ShouldBeZero()
    {
        var atomic = new DINT();

        var compare = atomic.CompareTo(0);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_Null_ShouldBeOne()
    {
        var atomic = new DINT();

        var compare = atomic.CompareTo(null);

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