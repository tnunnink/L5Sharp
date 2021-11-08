using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class StringTypeTests
    {
        [Test]
        public void New_ValidNameAndLength_ShouldNotBeNull()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();

            var type = new StringDefined("Test", 100, desc);

            type.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidLength_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();

            FluentActions.Invoking(() => new StringDefined("Test", 0, desc)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new StringDefined(null, 0)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Name.Should().Be("Test");
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Class.Should().Be(DataTypeClass.User);
        }
        
        [Test]
        public void Family_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Family.Should().Be(DataTypeFamily.String);
        }
        
        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Description.Should().BeEmpty();
        }
        
        [Test]
        public void DataFormat_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.DataFormat.Should().Be(TagDataFormat.String);
        }

        [Test]
        public void Len_GetValue_ShouldHaveExpectedProperties()
        {
            var type = new StringDefined("Test", 10);

            type.LEN.Should().NotBeNull();
            type.LEN.Name.Should().Be("LEN");
            type.LEN.DataType.Should().Be(new Dint());
            type.LEN.Dimensions.Length.Should().Be(0);
            type.LEN.Radix.Should().Be(Radix.Decimal);
            type.LEN.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.LEN.Description.Should().BeNull();
        }
        
        [Test]
        public void Data_GetValue_ShouldHaveExpectedProperties()
        {
            var type = new StringDefined("Test", 100);
            
            type.DATA.Should().NotBeNull();
            type.DATA.Name.Should().Be("DATA");
            type.DATA.DataType.Should().Be(new Sint());
            type.DATA.Dimensions.Length.Should().Be(100);
            type.DATA.Radix.Should().Be(Radix.Ascii);
            type.DATA.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.DATA.Description.Should().BeNull();
        }
    }
}