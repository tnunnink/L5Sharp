using System;
using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Converters;
using L5Sharp.Types;
using L5SharpTests.Specimens;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Converters
{
    [TestFixture]
    public class SintConverterTests
    {
        private Fixture _fixture;
        private TypeConverter _converter;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new BoolGenerator());
            _fixture.Customizations.Add(new SintGenerator());
            _fixture.Customizations.Add(new USintGenerator());
            _fixture.Customizations.Add(new IntGenerator());
            _fixture.Customizations.Add(new UIntGenerator());
            _fixture.Customizations.Add(new DintGenerator());
            _fixture.Customizations.Add(new UDintGenerator());
            _fixture.Customizations.Add(new LintGenerator());
            _fixture.Customizations.Add(new ULintGenerator());

            _converter = TypeDescriptor.GetConverter(typeof(SINT));
        }

        [Test]
        public void CanConvertFrom_NonSupportedType_ShouldBeFalse()
        {
            var value = _fixture.Create<bool>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }

        [Test]
        public void CanConvertFrom_sbyte_ShouldBeFalse()
        {
            var value = _fixture.Create<sbyte>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_byte_ShouldBeTrue()
        {
            var value = _fixture.Create<byte>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_short_ShouldBeFalse()
        {
            var value = _fixture.Create<short>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_ushort_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_int_ShouldBeFalse()
        {
            var value = _fixture.Create<int>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_uint_ShouldBeTrue()
        {
            var value = _fixture.Create<uint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_long_ShouldBeFalse()
        {
            var value = _fixture.Create<long>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_ulong_ShouldBeTrue()
        {
            var value = _fixture.Create<ulong>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Bool_ShouldBeFalse()
        {
            var value = _fixture.Create<BOOL>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }

        [Test]
        public void CanConvertFrom_Sint_ShouldBeFalse()
        {
            var value = _fixture.Create<SINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_USint_ShouldBeTure()
        {
            var value = _fixture.Create<USINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Int_ShouldBeFalse()
        {
            var value = _fixture.Create<INT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_UInt_ShouldBeTrue()
        {
            var value = _fixture.Create<UINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Dint_ShouldBeFalse()
        {
            var value = _fixture.Create<DINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_UDint_ShouldBeTrue()
        {
            var value = _fixture.Create<UDINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Lint_ShouldBeFalse()
        {
            var value = _fixture.Create<LINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_ULint_ShouldBeTrue()
        {
            var value = _fixture.Create<ULINT>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Real_ShouldBeFalse()
        {
            var value = _fixture.Create<REAL>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_string_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertFrom_NotSupportedType_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<DateTime>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_sbyte_ShouldBeExpected()
        {
            var value = _fixture.Create<sbyte>();

            var result = (SINT)_converter.ConvertFrom(value);

            result.Should().Be(new SINT(value));
        }

        [Test]
        public void ConvertFrom_ValidByte_ShouldBeExpected()
        {
            const byte value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }
        
        [Test]
        public void ConvertFrom_InvalidByte_ShouldThrowOverflowException()
        {
            const byte value = byte.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidShort_ShouldBeExpected()
        {
            const short value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }
        
        [Test]
        public void ConvertFrom_InvalidShort_ShouldThrowOverflowException()
        {
            const short value = short.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidUShort_ShouldBeExpected()
        {
            const ushort value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }
        
        [Test]
        public void ConvertFrom_InvalidUShort_ShouldThrowOverflowException()
        {
            const ushort value = ushort.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidInt_ShouldBeExpected()
        {
            const int value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_InvalidInt_ShouldThrowOverflowException()
        {
            const int value = int.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidUInt_ShouldBeExpected()
        {
            const uint value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }

        [Test]
        public void ConvertFrom_InvalidUInt_ShouldThrowOverflowException()
        {
            const uint value = uint.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidLong_ShouldBeExpected()
        {
            const long value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }

        [Test]
        public void ConvertFrom_InvalidLong_ShouldThrowOverflowException()
        {
            const long value = long.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidUlong_ShouldBeExpected()
        {
            const ulong value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }

        [Test]
        public void ConvertFrom_InvalidULong_ShouldThrowOverflowException()
        {
            const ulong value = ulong.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidFloat_ShouldBeExpected()
        {
            const float value = 0;

            var result = (SINT)_converter.ConvertFrom(value);

            result?.Value.Should().Be((sbyte)value);
        }

        [Test]
        public void ConvertFrom_InvalidFloat_ShouldThrowOverflowException()
        {
            const float value = float.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidSINT_ShouldBeExpected()
        {
            SINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be(atomic.Value);
        }

        [Test]
        public void ConvertFrom_ValidUSINT_ShouldBeExpected()
        {
            USINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidUSINT_ShouldThrowOverflowException()
        {
            USINT atomic = USINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidINT_ShouldBeExpected()
        {
            INT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidINT_ShouldThrowOverflowException()
        {
            INT atomic = INT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidUINT_ShouldBeExpected()
        {
            UINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidUINT_ShouldThrowOverflowException()
        {
            UINT atomic = UINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidDINT_ShouldBeExpected()
        {
            DINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidDINT_ShouldThrowOverflowException()
        {
            DINT atomic = DINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidUDINT_ShouldBeExpected()
        {
            UDINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidUDINT_ShouldThrowOverflowException()
        {
            UDINT atomic = UDINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidLINT_ShouldBeExpected()
        {
            LINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidLINT_ShouldThrowOverflowException()
        {
            LINT atomic = LINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidULINT_ShouldBeExpected()
        {
            ULINT atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidULINT_ShouldThrowOverflowException()
        {
            ULINT atomic = ULINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }
        
        [Test]
        public void ConvertFrom_ValidREAL_ShouldBeExpected()
        {
            REAL atomic = 0;

            var result = (SINT)_converter.ConvertFrom(atomic);

            result?.Value.Should().Be((sbyte)atomic.Value);
        }

        [Test]
        public void ConvertFrom_InvalidREAL_ShouldThrowOverflowException()
        {
            REAL atomic = REAL.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_string_ShouldBeExpected()
        {
            var value = _fixture.Create<sbyte>().ToString();

            var result = (SINT)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void CanConvertTo_sbyte_ShouldBeTrue()
        {
            var value = _fixture.Create<sbyte>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_byte_ShouldBeTrue()
        {
            var value = _fixture.Create<byte>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_short_ShouldBeTrue()
        {
            var value = _fixture.Create<short>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_ushort_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_int_ShouldBeTrue()
        {
            var value = _fixture.Create<int>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_uint_ShouldBeTrue()
        {
            var value = _fixture.Create<uint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_long_ShouldBeTrue()
        {
            var value = _fixture.Create<long>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_ulong_ShouldBeTrue()
        {
            var value = _fixture.Create<ulong>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_float_ShouldBeTrue()
        {
            var value = _fixture.Create<float>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Sint_ShouldBeTrue()
        {
            var value = _fixture.Create<SINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_USint_ShouldBeTrue()
        {
            var value = _fixture.Create<USINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Int_ShouldBeTrue()
        {
            var value = _fixture.Create<INT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_UInt_ShouldBeTrue()
        {
            var value = _fixture.Create<UINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Dint_ShouldBeTrue()
        {
            var value = _fixture.Create<DINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_UDint_ShouldBeTrue()
        {
            var value = _fixture.Create<UDINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Lint_ShouldBeTrue()
        {
            var value = _fixture.Create<LINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_ULint_ShouldBeTrue()
        {
            var value = _fixture.Create<ULINT>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Real_ShouldBeTrue()
        {
            var value = _fixture.Create<REAL>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_string_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertTo_Null_ShouldThrowInvalidOperationException()
        {
            var converter = new SintConverter();

            FluentActions.Invoking(() => converter.ConvertTo(null!, typeof(bool))!).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void ConvertTo_NotSupported_ShouldThrowNotSupportedException()
        {
            var converter = new SintConverter();

            FluentActions.Invoking(() => converter.ConvertTo(new SINT(), typeof(DateTime))!).Should()
                .Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_sbyte_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (sbyte)_converter.ConvertTo(atomic, typeof(sbyte))!;

            result.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_byte_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (byte)_converter.ConvertTo(atomic, typeof(byte))!;

            result.Should().Be((byte)atomic.Value);
        }
        
        [Test]
        public void ConvertTo_short_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (short)_converter.ConvertTo(atomic, typeof(short))!;

            result.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_ushort_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (ushort)_converter.ConvertTo(atomic, typeof(ushort))!;

            result.Should().Be((ushort)atomic.Value);
        }

        [Test]
        public void ConvertTo_int_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (int)_converter.ConvertTo(atomic, typeof(int))!;

            result.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_uint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (uint)_converter.ConvertTo(atomic, typeof(uint))!;

            result.Should().Be((uint)atomic.Value);
        }

        [Test]
        public void ConvertTo_long_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (long)_converter.ConvertTo(atomic, typeof(long))!;

            result.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_ulong_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (ulong)_converter.ConvertTo(atomic, typeof(ulong))!;

            result.Should().Be((ulong)atomic.Value);
        }

        [Test]
        public void ConvertTo_float_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (float)_converter.ConvertTo(atomic, typeof(float))!;

            result.Should().Be(atomic.Value);
        }

        [Test]
        public void ConvertTo_Sint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (SINT)_converter.ConvertTo(atomic, typeof(SINT));

            result.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_USint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (USINT)_converter.ConvertTo(atomic, typeof(USINT));

            result.Should().Be((byte)atomic.Value);
        }

        [Test]
        public void ConvertTo_Int_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (INT)_converter.ConvertTo(atomic, typeof(INT));

            result?.Value.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_UInt_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (UINT)_converter.ConvertTo(atomic, typeof(UINT));

            result.Should().Be((ushort)atomic.Value);
        }

        [Test]
        public void ConvertTo_Dint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (DINT)_converter.ConvertTo(atomic, typeof(DINT));

            result?.Value.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_UDint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (UDINT)_converter.ConvertTo(atomic, typeof(UDINT));

            result.Should().Be((uint)atomic.Value);
        }

        [Test]
        public void ConvertTo_Lint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (LINT)_converter.ConvertTo(atomic, typeof(LINT));

            result?.Value.Should().Be(atomic.Value);
        }
        
        [Test]
        public void ConvertTo_ULint_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (ULINT)_converter.ConvertTo(atomic, typeof(ULINT));

            result.Should().Be((ulong)atomic.Value);
        }

        [Test]
        public void ConvertTo_Real_ShouldBeExpected()
        {
            var atomic = _fixture.Create<SINT>();

            var result = (REAL)_converter.ConvertTo(atomic, typeof(REAL));

            result.Should().Be(atomic.Value);
        }

        [Test]
        public void ConvertTo_string_ShouldBeExpected()
        {
            var value = _fixture.Create<SINT>();

            var result = (string)_converter.ConvertTo(value, typeof(string));

            result.Should().Be(value.Format());
        }
    }
}