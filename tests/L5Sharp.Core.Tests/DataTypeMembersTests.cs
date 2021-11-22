using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeMembersTests
    {
        private DataType _userDefined;

        [SetUp]
        public void Setup()
        {
            _userDefined = new DataType("MyType", "This is a test user type");
        }

        [Test]
        public void New_ValidParentType_ShouldNotBeNull()
        {
            var members = new DataTypeMembers(_userDefined);

            members.Should().NotBeNull();
        }

        [Test]
        public void New_MembersOverload_ShouldHaveExpectedCount()
        {
            var members = new DataTypeMembers(_userDefined, new[] { Member.Create<Bool>("Member01") });

            members.Should().HaveCount(1);
        }

        [Test]
        public void Count_SeededCollection_ShouldBeExpected()
        {
            var members = CreateSeededMembers();

            members.Count.Should().Be(3);
        }

        [Test]
        public void Contains_ExistingMember_ShouldBeTrue()
        {
            var members = CreateSeededMembers();

            var result = members.Contains("Member01");

            result.Should().BeTrue();
        }

        [Test]
        public void Contains_NonExistingMember_ShouldBeFalse()
        {
            var members = CreateSeededMembers();

            var result = members.Contains("Member04");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var members = CreateSeededMembers();

            var result = members.Contains(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Get_ExistingMember_ShouldNotBeNull()
        {
            var members = CreateSeededMembers();

            var result = members.Get("Member01");

            result.Should().NotBeNull();
        }

        [Test]
        public void Get_ExistingMember_ShouldHaveExpectedName()
        {
            var members = CreateSeededMembers();

            var result = members.Get("Member01");

            result.Name.Should().Be("Member01");
        }

        [Test]
        public void Get_NonExistingMember_ShouldBeNull()
        {
            var members = CreateSeededMembers();

            var result = members.Get("Member04");

            result.Should().BeNull();
        }

        [Test]
        public void Get_Null_ShouldBeNull()
        {
            var members = CreateSeededMembers();

            var result = members.Get(null);

            result.Should().BeNull();
        }

        [Test]
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = CreateSeededMembers();
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = CreateSeededMembers();
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_Null_ShouldNotChangeCount()
        {
            var members = CreateSeededMembers();

            members.Add(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Add(Member.Create<Bool>("Member01"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = CreateSeededMembers();
            var collection = new List<IMember<IDataType>>()
            {
                Member.Create<Sint>("Member04"),
                Member.Create<Sint>("Member05"),
                Member.Create<Sint>("Member06")
            };

            members.AddRange(collection);

            members.Should().HaveCount(6);
        }

        [Test]
        public void AddRange_Null_ShouldNotChangeCount()
        {
            var members = CreateSeededMembers();

            members.AddRange(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Insert_ValidMember_ShouldBeContainedInMembers()
        {
            var members = CreateSeededMembers();
            var member = Member.Create<Dint>("TestMember");

            members.Insert(0, member);

            members.Should().Contain(member);
        }

        [Test]
        public void Insert_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = CreateSeededMembers();
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Insert(0, member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Insert_Null_ShouldNotChangeCount()
        {
            var members = CreateSeededMembers();

            members.Insert(0, null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Insert_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Insert(1, Member.Create<Bool>("Member01"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Update_ValidMember_ShouldBeContainedInMembers()
        {
            var members = CreateSeededMembers();
            var member = Member.Create<Dint>("TestMember");

            members.Update(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = CreateSeededMembers();
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_Null_ShouldHaveSameCount()
        {
            var members = CreateSeededMembers();

            members.Update(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Update_ExistingMember_ShouldChangeTheCurrentMember()
        {
            var members = CreateSeededMembers();
            var member = Member.Create<Dint>("Member02", Radix.Binary, ExternalAccess.ReadOnly);

            members.Update(member);

            var result = members.Get("Member02");
            result.Should().NotBeNull();
            result.DataType.Should().BeOfType<Dint>();
            result.Radix.Should().Be(Radix.Binary);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void UpdateRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = CreateSeededMembers();
            var collection = new List<IMember<IDataType>>
            {
                Member.Create<Sint>("Member02"),
                Member.Create<Real>("Member05"),
                Member.Create<Sint>("Member06")
            };

            members.UpdateRange(collection);

            members.Should().HaveCount(5);
        }

        [Test]
        public void UpdateRange_Null_ShouldNotChangeCount()
        {
            var members = CreateSeededMembers();

            members.UpdateRange(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Remove_ExistingName_ShouldShouldNotContainName()
        {
            var members = CreateSeededMembers();

            members.Remove("Member01");

            members.Should().NotContain(m => m.Name == "Member01");
        }

        [Test]
        public void Rename_ExistingMemberToDifferentName_ShouldHaveNewNotOld()
        {
            var members = CreateSeededMembers();

            members.Rename("Member01", "Member05");

            members.Should().Contain(m => m.Name == "Member05");
            members.Should().NotContain(m => m.Name == "Member01");
        }

        [Test]
        public void Rename_ExistingMemberToExistingName_ShouldThrowException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Rename("Member01", "Member02")).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Rename_NonExisting_ShouldThrowException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Rename("Member00", "Member05")).Should()
                .Throw<InvalidOperationException>();
        }
        
        [Test]
        public void Rename_CurrentNull_ShouldThrowException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Rename(null, "Member05")).Should()
                .Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Rename_NameNull_ShouldThrowException()
        {
            var members = CreateSeededMembers();

            FluentActions.Invoking(() => members.Rename("Member01", null)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_NonExistingName_ShouldNotChangeCout()
        {
            var members = CreateSeededMembers();
            var member = Member.Create<Dint>("TestMember");

            members.Remove(member.Name);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Remove_Null_ShouldNotChangeCount()
        {
            var members = CreateSeededMembers();

            members.Remove(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var members = CreateSeededMembers();

            var enumerable = (IEnumerable)members;

            var enumerator = enumerable.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        private DataTypeMembers CreateSeededMembers()
        {
            return new DataTypeMembers(_userDefined, new List<IMember<IDataType>>
            {
                Member.Create<Dint>("Member01"),
                Member.Create<Real>("Member02"),
                Member.Create<Bool>("Member03")
            });
        }
    }
}