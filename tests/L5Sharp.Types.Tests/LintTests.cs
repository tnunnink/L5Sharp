using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class LintTests
    {
        [Test]
        public void New_Lint_ShouldNotBeNull()
        {
            var type = new Lint();

            type.Should().NotBeNull();
            type.Name.Should().Be("LINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Predefined_Lint_ShouldNotBeNull()
        {
            var type = new Lint();

            type.Should().NotBeNull();
            type.Name.Should().Be("LINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeZero()
        {
            var type = new Lint();

            type.GetValue().Should().Be(0);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Create("LINT");

            type.Should().NotBeNull();
        }

        [Test]
        public void SetValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();
            var type = new Lint();

            type.SetValue(value.ToString());

            type.GetValue().Should().Be(value);
        }

        [Test]
        public void SetValue_InvalidValue_ShouldBeNull()
        {
            var type = new Lint();

            type.SetValue("Invalid");

            type.GetValue().Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = new Lint();

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = new Lint();

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
    }
}