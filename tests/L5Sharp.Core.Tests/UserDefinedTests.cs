using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class UserDefinedTests
    {
        [Test]
        public void Create_ValidArguments_ShouldNotBeNull()
        {
            var type = UserDefined.Create("Mytype");

            type.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => UserDefined.Create(null!)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_EmptyName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => UserDefined.Create(string.Empty)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidTagNameException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => UserDefined.Create(fixture.Create<string>())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_ValidNameAndDescription_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var description = fixture.Create<string>();

            var type = UserDefined.Create("Test", description: description);

            type.Should().NotBeNull();
            type.Name.Should().Be("Test");
            type.Class.Should().Be(DataTypeClass.User);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be(description);
            type.Members.Should().BeEmpty();
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

            var type = UserDefined.Create("Test", description, members);

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

            var type = UserDefined.Create("Test", description, members);

            type.Should().NotBeNull();
            type.Members.Should().HaveCount(10);
            type.Members.Should().BeEquivalentTo(members);
        }

        [Test]
        public void CastedToComplexType_ShouldHaveSameMemberCount()
        {
            var type = UserDefined.Create("Test", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Real())
            });

            type.Members.Should().HaveCount(1);

            type.Members.Add(Member.Create<Dint>("Member02"));
            type.Members.Should().HaveCount(2);

            var complex = (IComplexType)type;
            complex.Members.Should().HaveCount(2);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldReturnDifferentInstance()
        {
            var type = UserDefined.Create("Type01", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Counter()),
                Member.Create("Member03", new Dint()),
                Member.Create("Member04", new String())
            });

            var instance = type.Instantiate();

            instance.Should().NotBeSameAs(type);
        }
        
        [Test]
        public void GetDependentTypes_WhenCalled_ShouldContainExpectedTypes()
        {
            var type = UserDefined.Create("Type01", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Counter()),
                Member.Create("Member03", new Dint()),
                Member.Create("Member04", new String())
            });

            var dependents = type.GetDependentTypes().ToList();

            dependents.Should().HaveCount(5);
            dependents.Should().Contain(new Bool());
            dependents.Should().Contain(new Counter());
            dependents.Should().Contain(new Dint());
            dependents.Should().Contain(new String());
            dependents.Should().Contain(new Sint());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = UserDefined.Create("Test");

            type.ToString().Should().Be("Test");
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (UserDefined)UserDefined.Create("Test");
            var second = (UserDefined)UserDefined.Create("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (UserDefined)UserDefined.Create("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (UserDefined)UserDefined.Create("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = (UserDefined)UserDefined.Create("Test");
            var second = (UserDefined)UserDefined.Create("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = (UserDefined)UserDefined.Create("Test");
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = (UserDefined)UserDefined.Create("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (UserDefined)UserDefined.Create("Test");
            var second = (UserDefined)UserDefined.Create("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (UserDefined)UserDefined.Create("Test");
            var second = (UserDefined)UserDefined.Create("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = UserDefined.Create("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}