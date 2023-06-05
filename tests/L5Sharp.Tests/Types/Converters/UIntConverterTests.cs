using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Tests.Specimens;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Converters
{
    [TestFixture]
    public class UIntConverterTests
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

            _converter = TypeDescriptor.GetConverter(typeof(UINT));
        }

        [Test]
        public void ConvertFrom_NotSupportedType_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<DateTime>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_ValidSbyte_ShouldBeExpected()
        {
            const sbyte value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_MinValue_ShouldThrowOverflowException()
        {
            const sbyte value = sbyte.MinValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidByte_ShouldBeExpected()
        {
            const byte value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_ByteMaxValue_ShouldBeExpected()
        {
            const byte value = byte.MaxValue;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_ValidShort_ShouldBeExpected()
        {
            const short value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_ShortMaxValue_ShouldBeExpected()
        {
            const short value = short.MaxValue;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_ValidUShort_ShouldBeExpected()
        {
            const ushort value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_UShortMaxValue_ShouldBeExpected()
        {
            const ushort value = ushort.MaxValue;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_ValidInt_ShouldBeExpected()
        {
            const int value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_IntMaxValue_ShouldBeExpected()
        {
            const int value = int.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidUInt_ShouldBeExpected()
        {
            const uint value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_UintMaxValue_ShouldBeExpected()
        {
            const uint value = uint.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidLong_ShouldBeExpected()
        {
            const long value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_LongMaxValue_ShouldBeExpected()
        {
            const long value = long.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidUlong_ShouldBeExpected()
        {
            const ulong value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_ULongMaxValue_ShouldThrowOverflowException()
        {
            const ulong value = ulong.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidFloat_ShouldBeExpected()
        {
            const float value = 0;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
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

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)(sbyte)atomic);
        }

        [Test]
        public void ConvertFrom_ValidUSINT_ShouldBeExpected()
        {
            USINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
        }

        [Test]
        public void ConvertFrom_USINTMaxValue_ShouldBeExpected()
        {
            USINT value = USINT.MaxValue;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be((ushort)value);
        }

        [Test]
        public void ConvertFrom_ValidINT_ShouldBeExpected()
        {
            INT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)(short)atomic);
        }

        [Test]
        public void ConvertFrom_INTMaxValue_ShouldBeExpected()
        {
            INT atomic = INT.MaxValue;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)(short)atomic);
        }

        [Test]
        public void ConvertFrom_ValidUINT_ShouldBeExpected()
        {
            UINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be(atomic);
        }

        [Test]
        public void ConvertFrom_UINTMaxValue_ShouldBeExpected()
        {
            UINT value = UINT.MaxValue;

            var result = (UINT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }

        [Test]
        public void ConvertFrom_ValidDINT_ShouldBeExpected()
        {
            DINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
        }

        [Test]
        public void ConvertFrom_DINTMaxValue_ShouldBeExpected()
        {
            DINT atomic = DINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidUDINT_ShouldBeExpected()
        {
            UDINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
        }

        [Test]
        public void ConvertFrom_UDINTMaxValue_ShouldBeExpected()
        {
            UDINT atomic = UDINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidLINT_ShouldBeExpected()
        {
            LINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
        }

        [Test]
        public void ConvertFrom_LINTMaxValue_ShouldThrowOverflowException()
        {
            LINT atomic = LINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidULINT_ShouldBeExpected()
        {
            ULINT atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
        }

        [Test]
        public void ConvertFrom_ULINTMaxValue_ShouldThrowOverflowException()
        {
            ULINT atomic = ULINT.MaxValue;

            FluentActions.Invoking(() => _converter.ConvertFrom(atomic)).Should().Throw<OverflowException>();
        }

        [Test]
        public void ConvertFrom_ValidREAL_ShouldBeExpected()
        {
            REAL atomic = 0;

            var result = (UINT)_converter.ConvertFrom(atomic);

            result?.Should().Be((ushort)atomic);
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
            var value = _fixture.Create<ushort>().ToString();

            var result = (UINT)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
    }
}