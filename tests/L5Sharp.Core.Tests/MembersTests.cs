using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using L5Sharp.Types.Predefined;
using NUnit.Framework;
using String = L5Sharp.Types.Predefined.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MembersTests
    {
        private List<IMember<IDataType>> _members;
        private IUserDefined _dataType;

        [SetUp]
        public void Setup()
        {
            _dataType = UserDefined.Create("Test", "This is a test");
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
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            
            members.Clear();

            members.Should().BeEmpty();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Add(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Add(Member.Create<Bool>("M1"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void AddRange_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.AddRange(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddRange_DuplicateName_ShouldThrowComponentNameCollisionException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var collection = new List<IMember<IDataType>>
            {
                Member.Create<Sint>("M6"),
                Member.Create<Sint>("M7"),
                Member.Create<Sint>("M6")
            };

            FluentActions.Invoking(() => members.AddRange(collection)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddRange_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var collection = new List<IMember<IDataType>>
            {
                Member.Create<Sint>("M6"),
                Member.Create("M7", _dataType),
                Member.Create<Sint>("M8")
            };

            FluentActions.Invoking(() => members.AddRange(collection)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void AddRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var collection = new List<IMember<IDataType>>
            {
                Member.Create<Sint>("M6"),
                Member.Create<Sint>("M7"),
                Member.Create<Sint>("M8")
            };

            members.AddRange(collection);

            members.Should().HaveCount(8);
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Update(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create("TestMember", _dataType);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_NonExistingMember_ShouldAddMember()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var member = Member.Create<Int>("Test");

            members.Update(member);

            members.Should().HaveCount(6);
            members.Should().Contain(member);
        }

        [Test]
        public void Update_ExistingMember_ShouldAddMember()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            var member = Member.Create<Int>("M3");

            members.Update(member);

            members.Should().HaveCount(5);
            members.Should().Contain(member);
            members.GetMember("M3")?.DataType.Should().BeOfType<Int>();
        }

        [Test]
        public void Remove_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Remove(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Remove_Empty_ShouldThrowArgumentNullException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Remove(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Remove_NonExistingName_ShouldNotChangeCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Remove("Test");

            members.Should().HaveCount(5);
        }

        [Test]
        public void Remove_ExistingName_ShouldShouldNotContainName()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Remove("M1");

            members.Should().NotContain(m => m.Name == "M1");
        }
    }
}