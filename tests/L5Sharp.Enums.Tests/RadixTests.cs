using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Default_UserDefined_ShouldBeNullRadix()
        {
            var userDefined = new UserDefined("Test", "This is a test");

            var radix = Radix.Default(userDefined);

            radix.Should().Be(Radix.Null);
        }
        
        [Test]
        public void Default_Predefined_ShouldBeNullRadix()
        {
            var type = new String();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Default_Real_ShouldBeDecimalRadix()
        {
            var type = new Real();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Default_Bool_ShouldBeDecimalRadix()
        {
            var type = new Bool();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Sint_ShouldBeDecimalRadix()
        {
            var type = new Sint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Int_ShouldBeDecimalRadix()
        {
            var type = new Int();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Dint_ShouldBeDecimalRadix()
        {
            var type = new Dint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Lint_ShouldBeDecimalRadix()
        {
            var type = new Lint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_BoolArray_ShouldBeDecimal()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Bool());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Default_SintArray_ShouldBeDecimal()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Sint());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Default_IntArray_ShouldBeDecimal()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Int());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Default_DintArray_ShouldBeDecimal()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Dint());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Default_LintArray_ShouldBeDecimal()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Lint());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Default_RealArray_ShouldBeFloat()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Real());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Float);
        }
        
        [Test]
        public void Default_StringArray_ShouldBeNull()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new String());

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void ParseValue_InvalidString_ShouldThrowFormatException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Radix.ParseValue(fixture.Create<string>())).Should().Throw<FormatException>();
        }

        [Test]
        public void ParseValue_ValidBinary_ShouldBeExpectedValue()
        {
            const string value = "2#0000_0101";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidOctal_ShouldBeExpectedValue()
        {
            const string value = "8#005";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidDecimal_ShouldBeExpectedValue()
        {
            const string value = "5";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5);
        }
        
        [Test]
        public void ParseValue_ValidHex_ShouldBeExpectedValue()
        {
            const string value = "16#05";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5);
        }
        
        [Test]
        public void ParseValue_ValidAscii_ShouldBeExpectedValue()
        {
            const string value = "$05";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidFloat_ShouldBeExpected()
        {
            const string value = "5.0";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5.0f);
        }
        
        [Test]
        public void ParseValue_ValidExponential_ShouldBeExpected()
        {
            const string value = "5.00000000e+000";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(5.0f);
        }

        [Test]
        public void ParseValue_ValidDateTime_ShouldBeExpected()
        {
            const string value = "DT#2022-01-01-00:00:00.000_000(UTC-06:00)";

            var parsed = Radix.ParseValue(value);

            parsed.Value.Should().Be(1641016800000000);
        }
        
        [Test]
        public void TryParseValue_InvalidString_ShouldBeFalseAndNull()
        {
            var fixture = new Fixture();

            var parsed = Radix.TryParseValue(fixture.Create<string>(), out var atomicType);

            parsed.Should().BeFalse();
            atomicType.Should().BeNull();
        }

        [Test]
        public void TryParseValue_ValidBinary_ShouldBeExpectedValue()
        {
            const string value = "2#0000_0101";

            var parsed = Radix.TryParseValue(value, out var atomicType);

            parsed.Should().BeTrue();
            atomicType?.Value.Should().Be(5);
        }
        
        [Test]
        public void TryParseValue_ValidHexInvalidLength_ShouldBeExpectedValue()
        {
            const string value = "16#0000_0101_0000_0000_0000_0000_0000";

            var parsed = Radix.TryParseValue(value, out var atomicType);

            parsed.Should().BeFalse();
            atomicType.Should().BeNull();
        }

        [Test]
        public void SupportsType_Null_ShouldBeFalse()
        {
            var result = Radix.Decimal.SupportsType(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsType_NonAtomic_ShouldBeFalse()
        {
            var type = new String();
            
            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
        }
        
        [Test]
        public void SupportsType_AtomicAsIDataType_ShouldBeTrue()
        {
            var type = (IDataType)new Bool();
            
            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsType_ComplexAsIDataType_ShouldBeFalse()
        {
            var type = (IDataType)new String();

            var result = Radix.Decimal.SupportsType(type);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsType_BoolArrayAsIDataType_ShouldBeTrue()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new Bool());

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeTrue();
        }
        
        [Test]
        public void SupportsType_StringArrayAsIDataType_ShouldBeFalse()
        {
            var type = new ArrayType<IDataType>(new Dimensions(5), new String());

            var supported = Radix.Decimal.SupportsType(type);

            supported.Should().BeFalse();
        }
    }
}