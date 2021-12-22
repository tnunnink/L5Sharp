using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MembersTests
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
        public void GetMember_ImmediateMember_ShouldBeExpectedMember()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var member = members.Get("M3");

            member.Should().NotBeNull();
            member?.Name.Should().Be("M3");
            member?.DataType.Should().BeOfType<Dint>();
        }

        [Test]
        public void GetMember_NestedMember_ShouldBeExpectedMember()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var member = members.Get("M4.DATA");

            member.Should().NotBeNull();
            member?.Name.Should().Be("DATA");
            member?.DataType.Should().BeOfType<Sint>();
        }
    }
}