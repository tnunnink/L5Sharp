using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using LogixHelper.Enumerations;
using LogixHelper.Exceptions;
using LogixHelper.Primitives;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_EmptyString_ShouldNotBeNull()
        {
            FluentActions.Invoking(() => new DataType(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidString_ShouldNotBeNull()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new DataType(fixture.Create<string>())).Should()
                .Throw<InvalidTagNameException>();
        }

        [Test]
        public void New_Bool_ShouldNotBeNull()
        {
            var type = DataType.Bool;

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Sint_ShouldNotBeNull()
        {
            var type = DataType.Sint;

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Int_ShouldNotBeNull()
        {
            var type = DataType.Int;

            type.Should().NotBeNull();
            type.Name.Should().Be("INT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Dint_ShouldNotBeNull()
        {
            var type = DataType.Dint;

            type.Should().NotBeNull();
            type.Name.Should().Be("DINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Real_ShouldNotBeNull()
        {
            var type = DataType.Real;

            type.Should().NotBeNull();
            type.Name.Should().Be("REAL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_ValidNameAndDescription_ShouldBeValidType()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            
            var type = new DataType("Test_Type_001", description);
            
            type.Should().NotBeNull();
            type.Name.Should().Be("Test_Type_001");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
        }

        [Test]
        public void Predefined_WhenCalled_ShouldNotBeEmpty()
        {
            var predefined = DataType.Predefined;

            predefined.Should().NotBeEmpty();
        }

        [Test]
        public void StringType_ValidNameAndLength_ShouldHaveExpectedProperties()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();

            var type = DataType.StringType("Test_String_001", 100, desc);
            
            type.Should().NotBeNull();
            type.Name.Should().Be("Test_String_001");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.String);
            type.Description.Should().Be(desc);
            type.Members.Should().Contain(m => m.Name == "LEN");
            type.Members.Should().Contain(m => m.Name == "DATA");
        }
        
        [Test]
        public void StringType_InvalidLength_ShouldThrowException()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();

            FluentActions.Invoking(() => DataType.StringType("Test_String_001", 0, desc)).Should().Throw<ArgumentException>();
        }
        
        /*[Test]
        public void StringType_AddMember_ShouldThrowException()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();
            
            var type = DataType.StringType("Test_String_001", 100, desc);

            FluentActions.Invoking(() => type.AddMember("Test", DataType.Dint, "Test")).Should().Throw<NotConfigurableException>();
        }*/
        
        [Test]
        public void StringType_RemoveMember_ShouldThrowException()
        {
            var fixture = new Fixture();
            var desc = fixture.Create<string>();
            
            var type = DataType.StringType("Test_String_001", 100, desc);

            FluentActions.Invoking(() => type.RemoveMember("LEN")).Should().Throw<NotConfigurableException>();
        }

        [Test]
        public void AddMember_ValidArguments_ShouldHaveExpectedMember()
        {
            var type = new DataType("Test_Type_001");
            
            type.AddMember("Member", DataType.Dint);
            
            type.Members.Should().HaveCount(1);
            var result = type.Members.SingleOrDefault(m => m.Name == "Member");
            result.Should().NotBeNull();
            result?.Name.Should().Be("Member");
            result?.DataType.Should().Be(DataType.Dint);
            result?.Dimension.Should().Be(0);
            result?.Description.Should().Be(string.Empty);
            result?.Radix.Should().Be(Radix.Decimal);
            result?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
        
        [Test]
        public void AddMember_InvalidTagName_ShouldThrowInvalidTagNameException()
        {
            var type = new DataType("Test_Type_001");

            FluentActions.Invoking(() => type.AddMember("This_is Not_It", DataType.Dint)).Should()
                .Throw<InvalidTagNameException>();
        }

        [Test]
        public void AddMember_WithProperties_ShouldHaveExpectedProperties()
        {
            var type = new DataType("Test_Type_001");

            type.AddMember("Member", DataType.Dint)
                .WithDataType(DataType.Int)
                .WithDimension(4)
                .WithDescription("This is a test description")
                .WithRadix(Radix.Hex)
                .WithAccess(ExternalAccess.None);

            var result = type.Members.SingleOrDefault(m => m.Name == "Member");
            result.Should().NotBeNull();
            result?.Name.Should().Be("Member");
            result?.DataType.Should().Be(DataType.Int);
            result?.Dimension.Should().Be(4);
            result?.Description.Should().Be("This is a test description");
            result?.Radix.Should().Be(Radix.Hex);
            result?.ExternalAccess.Should().Be(ExternalAccess.None);
        }
    }
}