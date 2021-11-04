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
            var type = Logix.DataType.Lint;

            type.Should().NotBeNull();
            type.Name.Should().Be("LINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeZero()
        {
            var type = new Lint();

            type.DefaultValue.Should().Be(0);
        }
        
        [Test]
        public void DefaultRadix_WhenCalled_ShouldBeExpected()
        {
            var type = new Lint();

            type.DefaultRadix.Should().Be(Radix.Decimal);
        }
        

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("LINT");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();
            var type = Logix.DataType.Lint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }

        [Test]
        public void ParseValue_InvalidValue_ShouldBeNull()
        {
            var type = Logix.DataType.Lint;

            var result = type.ParseValue("Invalid");

            result.Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = Logix.DataType.Lint;

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = Logix.DataType.Lint;

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
        
        [Test]
        public void IsValidValue_ValidValue_ShouldBeTrue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();
            var type = Logix.DataType.Lint;

            var result = type.IsValidValue(value);

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_String_Should()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();
            var type = Logix.DataType.Lint;

            var result = type.IsValidValue(value.ToString());

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_Null_Should()
        {
            var type = Logix.DataType.Lint;

            var value = type.IsValidValue(null);

            value.Should().BeFalse();
        }
    }
}