using System;
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
            _userDefined = new DataType("MyType", description: "This is a test user type");
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
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new DataTypeMembers(_userDefined);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new DataTypeMembers(_userDefined);
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var members = new DataTypeMembers(_userDefined);

            FluentActions.Invoking(() => members.Add(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new DataTypeMembers(_userDefined);
            var member = Member.Create<Dint>("TestMember");

            members.Update(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new DataTypeMembers(_userDefined);
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var members = new DataTypeMembers(_userDefined);

            FluentActions.Invoking(() => members.Update(null)).Should().Throw<ArgumentNullException>();
        }
    }
}