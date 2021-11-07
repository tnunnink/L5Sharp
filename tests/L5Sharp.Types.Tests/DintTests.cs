using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class DintTests
    {
        [Test]
        public void New_Dint_ShouldNotBeNull()
        {
            var type = new Dint();

            type.Should().NotBeNull();
            type.Name.Should().Be("DINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Predefined_Dint_ShouldNotBeNull()
        {
            var type = new Dint();

            type.Should().NotBeNull();
            type.Name.Should().Be("DINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
            type.GetValue().Should().Be(0);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("DINT");

            type.Should().NotBeNull();
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var type = new Dint();

            type.SetValue(value.ToString());

            type.GetValue().Should().Be(value);
        }

        [Test]
        public void SetValue_InvalidValue_ShouldBeNull()
        {
            var type = new Dint();

            type.SetValue("Invalid");

            type.GetValue().Should().Be(0);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Dint();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = new Dint();

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
    }
}