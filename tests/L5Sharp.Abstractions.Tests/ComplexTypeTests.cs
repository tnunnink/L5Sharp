using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Abstractions.Tests
{
    [TestFixture]
    public class ComplexTypeTests
    {
        [Test]
        public void ComplexType_WhenCastedToUserDefined_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)new TestComplex();

            FluentActions.Invoking(() => (UserDefined)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_MyNullNamePredefined_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new NullNameComplex()).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_InvalidMemberType_ShouldThrowComponentNameCollisionException()
        {
            FluentActions.Invoking(() => new DuplicateMemberNameComplex()).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void New_MemberLessComplex_ShouldNotBeNullAndHaveEmptyMembers()
        {
            var type = new MemberLessComplex();

            type.Should().NotBeNull();
            type.Members.Should().BeEmpty();
        }

        [Test]
        public void New_NullMemberComplex_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new NullMemberComplex()).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_MemberConstructorComplex_ShouldNotBeNullAndHaveExpectedMembers()
        {
            var type = new MemberConstructorComplex();

            type.Should().NotBeNull();
            type.Members.Should().HaveCount(3);
        }

        [Test]
        public void New_ComplexType_ShouldNotBeNull()
        {
            var type = new TestComplex();

            type.Should().NotBeNull();
        }

        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = new TestComplex();

            type.Name.Should().Be(nameof(TestComplex));
        }

        [Test]
        public void Description_GetValue_ShouldBeNull()
        {
            var type = new TestComplex();

            type.Description.Should().BeEmpty();
        }

        [Test]
        public void Class_ValidType_ShouldBeExpected()
        {
            var type = new TestComplex();

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Family_ValidType_ShouldBeExpected()
        {
            var type = new TestComplex();

            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void Members_ValidType_ShouldNotBeEmpty()
        {
            var type = new TestComplex();

            type.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Instantiate_WhenCalled_ReturnsNewInstanceWithEqualValue()
        {
            var type = new TestComplex();

            var instance = type.Instantiate();

            instance.Should().NotBeSameAs(type);
            instance.Should().BeEquivalentTo(type);
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var type = new TestComplex();

            var str = type.ToString();

            str.Should().Be(nameof(TestComplex));
        }
    }
}