using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Factories;
using L5Sharp.Predefined;
using NUnit.Framework;
using String = L5Sharp.Predefined.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberCollectionTests
    {
        private List<IMember<IDataType>> _members;
        private IUserDefined _dataType;

        [SetUp]
        public void Setup()
        {
            _dataType = new UserDefined("Test", "This is a test");
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
        public void Count_WhenCalled_ShouldBeExpectedNumber()
        {
            var members = new MemberCollection(_dataType, _members);

            members.Count.Should().Be(5);
        }

        [Test]
        public void Clear_WhenCalled_ShouldBeEmpty()
        {
            var members = new MemberCollection(_dataType, _members);

            members.Clear();

            members.Should().BeEmpty();
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var members = new MemberCollection(_dataType, _members);

            var result = members.Contains(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_NonExistingName_ShouldBeFalse()
        {
            var members = new MemberCollection(_dataType, _members);

            var result = members.Contains("M6");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_ExistingName_ShouldBeTrue()
        {
            var members = new MemberCollection(_dataType, _members);

            var result = members.Contains("M2");

            result.Should().BeTrue();
        }

        [Test]
        public void Find_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Find(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_ValidPredicate_ShouldReturnExpected()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = members.Find(m => m.DataType.Name == "DINT");

            member.Should().NotBeNull();
            member?.Name.Should().Be("M3");
        }

        [Test]
        public void FindAll_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.FindAll(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindAll_ValidPredicate_ShouldReturnExpected()
        {
            var members = new MemberCollection(_dataType, _members);

            var results = members.FindAll(m => m.Radix == Radix.Decimal).ToList();

            results.Should().NotBeNull();
            results.Should().HaveCount(3);
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Add(Member.Create<Bool>("M1"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Upsert_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Update(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Upsert_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Upsert_NonExistingMember_ShouldAddMember()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create<Int>("Test");

            members.Update(member);

            members.Should().HaveCount(6);
            members.Should().Contain(member);
        }

        [Test]
        public void Upsert_ExistingMember_ShouldAddMember()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create<Int>("M3");

            members.Update(member);

            members.Should().HaveCount(5);
            members.Should().Contain(member);
            members.Get("M3")?.DataType.Should().BeOfType<Int>();
        }

        [Test]
        public void Insert_Null_ShouldThrow()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Insert(0, null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Insert_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Insert(0, Member.Create<Bool>("M1"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Insert_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Insert(0, member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Insert_InvalidIndex_ShouldBeThrowArgumentOutOfRangeException()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            FluentActions.Invoking(() => members.Insert(-1, member)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Insert_ValidMemberAndIndex_ShouldBeContainedInMembers()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Insert(0, member);

            members.Should().Contain(member);
        }

        [Test]
        public void Remove_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Remove(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_Empty_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Remove(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Remove_NonExistingName_ShouldNotChangeCount()
        {
            var members = new MemberCollection(_dataType, _members);

            members.Remove("Test");

            members.Should().HaveCount(5);
        }

        [Test]
        public void Remove_ExistingName_ShouldShouldNotContainName()
        {
            var members = new MemberCollection(_dataType, _members);

            members.Remove("M1");

            members.Should().NotContain(m => m.Name == "M1");
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.Update(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_ExistingMember_ShouldUpdateMember()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create<Timer>("M2");

            members.Update(member);

            var updated = members.Get("M2");
            updated.Should().NotBeNull();
            updated?.DataType.Should().BeOfType<Timer>();
        }

        [Test]
        public void UpdateMany_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection(_dataType, _members);

            FluentActions.Invoking(() => members.UpdateMany(null!)).Should()
                .Throw<ArgumentNullException>();
        }
        
        [Test]
        public void UpdateMany_ValidCollection_ShouldUpdateMembers()
        {
            var members = new MemberCollection(_dataType, _members);
            var collection = new List<IMember<IDataType>>()
            {
                Member.Create<Timer>("M2"),
                Member.Create<String>("M4"),
                Member.Create<Dint>("M7")
            };
            

            members.UpdateMany(collection);
            
            members.Should().HaveCount(6);
        }

        [Test]
        public void Enumerate_WhenPerformed_ShouldWork()
        {
            var members = new MemberCollection(_dataType, _members);

            foreach (var member in members)
            {
                member.Should().NotBeNull();
            }
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var members = (IEnumerable)new MemberCollection(_dataType, _members);

            var enumerator = members.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}