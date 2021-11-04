using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new DataType(string.Empty)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_EmptyName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new DataType(string.Empty)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new DataType(fixture.Create<string>())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_ValidNameAndDescription_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();

            var type = new DataType("Test", description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.IsAtomic.Should().BeFalse();
            type.Members.Should().BeEmpty();
            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void New_MemberOverload_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var member = new DataTypeMember("Member", Logix.DataType.Bool);

            var type = new DataType("Test", member, description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.IsAtomic.Should().BeFalse();
            type.Members.Should().HaveCount(1);
            type.Members.Should().Contain(member);
        }

        [Test]
        public void New_MembersOverload_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var members = new List<DataTypeMember>
            {
                new("Member01", Logix.DataType.Bool),
                new("Member02", Logix.DataType.Bool)
            };

            var type = new DataType("Test", members, description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.IsAtomic.Should().BeFalse();
            type.Members.Should().HaveCount(2);
            type.Members.Should().BeEquivalentTo(members);
        }

        [Test]
        public void New_MembersOverloadTen_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var members = new List<DataTypeMember>
            {
                new("Member01", Logix.DataType.Bool),
                new("Member02", Logix.DataType.Bool),
                new("Member03", Logix.DataType.Bool),
                new("Member04", Logix.DataType.Bool),
                new("Member05", Logix.DataType.Int),
                new("Member06", Logix.DataType.Bool),
                new("Member07", Logix.DataType.Bool),
                new("Member08", Logix.DataType.Real),
                new("Member09", Logix.DataType.Bool),
                new("Member10", Logix.DataType.Bool)
            };

            var type = new DataType("Test", members, description);

            type.Should().NotBeNull();
            type.Members.Should().HaveCount(10);
            type.Members.Should().BeEquivalentTo(members);
        }

        [Test]
        public void Name_SetValueValidName_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description);

            type.SetName("NewName");

            type.Name.Should().Be("NewName");
        }

        [Test]
        public void Name_SetValueInvalidName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description);

            FluentActions.Invoking(() => type.SetName("Not.Valid%01")).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Name_SetValueNull_ShouldThrowInvalidTagNameException()
        {
            var type = new DataType("Test");

            FluentActions.Invoking(() => type.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Description_SetValueValidValue_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var newDescription = fixture.Create<string>();
            var type = new DataType("Test", description);

            type.SetDescription(newDescription);

            type.Description.Should().Be(newDescription);
        }

        [Test]
        public void Description_SetValueNull_ShouldBeNull()
        {
            var type = new DataType("Test");

            type.SetDescription(null);

            type.Description.Should().BeNull();
        }

        [Test]
        public void GetMember_ValidName_ShouldBeExpectedMember()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Bool);
            var type = new DataType("Test", member);

            var result = type.Members.Get("Member");

            result.Should().Be(member);
        }

        [Test]
        public void GetMember_Null_ShouldThrowArgumentNullException()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Bool);
            var type = new DataType("Test", member);

            var result = type.Members.Get((string)null);

            result.Should().BeNull();
        }

        [Test]
        public void GetMember_MemberThatDoesNotExist_ShouldBeNull()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Bool);
            var type = new DataType("Test", member);

            var result = type.Members.Get("MemberName");

            result.Should().BeNull();
        }

        [Test]
        public void AddMember_ExistingMember_ShouldThrowMemberNameCollisionException()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Dint);
            var type = new DataType("Test", member);

            FluentActions.Invoking(() => type.Members.Add(new DataTypeMember("Member", Logix.DataType.Int))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddMember_InvalidName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();
            var type = new DataType("Test");

            FluentActions
                .Invoking(() => type.Members.Add(new DataTypeMember(fixture.Create<string>(), Logix.DataType.Dint)))
                .Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void AddMember_NullName_ShouldThrowArgumentNullException()
        {
            var type = new DataType("Test");

            FluentActions.Invoking(() => type.Members.Add(new DataTypeMember(null, Logix.DataType.Dint)))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void AddMember_NullDataType_ShouldHaveMemberWithNullType()
        {
            var type = new DataType("Test");

            type.Members.Add(new DataTypeMember("Member", null));

            var member = type.Members.Get("Member");
            member.DataType.Should().Be(Logix.DataType.Undefined);
        }

        [Test]
        public void AddMember_InvalidRadixForType_ShouldThrowRadixNotSupportedException()
        {
            var type = new DataType("Test");

            FluentActions
                .Invoking(() => type.Members.Add(new DataTypeMember("Member", Logix.DataType.Int, radix: Radix.Float)))
                .Should().Throw<RadixNotSupportedException>();
        }

        [Test]
        public void AddMember_ValidArguments_ShouldHaveExpectedMember()
        {
            var type = new DataType("Test");

            type.Members.Add(new DataTypeMember("Member", Logix.DataType.Dint));

            type.Members.Should().HaveCount(1);
            var member = type.Members.Get("Member");
            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(Logix.DataType.Dint);
            member.Dimensions.Length.Should().Be(0);
            member.Description.Should().BeNull();
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void AddMember_BooleanMember_ShouldHaveBackingMember()
        {
            var type = new DataType("Test");

            type.Members.Add(new DataTypeMember("Member", Logix.DataType.Bool));

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void RemoveMember_ExistingMember_MembersShouldEmpty()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Dint);
            var type = new DataType("Test", member);

            type.Members.Remove("Member");

            type.Members.Should().BeEmpty();
        }

        [Test]
        public void RemoveMember_NonExistingMember_MembersShouldSingle()
        {
            var member = new DataTypeMember("Member", Logix.DataType.Dint);
            var type = new DataType("Test", member);

            type.Members.Remove("Test");

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void GetDependentTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.Members.Add(new DataTypeMember("Member01", Logix.DataType.Bool));
            type.Members.Add(new DataTypeMember("Member02", Logix.DataType.Counter));
            type.Members.Add(new DataTypeMember("Member03", Logix.DataType.Dint));
            type.Members.Add(new DataTypeMember("Member04", Logix.DataType.String));
            var dependentUserType = new DataType("Type02");
            dependentUserType.Members.Add(new DataTypeMember("Member01", Logix.DataType.Bool));
            dependentUserType.Members.Add(new DataTypeMember("Member02", Logix.DataType.Dint));
            dependentUserType.Members.Add(new DataTypeMember("Member03", Logix.DataType.String));
            dependentUserType.Members.Add(new DataTypeMember("Member04", Logix.DataType.Timer));
            type.Members.Add(new DataTypeMember("Member05", dependentUserType));

            var dependents = type.GetDependentTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().Contain(Logix.DataType.Bool);
            dependents.Should().Contain(Logix.DataType.Counter);
            dependents.Should().Contain(Logix.DataType.Dint);
            dependents.Should().Contain(Logix.DataType.String);
            dependents.Should().Contain(Logix.DataType.Timer);
            dependents.Should().Contain(dependentUserType);
        }

        [Test]
        public void GetDependentUserTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.Members.Add(new DataTypeMember("Member01", Logix.DataType.Bool));
            type.Members.Add(new DataTypeMember("Member02", Logix.DataType.Counter));
            type.Members.Add(new DataTypeMember("Member03", Logix.DataType.Dint));
            type.Members.Add(new DataTypeMember("Member04", Logix.DataType.String));
            var dependentUserType = new DataType("Type02");
            dependentUserType.Members.Add(new DataTypeMember("Member01", Logix.DataType.Bool));
            dependentUserType.Members.Add(new DataTypeMember("Member02", Logix.DataType.Dint));
            dependentUserType.Members.Add(new DataTypeMember("Member03", Logix.DataType.String));
            dependentUserType.Members.Add(new DataTypeMember("Member04", Logix.DataType.Timer));
            type.Members.Add(new DataTypeMember("Member05", dependentUserType));

            var dependents = type.GetDependentUserTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().HaveCount(1);
            dependents.Should().Contain(dependentUserType);
        }

        [Test]
        public void TypeEquals_Equal_ShouldBeTrue()
        {
        }

        [Test]
        public void AddMember_Configuration_ShouldHaveExpectedMember()
        {
            var type = new DataType("Test");

            type.Members.Add("Test",
                c => c.OfType(Logix.DataType.Alarm)
                    .WithAccess(ExternalAccess.ReadOnly));

            var result = type.Members.Get("Test");

            result.Should().NotBeNull();
            result.DataType.Should().Be(Logix.DataType.Alarm);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
    }
}