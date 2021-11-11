using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MembersTests
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
            var members = new Members(_userDefined);

            members.Should().NotBeNull();
        }

        [Test]
        public void New_MembersOverload_ShouldHaveExpectedCount()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            members.Should().HaveCount(1);
        }

        [Test]
        public void Add_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new Members(_userDefined);
            var member = Member.Create<Dint>("TestMember");

            members.Add(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Add_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members(_userDefined);
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Add(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members(_userDefined);

            FluentActions.Invoking(() => members.Add(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_ValidMember_ShouldBeContainedInMembers()
        {
            var members = new Members(_userDefined);
            var member = Member.Create<Dint>("TestMember");

            members.Update(member);

            members.Should().Contain(member);
        }

        [Test]
        public void Update_SelfReferencingMember_ShouldThrowCircularReferenceException()
        {
            var members = new Members(_userDefined);
            var member = Member.Create("TestMember", _userDefined);

            FluentActions.Invoking(() => members.Update(member)).Should().Throw<CircularReferenceException>();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var members = new Members(_userDefined);

            FluentActions.Invoking(() => members.Update(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void UpdateDataType_InvalidName_ShouldThrowInvalidOperationException()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            FluentActions.Invoking(() => members.UpdateDataType("Member02", new Dint())).Should()
                .Throw<InvalidOperationException>();
        }
        
        [Test]
        public void UpdateDataType_InvalidType_ShouldThrowCircularReferenceException()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            FluentActions.Invoking(() => members.UpdateDataType("Member01", _userDefined)).Should()
                .Throw<CircularReferenceException>();
        }

        [Test]
        public void UpdateDataType_ValidName_ShouldHaveUpdatedType()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            members.UpdateDataType("Member01", new Counter());

            members.Get("Member01").DataType.Should().Be(new Counter());
        }
        
        [Test]
        public void UpdateDimensions_InvalidName_ShouldThrowInvalidOperationException()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            FluentActions.Invoking(() => members.UpdateDimensions("Member02", 4)).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void UpdateDimensions_ValidName_ShouldHaveUpdatedType()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            members.UpdateDimensions("Member01", new Dimensions(4));

            members.Get("Member01").Dimensions.Length.Should().Be(4);
        }

        [Test]
        public void UpdateAccess_InvalidName_ShouldThrowInvalidOperationException()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            FluentActions.Invoking(() => members.UpdateAccess("Member02", ExternalAccess.ReadOnly)).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void UpdateAccess_ValidName_ShouldHaveUpdatedType()
        {
            var members = new Members(_userDefined, new[] { Member.Create<Bool>("Member01") });

            members.UpdateAccess("Member01", ExternalAccess.ReadOnly);

            members.Get("Member01").ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
    }
}