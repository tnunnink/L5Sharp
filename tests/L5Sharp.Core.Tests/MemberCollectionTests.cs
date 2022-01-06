using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

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
        public void Clear_WhenCalled_ShouldBeEmpty()
        {
            var members = new MemberCollection(_dataType, _members);
            
            members.Clear();

            members.Should().BeEmpty();
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

            FluentActions.Invoking(() => members.Upsert(((IMember<IDataType>)null)!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Upsert_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new MemberCollection(_dataType, _members);
            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Upsert(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Upsert_NonExistingMember_ShouldAddMember()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create<Int>("Test");

            members.Upsert(member);

            members.Should().HaveCount(6);
            members.Should().Contain(member);
        }

        [Test]
        public void Upsert_ExistingMember_ShouldAddMember()
        {
            var members = new MemberCollection(_dataType, _members);

            var member = Member.Create<Int>("M3");

            members.Upsert(member);

            members.Should().HaveCount(5);
            members.Should().Contain(member);
            members.Get("M3")?.DataType.Should().BeOfType<Int>();
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
    }
}