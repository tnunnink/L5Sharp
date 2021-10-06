using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_EmptyTagName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new DataType(string.Empty)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new DataType(fixture.Create<string>())).Should()
                .Throw<InvalidNameException>();
        }

        [Test]
        public void New_ValidNameAndDescription_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();

            var type = new DataType("Test_Type_001", description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test_Type_001");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.IsAtomic.Should().BeFalse();
            type.Members.Should().BeEmpty();
        }

        [Test]
        public void SetName_ValidName_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description);

            type.Name = "NewName";

            type.Name.Should().Be("NewName");
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description);

            FluentActions.Invoking(() => type.Name = "Not.Valid%01").Should().Throw<InvalidNameException>();
        }

        [Test]
        public void SetDescription_WhenCalled_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var newDescription = fixture.Create<string>();
            var type = new DataType("Test", description);

            type.Description = newDescription;

            type.Description.Should().Be(newDescription);
        }

        [Test]
        public void AddMember_ExistingMember_ShouldThrowMemberNameCollisionException()
        {
            var type = new DataType("Test_Type_001");
            type.AddMember("Member", DataType.Dint);

            FluentActions.Invoking(() => type.AddMember("Member", DataType.Int)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddMember_InvalidTagName_ShouldThrowInvalidTagNameException()
        {
            var type = new DataType("Test_Type_001");

            FluentActions.Invoking(() => type.AddMember("This_is Not_It", DataType.Dint)).Should()
                .Throw<InvalidNameException>();
        }

        [Test]
        public void AddMember_NullDataType_ShouldHaveMemberWithNullType()
        {
            var type = new DataType("Test_Type_001");

            type.AddMember("Member", null);

            var member = type.Members.Single(m => m.Name == "Member");
            member.DataType.Should().Be(DataType.Null);
        }

        [Test]
        public void AddMember_NullRadix_ShouldThrowArgumentNullException()
        {
            var type = new DataType("Test_Type_001");

            FluentActions.Invoking(() => type.AddMember("Member", DataType.Int, builder => builder.HasRadix(null)))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddMember_InvalidRadixForType_ShouldThrowRadixNotSupportedException()
        {
            var type = new DataType("Test_Type_001");

            FluentActions.Invoking(() => type.AddMember("Member", DataType.Int, radix: Radix.Float)).Should()
                .Throw<RadixNotSupportedException>();
        }

        [Test]
        public void AddMember_NullExternalAccess_ShouldThrowArgumentNullException()
        {
            var type = new DataType("Test_Type_001");

            FluentActions.Invoking(() => type.AddMember("Member", DataType.Int, b => b.HasAccess(null))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddMember_ValidArguments_ShouldHaveExpectedMember()
        {
            var type = new DataType("Test_Type_001");

            type.AddMember("Member", DataType.Dint);

            type.Members.Should().HaveCount(1);
            var result = type.Members.Single(m => m.Name == "Member");
            result.Should().NotBeNull();
            result.Name.Should().Be("Member");
            result.DataType.Should().Be(DataType.Dint);
            result.Dimension.Should().Be(0);
            result.Description.Should().Be(string.Empty);
            result.Radix.Should().Be(Radix.Decimal);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void AddMember_FluentBuilder_ShouldHaveExpectedProperties()
        {
            var type = new DataType("Test_Type_001");

            type.AddMember("Member", DataType.Dint,
                b => b.HasDimension(4)
                    .HasDescription("This is a test description")
                    .HasRadix(Radix.Hex)
                    .HasAccess(ExternalAccess.None));

            var result = type.Members.SingleOrDefault(m => m.Name == "Member");
            result.Should().NotBeNull();
            result?.Name.Should().Be("Member");
            result?.DataType.Should().Be(DataType.Dint);
            result?.Dimension.Should().Be(4);
            result?.Description.Should().Be("This is a test description");
            result?.Radix.Should().Be(Radix.Hex);
            result?.ExternalAccess.Should().Be(ExternalAccess.None);
        }

        [Test]
        public void AddMember_BooleanMember_ShouldHaveBackingMember()
        {
            var type = new DataType("Test");
            
            type.AddMember("Member", DataType.Bool);

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void UpdateMember_ExistingMemberWithProperties_ShouldHaveExpectedProperties()
        {
            var type = new DataType("Test_Type_001");
            type.AddMember("Member", DataType.Dint);

            type.UpdateMember("Member",
                b => b.HasType(DataType.Int)
                    .HasDimension(4)
                    .HasDescription("This is a test description")
                    .HasRadix(Radix.Hex)
                    .HasAccess(ExternalAccess.None));

            var result = type.Members.SingleOrDefault(m => m.Name == "Member");
            result.Should().NotBeNull();
            result?.Name.Should().Be("Member");
            result?.DataType.Should().Be(DataType.Int);
            result?.Dimension.Should().Be(4);
            result?.Description.Should().Be("This is a test description");
            result?.Radix.Should().Be(Radix.Hex);
            result?.ExternalAccess.Should().Be(ExternalAccess.None);
        }

        [Test]
        public void UpdateMember_NonExistingMember_ShouldThrowMemberNotFoundException()
        {
            var type = new DataType("Test_Type_001");
            type.AddMember("Member", DataType.Dint);

            FluentActions.Invoking(() => type.UpdateMember("Test", b => b.HasType(DataType.Bool))).Should()
                .Throw<ComponentNotFoundException>();
        }

        [Test]
        public void RemoveMember_ExistingMember_MembersShouldEmpty()
        {
            var type = new DataType("Test_Type_001");
            type.AddMember("Member", DataType.Dint);

            type.RemoveMember("Member");

            type.Members.Should().BeEmpty();
        }

        [Test]
        public void RemoveMember_NonExistingMember_MembersShouldSame()
        {
            var type = new DataType("Test_Type_001");
            type.AddMember("Member", DataType.Dint);

            type.RemoveMember("Test");

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void GetMember_ExistingName_ShouldNotBeNullAndExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description);
            type.AddMember("Member", DataType.Dint);

            var member = type.GetMember("Member");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(DataType.Dint);
        }

        [Test]
        public void GetDependentTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.AddMember("Member01", DataType.Bool);
            type.AddMember("Member02", DataType.Counter);
            type.AddMember("Member03", DataType.Dint);
            type.AddMember("Member04", DataType.String);
            var dependentUserType = new DataType("Type02");
            dependentUserType.AddMember("Member01", DataType.Bool);
            dependentUserType.AddMember("Member02", DataType.Dint);
            dependentUserType.AddMember("Member03", DataType.String);
            dependentUserType.AddMember("Member04", DataType.Timer);
            type.AddMember("Member05", dependentUserType);

            var dependents = type.GetDependentTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().Contain(DataType.Bool);
            dependents.Should().Contain(DataType.Counter);
            dependents.Should().Contain(DataType.Dint);
            dependents.Should().Contain(DataType.String);
            dependents.Should().Contain(DataType.Timer);
            dependents.Should().Contain(dependentUserType);
        }
        
        [Test]
        public void GetDependentUserTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.AddMember("Member01", DataType.Bool);
            type.AddMember("Member02", DataType.Counter);
            type.AddMember("Member03", DataType.Dint);
            type.AddMember("Member04", DataType.String);
            var dependentUserType = new DataType("Type02");
            dependentUserType.AddMember("Member01", DataType.Bool);
            dependentUserType.AddMember("Member02", DataType.Dint);
            dependentUserType.AddMember("Member03", DataType.String);
            dependentUserType.AddMember("Member04", DataType.Timer);
            type.AddMember("Member05", dependentUserType);

            var dependents = type.GetDependentUserTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().HaveCount(1);
            dependents.Should().Contain(dependentUserType);
        }
    }
}