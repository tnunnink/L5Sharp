using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new DataType(null)).Should()
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

            var type = new DataType("Test", description: description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.Members.Should().BeEmpty();
            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void New_MembersOverload_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var members = new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Bool())
            };

            var type = new DataType("Test", members, description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.Members.Should().HaveCount(2);
            type.Members.Should().BeEquivalentTo(members);
        }

        [Test]
        public void New_MembersOverloadTen_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var members = new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Bool()),
                Member.Create("Member03", new Bool()),
                Member.Create("Member04", new Bool()),
                Member.Create("Member05", new Int()),
                Member.Create("Member06", new Bool()),
                Member.Create("Member07", new Bool()),
                Member.Create("Member08", new Real()),
                Member.Create("Member09", new Bool()),
                Member.Create("Member10", new Bool())
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
            var type = new DataType("Test", description: description);

            type.SetName("NewName");

            type.Name.Should().Be("NewName");
        }

        [Test]
        public void Name_SetValueInvalidName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();
            var type = new DataType("Test", description: description);

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
            var type = new DataType("Test", description: description);

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
            var member = Member.Create("Member", new Bool());
            var type = new DataType("Test", members: new[] { member });

            var result = type.Members.Get("Member");

            result.Should().Be(member);
        }

        [Test]
        public void GetMember_Null_ShouldThrowArgumentNullException()
        {
            var member = Member.Create("Member", new Bool());
            var type = new DataType("Test", members: new[] { member });

            var result = type.Members.Get((string)null);

            result.Should().BeNull();
        }

        [Test]
        public void GetMember_MemberThatDoesNotExist_ShouldBeNull()
        {
            var member = Member.Create("Member", new Bool());
            var type = new DataType("Test", members: new[] { member });

            var result = type.Members.Get("MemberName");

            result.Should().BeNull();
        }

        [Test]
        public void AddMember_ExistingMember_ShouldThrowMemberNameCollisionException()
        {
            var member = Member.Create("Member", new Dint());
            var type = new DataType("Test", members: new[] { member });

            FluentActions.Invoking(() => type.Members.Add(Member.Create("Member", new Int()))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddMember_InvalidName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();
            var type = new DataType("Test");

            FluentActions
                .Invoking(() => type.Members.Add(Member.Create(fixture.Create<string>(), new Dint())))
                .Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void AddMember_NullName_ShouldThrowArgumentNullException()
        {
            var type = new DataType("Test");

            FluentActions.Invoking(() => type.Members.Add(Member.Create(null, new Dint())))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void AddMember_NullDataType_ShouldHaveMemberWithNullType()
        {
            var type = new DataType("Test");

            type.Members.Add(Member.Create("Member", null));

            var member = type.Members.Get("Member");
            member.DataType.Should().BeNull();
        }

        [Test]
        public void AddMember_InvalidRadixForType_ShouldThrowRadixNotSupportedException()
        {
            var type = new DataType("Test");

            FluentActions
                .Invoking(
                    () => type.Members.Add(Member.Create("Member", new Int(), radix: Radix.Float)))
                .Should().Throw<RadixNotSupportedException>();
        }

        [Test]
        public void AddMember_ValidArguments_ShouldHaveExpectedMember()
        {
            var type = new DataType("Test");

            type.Members.Add(Member.Create("Member", new Dint()));

            type.Members.Should().HaveCount(1);
            var member = type.Members.Get("Member");
            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(new Dint());
            member.Dimensions.Length.Should().Be(0);
            member.Description.Should().BeNull();
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void AddMember_BooleanMember_ShouldContainMember()
        {
            var type = new DataType("Test");

            type.Members.Add(Member.Create("Member", new Bool()));

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void RemoveMember_ExistingMember_MembersShouldEmpty()
        {
            var member = Member.Create("Member", new Dint());
            var type = new DataType("Test", members: new[] { member });

            type.Members.Remove("Member");

            type.Members.Should().BeEmpty();
        }

        [Test]
        public void RemoveMember_NonExistingMember_MembersShouldSingle()
        {
            var member = Member.Create("Member", new Dint());
            var type = new DataType("Test", members: new[] { member });

            type.Members.Remove("Test");

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void GetDependentTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.Members.Add(Member.Create("Member01", new Bool()));
            type.Members.Add(Member.Create("Member02", new Counter()));
            type.Members.Add(Member.Create("Member03", new Dint()));
            type.Members.Add(Member.Create("Member04", new String()));
            var dependentUserType = new DataType("Type02");
            dependentUserType.Members.Add(Member.Create("Member01", new Bool()));
            dependentUserType.Members.Add(Member.Create("Member02", new Dint()));
            dependentUserType.Members.Add(Member.Create("Member03", new String()));
            dependentUserType.Members.Add(Member.Create("Member04", new Timer()));
            type.Members.Add(Member.Create("Member05", dependentUserType));

            var dependents = type.GetDependentTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().Contain(new Bool());
            dependents.Should().Contain(new Counter());
            dependents.Should().Contain(new Dint());
            dependents.Should().Contain(new String());
            dependents.Should().Contain(new Timer());
            dependents.Should().Contain(dependentUserType);
        }

        [Test]
        public void GetDependentUserTypes_ComplexType_ShouldContainExpectedTypes()
        {
            var type = new DataType("Type01");
            type.Members.Add(Member.Create("Member01", new Bool()));
            type.Members.Add(Member.Create("Member02", new Counter()));
            type.Members.Add(Member.Create("Member03", new Dint()));
            type.Members.Add(Member.Create("Member04", new String()));
            var dependentUserType = new DataType("Type02");
            dependentUserType.Members.Add(Member.Create("Member01", new Bool()));
            dependentUserType.Members.Add(Member.Create("Member02", new Dint()));
            dependentUserType.Members.Add(Member.Create("Member03", new String()));
            dependentUserType.Members.Add(Member.Create("Member04", new Timer()));
            type.Members.Add(Member.Create("Member05", dependentUserType));

            var dependents = type.GetDependentTypes().Where(t => t is IUserDefined).ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().HaveCount(1);
            dependents.Should().Contain(dependentUserType);
        }

        [Test]
        public void TypeEquals_Equal_ShouldBeTrue()
        {
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DataType("Test");
            var second = new DataType("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new DataType("Test");
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new DataType("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DataType("Test");
            var second = new DataType("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new DataType("Test");
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new DataType("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new DataType("Test");
            var second = new DataType("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new DataType("Test");
            var second = new DataType("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = Member.Create("Test", new Bool());

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}