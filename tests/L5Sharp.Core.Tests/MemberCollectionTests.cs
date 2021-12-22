using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public void Get_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.Get(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Iterate_EachMember_ShouldNotBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            foreach (var member in members)
            {
                member.Should().NotBeNull();
            }
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var enumerable = (IEnumerable)members;

            var enumerator = enumerable.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void DeepContains_ExistingMember_ShouldBeTrue()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepContains("M5.PRE");

            result.Should().BeTrue();
        }

        [Test]
        public void DeepContains_NonExistingMember_ShouldBeFalse()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepContains("M1.DATA");

            result.Should().BeFalse();
        }

        [Test]
        public void DeepContains_Null_ShouldBeFalse()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepContains(null!);

            result.Should().BeFalse();
        }
        
        [Test]
        public void DeepGet_ExistingMember_ShouldNotBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepGet("M5.PRE");

            result.Should().NotBeNull();
        }

        [Test]
        public void DeepGet_ExistingMember_ShouldHaveExpectedName()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepGet("M5.PRE");

            result?.Name.Should().Be("PRE");
            result?.DataType.Should().BeOfType<Dint>();
        }

        [Test]
        public void DeepGet_NonExistingMember_ShouldBeNull()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var result = members.DeepGet("M1.DATA");

            result.Should().BeNull();
        }
        
        [Test]
        public void DeepGet_Empty_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.DeepGet(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void DeepGet_Null_ShouldThrowArgumentNullException()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            FluentActions.Invoking(() => members.DeepGet(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void DeepGetAll_WhenCalled_ShouldHaveExpectedCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var all = members.DeepGetAll();

            all.Should().HaveCount(12);
        }

        [Test]
        public void DeepNames_WhenCalled_ShouldHaveExpectedCount()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var all = members.DeepNames();

            all.Should().HaveCount(12);
        }
        
        [Test]
        public void DeepNames_WhenCalled_ShouldContainExpectedNames()
        {
            var members = new MemberCollection<IMember<IDataType>>(_members);

            var all = members.DeepNames().ToList();

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