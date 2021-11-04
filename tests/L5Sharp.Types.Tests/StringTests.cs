using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;
using String = L5Sharp.Types.String;

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
        public void Predefined_String_ShouldHaveMembers()
        {
            var type = Predefined.String;

            type.Len.Should().NotBeNull();
            type.Data.Should().NotBeNull();
        }

        [Test]
        public void ParseType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("STRING");

            type.Should().NotBeNull();
        }
    }
}