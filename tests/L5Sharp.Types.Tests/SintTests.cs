using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class SintTests
    {
        [Test]
        public void New_Sint_ShouldNotBeNull()
        {
            var type = new Sint();

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Predefined_Sint_ShouldNotBeNull()
        {
            var type = new Sint();

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeZero()
        {
            var type = new Sint();

            type.GetValue().Should().Be(0);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("SINT");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = new Sint();

            type.SetValue(value.ToString());

            type.GetValue().Should().Be(value);
        }


        [Test]
        public void ParseValue_InvalidValue_ShouldBeNull()
        {
            var type = new Sint();

            type.SetValue("Invalid");

            type.GetValue().Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Sint();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = new Sint();

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
    }
}