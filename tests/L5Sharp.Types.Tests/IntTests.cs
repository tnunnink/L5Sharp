using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class IntTests
    {
        [Test]
        public void New_Int_ShouldNotBeNull()
        {
            var type = new Int();

            type.Should().NotBeNull();
            type.Name.Should().Be("INT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Predefined_Int_ShouldNotBeNull()
        {
            var type = new Int();

            type.Should().NotBeNull();
            type.Name.Should().Be("INT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeZero()
        {
            var type = new Int();

            type.GetValue().Should().Be(0);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("INT");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = new Int();

            type.SetValue(value.ToString());

            type.GetValue().Should().Be(0);
        }

        [Test]
        public void ParseValue_InvalidValue_ShouldBeNull()
        {
            var type = new Int();

            type.SetValue("Invalid");

            type.GetValue().Should().Be(0);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Int();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = new Int();

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
    }
}