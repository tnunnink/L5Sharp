using System;
using System.ComponentModel;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5SharpTests;
using L5SharpTests.Specimens;
using NUnit.Framework;

namespace L5Sharp.Converters.Tests
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
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<bool>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_byte_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<byte>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_short_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<short>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<int>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_long_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<long>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Sint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Sint>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Int>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Dint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Dint>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_Lint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Lint>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertFrom_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<string>();

            var result = converter.CanConvertFrom(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void ConvertFrom_NotSupported_ShouldThrowNotSupportedException()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<float>();

            FluentActions.Invoking(() => (Bool)converter.ConvertFrom(value)).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertFrom_bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<bool>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_byte_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<byte>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_short_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<short>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<int>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_long_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<long>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Sint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Sint>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Int_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Int>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Dint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Dint>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_Lint_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Lint>();

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void ConvertFrom_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            const string value = "1";

            var result = (Bool)converter.ConvertFrom(value);

            result.Should().Be(result);
        }

        [Test]
        public void CanConvertTo_bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<bool>();

            var result = converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_Bool_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = converter.CanConvertTo(value.GetType());

            result.Should().BeTrue();
        }

        [Test]
        public void CanConvertTo_string_ShouldBeTrue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
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

            FluentActions.Invoking(() => converter.ConvertTo(new Bool(), typeof(short))!).Should()
                .Throw<NotSupportedException>();
        }

        [Test]
        public void ConvertTo_bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = (bool)converter.ConvertTo(value, typeof(bool))!;

            result.Should().Be(value);
        }

        [Test]
        public void ConvertTo_Bool_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = (Bool)converter.ConvertTo(value, typeof(Bool))!;

            result.Should().Be(value);
        }

        [Test]
        public void ConvertTo_string_ShouldBeExpected()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Bool));
            var value = _fixture.Create<Bool>();

            var result = (string)converter.ConvertTo(value, typeof(string))!;

            result.Should().Be(value.Format(Radix.Decimal));
        }
    }
}