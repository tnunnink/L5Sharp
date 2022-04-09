using System;
using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Converters;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5SharpTests.Specimens;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Converters
{
    [TestFixture]
    public class BoolConverterTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new BoolGenerator());
            _fixture.Customizations.Add(new SintGenerator());
            _fixture.Customizations.Add(new IntGenerator());
            _fixture.Customizations.Add(new DintGenerator());
            _fixture.Customizations.Add(new LintGenerator());
        }

        [Test]
        public void CanConvertFrom_bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<bool>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_byte_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<byte>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_short_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<short>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<int>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_long_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<long>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Sint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<SINT>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<INT>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Dint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<DINT>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Lint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<LINT>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<string>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertFrom_NotSupported_ShouldThrowNotSupportedException()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<float>();

            FluentActions.Invoking(() => (BOOL)converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<bool>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_byte_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<byte>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_short_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<short>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<int>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_long_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<long>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Sint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<SINT>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<INT>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Dint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<DINT>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Lint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<LINT>();

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            const string value = "1";

            var result = (BOOL)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void CanConvertTo_bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<bool>();

            var result = converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<string>();

            var result = converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertTo_Null_ShouldThrowInvalidOperationException()
        {
            var converter = new BoolConverter();

            FluentActions.Invoking(() => converter.ConvertTo(null!, typeof(bool))!).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void ConvertTo_NotSupported_ShouldThrowNotSupportedException()
        {
            var converter = new BoolConverter();

            FluentActions.Invoking(() => converter.ConvertTo(new BOOL(), typeof(short))!).Should()
                .Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = (bool)converter.ConvertTo(value, typeof(bool))!;

            result.Should().Be(value);
        }

        [Test]
        public void ConvertTo_Bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = (BOOL)converter.ConvertTo(value, typeof(BOOL))!;

            result.Should().Be(value);
        }

        [Test]
        public void ConvertTo_string_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(BOOL));
            var value = _fixture.Create<BOOL>();

            var result = (string)converter.ConvertTo(value, typeof(string))!;

            result.Should().Be(value.Format(Radix.Decimal));
        }
    }
}