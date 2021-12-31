using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class ReadOnlyMembersTests
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
            var members = new ReadOnlyMembers<IMember<IDataType>>();

            members.Should().NotBeNull();
            members.Should().BeEmpty();
        }

        [Test]
        public void New_MembersOverload_ShouldHaveExpectedCount()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

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

            FluentActions.Invoking(() => new ReadOnlyMembers<IMember<IDataType>>(members)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Count_WhenCalled_ShouldBeExpected()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            members.Count.Should().Be(5);
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = Enumerable.Contains(members, null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Get_ExistingMember_ShouldNotBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M1");

            result.Should().NotBeNull();
        }

        [Test]
        public void Get_ExistingMember_ShouldHaveExpectedName()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M1");

            result?.Name.Should().Be("M1");
        }

        [Test]
        public void Get_NonExistingMember_ShouldBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M0");

            result.Should().BeNull();
        }

        [Test]
        public void Get_Null_ShouldThrowArgumentNullException()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.GetMember(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Iterate_EachMember_ShouldNotBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            foreach (var member in members)
            {
                member.Should().NotBeNull();
            }
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var enumerable = (IEnumerable)members;

            var enumerator = enumerable.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void DeepContains_ExistingMember_ShouldBeTrue()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.Contains("M5.PRE");

            result.Should().BeTrue();
        }

        [Test]
        public void DeepContains_NonExistingMember_ShouldBeFalse()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.Contains("M1.DATA");

            result.Should().BeFalse();
        }

        [Test]
        public void DeepContains_Null_ShouldBeFalse()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.Contains(null!);

            result.Should().BeFalse();
        }
        
        [Test]
        public void DeepGet_ExistingMember_ShouldNotBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M5.PRE");

            result.Should().NotBeNull();
        }

        [Test]
        public void DeepGet_ExistingMember_ShouldHaveExpectedName()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M5.PRE");

            result?.Name.Should().Be("PRE");
            result?.DataType.Should().BeOfType<Dint>();
        }

        [Test]
        public void DeepGet_NonExistingMember_ShouldBeNull()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var result = members.GetMember("M1.DATA");

            result.Should().BeNull();
        }
        
        [Test]
        public void DeepGet_Empty_ShouldThrowArgumentNullException()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.GetMember(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void DeepGet_Null_ShouldThrowArgumentNullException()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.GetMember(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void DeepGetAll_WhenCalled_ShouldHaveExpectedCount()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var all = members.GetMembers();

            all.Should().HaveCount(12);
        }

        [Test]
        public void DeepNames_WhenCalled_ShouldHaveExpectedCount()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var all = members.GetNames();

            all.Should().HaveCount(12);
        }
        
        [Test]
        public void DeepNames_WhenCalled_ShouldContainExpectedNames()
        {
            var members = new ReadOnlyMembers<IMember<IDataType>>(_members);

            var all = members.GetNames().ToList();

            all.Should().Contain("M1");
            all.Should().Contain("M2");
            all.Should().Contain("M3");
            all.Should().Contain("M4");
            all.Should().Contain("M4.LEN");
            all.Should().Contain("M4.DATA");
            all.Should().Contain("M5");
            all.Should().Contain("M5.PRE");
            all.Should().Contain("M5.ACC");
            all.Should().Contain("M5.DN");
            all.Should().Contain("M5.EN");
            all.Should().Contain("M5.TT");
        }
    }
}