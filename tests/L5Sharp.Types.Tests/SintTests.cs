﻿using AutoFixture;
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
            var type = Predefined.Sint;

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void DefaultValue_WhenCalled_ShouldBeZero()
        {
            var type = new Sint();

            type.DefaultValue.Should().Be(0);
        }
        
        [Test]
        public void DefaultRadix_WhenCalled_ShouldBeExpected()
        {
            var type = new Sint();

            type.DefaultRadix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Predefined.ParseType("SINT");

            type.Should().NotBeNull();
        }

        [Test]
        public void ParseValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = Predefined.Sint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }


        [Test]
        public void ParseValue_InvalidValue_ShouldBeNull()
        {
            var type = Predefined.Sint;

            var result = type.ParseValue("Invalid");

            result.Should().Be(null);
        }
        
        [Test]
        public void SupportsRadix_Decimal_ShouldBeTrue()
        {
            var type = Predefined.Sint;

            var value = type.SupportsRadix(Radix.Decimal);

            value.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_Null_ShouldBeFalse()
        {
            var type = Predefined.Sint;

            var value = type.SupportsRadix(Radix.Null);

            value.Should().BeFalse();
        }
        
        [Test]
        public void IsValidValue_ValidValue_ShouldBeTrue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = Predefined.Sint;

            var result = type.IsValidValue(value);

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_String_Should()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = Predefined.Sint;

            var result = type.IsValidValue(value.ToString());

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsValidValue_Null_Should()
        {
            var type = Predefined.Sint;

            var value = type.IsValidValue(null);

            value.Should().BeFalse();
        }
    }
}