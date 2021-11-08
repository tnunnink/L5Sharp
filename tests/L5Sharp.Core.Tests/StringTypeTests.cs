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

            type.Len.Should().NotBeNull();
            type.Len.Name.Should().Be("LEN");
            type.Len.DataType.Should().Be(new Dint());
            type.Len.Dimensions.Length.Should().Be(0);
            type.Len.Radix.Should().Be(Radix.Decimal);
            type.Len.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.Len.Description.Should().BeNull();
        }
        
        [Test]
        public void Data_GetValue_ShouldHaveExpectedProperties()
        {
            var type = new StringDefined("Test", 100);
            
            type.Data.Should().NotBeNull();
            type.Data.Name.Should().Be("DATA");
            type.Data.DataType.Should().Be(new Sint());
            type.Data.Dimensions.Length.Should().Be(100);
            type.Data.Radix.Should().Be(Radix.Ascii);
            type.Data.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.Data.Description.Should().BeNull();
        }

        [Test]
        public void UpdateLength_ValidLength_ShouldUpdateDataMember()
        {
            var type = new StringDefined("Test", 100);
            
            type.UpdateLength(25);

            type.Data.Dimensions.Length.Should().Be(25);
        }
        
        [Test]
        public void UpdateLength_InvalidLength_ShouldUpdateDataMember()
        {
            var type = new StringDefined("Test", 10);
            
            FluentActions.Invoking(() => type.UpdateLength(0)).Should().Throw<ArgumentException>();
        }
    }
}