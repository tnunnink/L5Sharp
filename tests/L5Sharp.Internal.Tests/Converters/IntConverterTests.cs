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

            _converter = TypeDescriptor.GetConverter(typeof(Int));
        }
        
        [Test]
        public void CanConvertFrom_NonSupportedType_ShouldBeFalse()
        {
            var value = _fixture.Create<bool>();

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
        public void CanConvertFrom_short_ShouldBeTrue()
        {
            var value = _fixture.Create<short>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Sint_ShouldBeTrue()
        {
            var value = _fixture.Create<Sint>();

            var result = _converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }
        
        [Test]
        public void CanConvertFrom_Int_ShouldBeTrue()
        {
            var value = _fixture.Create<Int>();

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
            var value = _fixture.Create<float>();

            FluentActions.Invoking(() => _converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_byte_ShouldBeExpected()
        {
            var value = _fixture.Create<byte>();

            var result = (Int)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
        
        [Test]
        public void ConvertFrom_short_ShouldBeExpected()
        {
            var value = _fixture.Create<short>();

            var result = (Int)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Sint_ShouldBeExpected()
        {
            var value = _fixture.Create<Sint>();

            var result = (Int)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }
        
        [Test]
        public void ConvertFrom_Int_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (Int)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_string_ShouldBeExpected()
        {
            var value = _fixture.Create<short>().ToString();

            var result = (Int)_converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void CanConvertTo_short_ShouldBeTrue()
        {
            var value = _fixture.Create<short>();

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
        public void CanConvertTo_long_ShouldBeTrue()
        {
            var value = _fixture.Create<long>();

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
        public void CanConvertTo_Int_ShouldBeTrue()
        {
            var value = _fixture.Create<Int>();

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
        public void CanConvertTo_Lint_ShouldBeTrue()
        {
            var value = _fixture.Create<Lint>();

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
            var converter = new IntConverter();

            FluentActions.Invoking(() => converter.ConvertTo(null!, typeof(bool))!).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void ConvertTo_NotSupported_ShouldThrowNotSupportedException()
        {
            var converter = new IntConverter();

            FluentActions.Invoking(() => converter.ConvertTo(new Int(), typeof(bool))!).Should()
                .Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_short_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (short)_converter.ConvertTo(value, typeof(short))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_int_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (int)_converter.ConvertTo(value, typeof(int))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_long_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (long)_converter.ConvertTo(value, typeof(long))!;

            result.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_float_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (float)_converter.ConvertTo(value, typeof(float))!;

            result.Should().Be(value);
        }

        [Test]
        public void ConvertTo_Int_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (Int)_converter.ConvertTo(value, typeof(Int));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Dint_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (Dint)_converter.ConvertTo(value, typeof(Dint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Lint_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (Lint)_converter.ConvertTo(value, typeof(Lint));

            result?.Value.Should().Be(value);
        }
        
        [Test]
        public void ConvertTo_Real_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (Real)_converter.ConvertTo(value, typeof(Real));

            result?.Value.Should().Be(value);
        }

        [Test]
        public void ConvertTo_string_ShouldBeExpected()
        {
            var value = _fixture.Create<Int>();

            var result = (string)_converter.ConvertTo(value, typeof(string));

            result.Should().Be(value.Format());
        }
    }
}