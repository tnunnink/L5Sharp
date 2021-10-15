using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class StringTests
    {
        [Test]
        public void New_String_ShouldNotBeNull()
        {
            var type = new String();

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void SetMemberName_ShouldNotBeNull()
        {
            var type = new String();

            FluentActions.Invoking(() => (Member) type.Data).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void Predefined_String_ShouldNotBeNull()
        {
            var type = Predefined.String;

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void Predefined_String_ShouldHaveMembers()
        {
            var type = Predefined.String;

            type.Len.Should().NotBeNull();
            type.Data.Should().NotBeNull();
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeEmpty()
        {
            var type = new String();

            type.DefaultValue.Should().Be(string.Empty);
        }
        
        [Test]
        public void DefaultRadix_WhenCalled_ShouldBeExpected()
        {
            var type = new String();

            type.DefaultRadix.Should().Be(Radix.Null);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Predefined.ParseType("STRING");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = Predefined.String;

            var result = type.ParseValue(value);

            result.Should().Be(value);
        }

        [Test]
        public void ParseValue_InvalidValue_ShouldBeNull()
        {
            var type = Predefined.String;

            var result = type.ParseValue(null);

            result.Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = Predefined.String;

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = Predefined.String;

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeFalse();
        }
        
        [Test]
        public void IsValidValue_ValidValue_ShouldBeTrue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = Predefined.String;

            var result = type.IsValidValue(value);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidValue_Null_Should()
        {
            var type = Predefined.String;

            var value = type.IsValidValue(null);

            value.Should().BeFalse();
        }
    }
}