using System;
using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Internal.Tests.Specimens;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Converters
{
    [TestFixture]
    public class USintConverterTests
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

            _converter = TypeDescriptor.GetConverter(typeof(USint));
        }
        
        [Test]
        public void CanConvertFrom_bool_ShouldBeFalse()
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

            result.Should().BeFalse();
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

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_ushort_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_int_ShouldBeFalse()
        {
            var value = _fixture.Create<int>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_uint_ShouldBeFalse()
        {
            var value = _fixture.Create<uint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_long_ShouldBeFalse()
        {
            var value = _fixture.Create<long>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_ulong_ShouldBeFalse()
        {
            var value = _fixture.Create<ulong>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_Bool_ShouldBeFalse()
        {
            var value = _fixture.Create<Bool>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }

        [Test]
        public void CanConvertFrom_Sint_ShouldBeFalse()
        {
            var value = _fixture.Create<Sint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_USint_ShouldBeTure()
        {
            var value = _fixture.Create<USint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertFrom_Int_ShouldBeFalse()
        {
            var value = _fixture.Create<Int>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_UInt_ShouldBeFalse()
        {
            var value = _fixture.Create<UInt>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_Dint_ShouldBeFalse()
        {
            var value = _fixture.Create<Dint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_UDint_ShouldBeFalse()
        {
            var value = _fixture.Create<UDint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_Lint_ShouldBeFalse()
        {
            var value = _fixture.Create<Lint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_ULint_ShouldBeFalse()
        {
            var value = _fixture.Create<ULint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }
        
        [Test]
        public void CanConvertFrom_Real_ShouldBeFalse()
        {
            var value = _fixture.Create<Real>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeFalse();
        }

        [Test]
        public void CanConvertFrom_string_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertFrom_bool_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<bool>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_sbyte_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<sbyte>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_byte_ShouldBeExpected()
        {
            var value = _fixture.Create<byte>();

            var result = (USint)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
        
        [Test]
        public void ConvertFrom_short_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<short>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_ushort_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<ushort>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_int_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<int>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_uint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<uint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_long_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<long>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_ulong_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<ulong>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_float_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<float>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_Sint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<Sint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_USint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (USint)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Int_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<Int>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_UInt_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<UInt>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_Dint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<Dint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_UDint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<UDint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_Lint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<Lint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_ULint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<ULint>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_Real_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<Real>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertFrom_string_ShouldBeExpected()
        {
            var value = _fixture.Create<byte>().ToString();

            var result = (USint)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
        
        [Test]
        public void CanConvertTo_sbyte_ShouldBeFalse()
        {
            var value = _fixture.Create<sbyte>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeFalse();
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
        public void CanConvertTo_Sint_ShouldBeFalse()
        {
            var value = _fixture.Create<Sint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeFalse();
        }

        [Test]
        public void CanConvertTo_USint_ShouldBeTrue()
        {
            var value = _fixture.Create<USint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_Int_ShouldBeTrue()
        {
            var value = _fixture.Create<Int>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_UInt_ShouldBeTrue()
        {
            var value = _fixture.Create<UInt>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Dint_ShouldBeTrue()
        {
            var value = _fixture.Create<Dint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_UDint_ShouldBeTrue()
        {
            var value = _fixture.Create<UDint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_Lint_ShouldBeTrue()
        {
            var value = _fixture.Create<Lint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_ULint_ShouldBeTrue()
        {
            var value = _fixture.Create<ULint>();

            var result = _converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertTo_Real_ShouldBeTrue()
        {
            var value = _fixture.Create<Real>();

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
            FluentActions.Invoking(() => _converter.ConvertTo(null!, typeof(bool))!).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void ConvertTo_bool_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<USint>();
            
            FluentActions.Invoking(() => _converter.ConvertTo(value, typeof(bool))!)
                .Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertTo_sbyte_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<USint>();
            
            FluentActions.Invoking(() => _converter.ConvertTo(value, typeof(sbyte))!)
                .Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_byte_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (byte)_converter.ConvertTo(value, typeof(byte))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_short_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (short)_converter.ConvertTo(value, typeof(short))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_ushort_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (ushort)_converter.ConvertTo(value, typeof(ushort))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_int_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (int)_converter.ConvertTo(value, typeof(int))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_uint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (uint)_converter.ConvertTo(value, typeof(uint))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_long_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (long)_converter.ConvertTo(value, typeof(long))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_ulong_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (ulong)_converter.ConvertTo(value, typeof(ulong))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_float_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (float)_converter.ConvertTo(value, typeof(float))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Bool_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<USint>();
            
            FluentActions.Invoking(() => _converter.ConvertTo(value, typeof(Bool))!)
                .Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void ConvertTo_Sint_ShouldThrowNotSupportedException()
        {
            var value = _fixture.Create<USint>();
            
            FluentActions.Invoking(() => _converter.ConvertTo(value, typeof(Sint))!)
                .Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_USint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (USint)_converter.ConvertTo(value, typeof(USint));

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Int_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (Int)_converter.ConvertTo(value, typeof(Int));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_UInt_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (UInt)_converter.ConvertTo(value, typeof(UInt));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Dint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (Dint)_converter.ConvertTo(value, typeof(Dint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_UDint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (UDint)_converter.ConvertTo(value, typeof(UDint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Lint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (Lint)_converter.ConvertTo(value, typeof(Lint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_ULint_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (ULint)_converter.ConvertTo(value, typeof(ULint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Real_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (Real)_converter.ConvertTo(value, typeof(Real));

            result?.Value.Should().Be(value);
        }

        [Test]
        public void ConvertTo_string_ShouldBeExpected()
        {
            var value = _fixture.Create<USint>();

            var result = (string)_converter.ConvertTo(value, typeof(string));

            result.Should().Be(value.Format());
        }
    }
}