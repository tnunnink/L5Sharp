using System.Collections.Generic;
using FluentAssertions;
using L5Sharp;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5SharpTests
{
    [TestFixture]
    public class LogixTests
    {
        [Test]
        public void List_WhenCalled_ShouldNotBeEmpty()
        {
            var dataTypes = Logix.DataType.List;

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Contains_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Logix.DataType.Contains("BOOL").Should().BeTrue();
        }

        [Test]
        public void Contains_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Logix.DataType.Contains("TEMP").Should().BeFalse();
        }

        [Test]
        public void CreateType_RegisteredType_ShouldNotBeNull()
        {
            var type = Logix.DataType.Instantiate("Bool");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void CreateType_StaticField_ShouldNotBeNull()
        {
            var type = Logix.DataType.Instantiate("bit");
            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void CreateType_AssemblyValidType_ShouldNotBeExpected()
        {
            var type = Logix.DataType.Instantiate("MyPredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("MyPredefined");
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void CreateType_AssemblyInvalidType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Instantiate("MyNullNamePredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }

        [Test]
        public void CreateType_NonExistingType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Instantiate("Invalid");
            type.Name.Should().Be("Undefined");
            type.Should().BeOfType<Undefined>();
        }

        [Test]
        public void CreateType_ValidName_ShouldNotBeNull()
        {
            var type = Logix.DataType.Instantiate("string");

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.String);
        }
    }
}