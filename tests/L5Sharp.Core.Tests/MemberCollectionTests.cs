using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberCollectionTests
    {
        private List<IMember<IDataType>> _members;

        [SetUp]
        public void Setup()
        {
            _members = new List<IMember<IDataType>>
            {
                Member.Create<Bool>("M1"),
                Member.Create<Int>("M2"),
                Member.Create<Dint>("M3"),
                Member.Create<String>("M4"),
                Member.Create<Timer>("M5")
            };
        }
        
        [Test]
        public void New_DefaultOverload_ShouldNotBeNullAndEmpty()
        {
            var members = new MemberCollection<IMember<IDataType>>();

            members.Should().NotBeNull();
            members.Should().BeEmpty();
        }

        [Test]
        public void New_MembersOverload_ShouldHaveExpectedCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Should().HaveCount(5);
        }

        [Test]
        public void New_DuplicateMembers_ShouldThrowComponentNameCollisionException()
        {
            var members = new List<IMember<IDataType>>
            {
                Member.Create<Bool>("M1"),
                Member.Create<Int>("M2"),
                Member.Create<Dint>("M1"),
            };

            FluentActions.Invoking(() => new MemberCollection<IMember<IDataType>>(members)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Count_WhenCalled_ShouldBeExpected()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Count.Should().Be(5);
        }

        [Test]
        public void Contains_ExistingMember_ShouldBeTrue()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Contains("M1");

            result.Should().BeTrue();
        }

        [Test]
        public void Contains_NonExistingMember_ShouldBeFalse()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Contains("M0");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Contains(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Get_ExistingMember_ShouldNotBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Get("M1");

            result.Should().NotBeNull();
        }

        [Test]
        public void Get_ExistingMember_ShouldHaveExpectedName()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Get("M1");

            result?.Name.Should().Be("M1");
        }

        [Test]
        public void Get_NonExistingMember_ShouldBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Get("M0");

            result.Should().BeNull();
        }

        [Test]
        public void Get_Null_ShouldBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.Get(null!);

            result.Should().BeNull();
        }

        [Test]
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Int>("TestMember");

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_Null_ShouldNotChangeCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Add(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.Add(Member.Create<Bool>("Member01"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            
            var collection = new List<IMember<IDataType>>
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
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.AddRange(null!);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Update_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Dint>("TestMember");

            members.Update(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Int>("TestMember");

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_Null_ShouldHaveSameCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Update(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Update_ExistingMember_ShouldChangeTheCurrentMember()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Dint>("Member02", Dimensions.Empty, Radix.Binary, ExternalAccess.ReadOnly);

            members.Update(member);

            var result = members.Get("Member02");
            result.Should().NotBeNull();
            result?.DataType.Should().BeOfType<Dint>();
            result?.Radix.Should().Be(Radix.Binary);
            result?.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        /*[Test]
        public void UpdateRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
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
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.UpdateRange(null);

            members.Should().HaveCount(3);
        }*/

        [Test]
        public void Remove_ExistingName_ShouldShouldNotContainName()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Remove("Member01");

            members.Should().NotContain(m => m.Name == "Member01");
        }

        [Test]
        public void Remove_NonExistingName_ShouldNotChangeCout()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);
            var member = Member.Create<Dint>("TestMember");

            members.Remove(member.Name);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Remove_Null_ShouldNotChangeCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            members.Remove(null!);

            members.Should().HaveCount(3);
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var enumerable = (IEnumerable)members;

            var enumerator = enumerable.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}