using AutoFixture;
using FluentAssertions;
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
            var type = new Bool();

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
            type.GetValue().Should().Be(false);
        }

        [Test]
        public void ParseType_BoolLower_ShouldReturnExpectedType()
        {
            var type = Logix.DataType.Create("bool");

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("BOOL");

            type.Should().NotBeNull();
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = new Bool();

            type.SetValue(value.ToString());

            type.GetValue().Should().Be(value);
        }

        [Test]
        public void ParseValue_ValidValueBoolOne_ShouldBeTrue()
        {
            var type = new Bool();

            type.SetValue("1");

            type.GetValue().Should().BeTrue();
        }

        [Test]
        public void ParseValue_ValidValueBoolYes_ShouldBeTrue()
        {
            var type = new Bool();
            
            type.SetValue("Yes");

            type.GetValue().Should().BeTrue();
        }

        [Test]
        public void ParseValue_ValidValueBoolZero_ShouldBeFalse()
        {
            var type = new Bool();
            
            type.SetValue("0");

            type.GetValue().Should().BeFalse();
        }

        [Test]
        public void ParseValue_ValidValueBoolNo_ShouldBeFalse()
        {
            var type = new Bool();
            
            type.SetValue("No");

            type.GetValue().Should().BeFalse();
        }

        [Test]
        public void ParseValue_ValidValueBoolNull_ShouldBeFalse()
        {
            var type = new Bool();
            
            type.SetValue("Invalid");

            type.GetValue().Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Bool();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_Float_ShouldBeFalse()
        {
            var type = new Bool();

            var value = type.SupportsRadix(Radix.Float);

            value.Should().BeFalse();
        }
    }
}