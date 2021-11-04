using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class BoolTests
    {
        [Test]
        public void New_Bool_ShouldNotBeNull()
        {
            var type = new Bool();

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Predefined_Bool_ShouldNotBeNull()
        {
            var type = Logix.DataType.Bool;

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeFalse()
        {
            var type = new Bool();

            type.DefaultValue.Should().Be(false);
        }
        
        [Test]
        public void DefaultRadix_WhenCalled_ShouldBeFalse()
        {
            var type = new Bool();

            type.DefaultRadix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void ParseType_BoolLower_ShouldReturnExpectedType()
        {
            var type = Logix.DataType.Parse("bool");

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("BOOL");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = Logix.DataType.Bool;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }
        
        [Test]
        public void ParseValue_ValidValueBoolOne_ShouldBeTrue()
        {
            var type = Logix.DataType.Bool;

            var result = type.ParseValue("1");

            result.Should().Be(true);
        }
        
        [Test]
        public void ParseValue_ValidValueBoolYes_ShouldBeTrue()
        {
            var type = Logix.DataType.Bool;

            var result = type.ParseValue("Yes");

            result.Should().Be(true);
        }

        [Test]
        public void ParseValue_ValidValueBoolZero_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var result = type.ParseValue("0");

            result.Should().Be(false);
        }
        
        [Test]
        public void ParseValue_ValidValueBoolNo_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var result = type.ParseValue("No");

            result.Should().Be(false);
        }
        
        [Test]
        public void ParseValue_ValidValueBoolNull_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var result = type.ParseValue("Invalid");

            result.Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = Logix.DataType.Bool;

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Float_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var value = type.SupportsRadix(Radix.Float);

            value.Should().BeFalse();
        }
        
        [Test]
        public void IsValidValue_BoolFalse_ShouldBeTrue()
        {
            var type = Logix.DataType.Bool;

            var value = type.IsValidValue(false);

            value.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_BoolFalseString_Should()
        {
            var type = Logix.DataType.Bool;

            var value = type.IsValidValue("True");

            value.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_Bool_Should()
        {
            var type = Logix.DataType.Bool;

            var value = type.IsValidValue(null);

            value.Should().BeFalse();
        }
    }
}