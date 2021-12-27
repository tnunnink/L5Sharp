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
    public class MembersTests
    {
        private List<IMember<IDataType>> _members;
        private DataType _dataType;

        [SetUp]
        public void Setup()
        {
            _dataType = new DataType("Test", DataTypeClass.Unknown, "This is a test");
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
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Int>("TestMember");

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_Null_ShouldNotChangeCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Add(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            FluentActions.Invoking(() => members.Add(Member.Create<Bool>("Member01"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddRange_ValidMembers_ShouldHaveExpectedCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            
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
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.AddRange(null!);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Update_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Update(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Int>("TestMember");

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_Null_ShouldHaveSameCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Update(null);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Update_ExistingMember_ShouldChangeTheCurrentMember()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Dint>("Member02", Dimensions.Empty, Radix.Binary, ExternalAccess.ReadOnly);

            members.Update(member);

            var result = members.GetMember("Member02");
            result.Should().NotBeNull();
            result?.DataType.Should().BeOfType<Dint>();
            result?.Radix.Should().Be(Radix.Binary);
            result?.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void Remove_ExistingName_ShouldShouldNotContainName()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Remove("Member01");

            members.Should().NotContain(m => m.Name == "Member01");
        }

        [Test]
        public void Remove_NonExistingName_ShouldNotChangeCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);
            var member = Member.Create<Dint>("TestMember");

            members.Remove(member.Name);

            members.Should().HaveCount(3);
        }

        [Test]
        public void Remove_Null_ShouldNotChangeCount()
        {
            var members = new Members<IMember<IDataType>>(_dataType, _members);

            members.Remove(null!);

            members.Should().HaveCount(3);
        }
    }
}