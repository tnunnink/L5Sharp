using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Tests.Specimens;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Converters
{
    [TestFixture]
    public class IntConverterTests
    {
        private Fixture _fixture;
        private TypeConverter _converter;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new BoolGenerator());
            _fixture.Customizations.Add(new SintGenerator());
            _fixture.Customizations.Add(new IntGenerator());
            _fixture.Customizations.Add(new DintGenerator());
            _fixture.Customizations.Add(new LintGenerator());

            _converter = TypeDescriptor.GetConverter(typeof(INT));
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

            var result = (INT)_converter.ConvertFrom(value);

            result.Should().Be(new INT(value));
        }

        [Test]
        public void ConvertFrom_ValidByte_ShouldBeExpected()
        {
            const byte value = 0;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_ByteMaxValue_ShouldBeExpected()
        {
            const byte value = byte.MaxValue;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_ValidShort_ShouldBeExpected()
        {
            const short value = 0;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_ShortMaxValue_ShouldBeExpected()
        {
            const short value = short.MaxValue;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_ValidUShort_ShouldBeExpected()
        {
            const ushort value = 0;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
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

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
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

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
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

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
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

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
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

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)atomic);
        }

        [Test]
        public void ConvertFrom_ValidUSINT_ShouldBeExpected()
        {
            USINT atomic = 0;

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)atomic);
        }

        [Test]
        public void ConvertFrom_USINTMaxValue_ShouldBeExpected()
        {
            USINT value = USINT.MaxValue;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be((short)value);
        }
        
        [Test]
        public void ConvertFrom_ValidINT_ShouldBeExpected()
        {
            INT atomic = 0;

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be(atomic);
        }

        [Test]
        public void ConvertFrom_INTMaxValue_ShouldBeExpected()
        {
            INT value = INT.MaxValue;

            var result = (INT)_converter.ConvertFrom(value);

            result?.Should().Be(value);
        }
        
        [Test]
        public void ConvertFrom_ValidUINT_ShouldBeExpected()
        {
            UINT atomic = 0;

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)(ushort)atomic);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)atomic);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)(uint)atomic);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)atomic);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)(ulong)atomic);
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

            var result = (INT)_converter.ConvertFrom(atomic);

            result?.Should().Be((short)atomic);
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
            var value = _fixture.Create<short>().ToString();

            var result = (INT)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
    }
}