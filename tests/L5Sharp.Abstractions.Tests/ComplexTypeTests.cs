using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

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
        public void New_MemberCircularReference_ShouldThrowCircularReferenceException()
        {
            FluentActions.Invoking(() => new NullMemberComplex()).Should()
                .Throw<ArgumentNullException>();
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
        public void GetDependentTypes_WhenCalled_ShouldContainExpectedTypes()
        {
            var type = new TestComplex();

            var dependents = type.Members.GetDependentTypes().ToList();

            dependents.Should().NotBeEmpty();
            dependents.Should().Contain(new Bool());
            dependents.Should().Contain(new Int());
            dependents.Should().Contain(new Dint());
            dependents.Should().Contain(new String());
            dependents.Should().Contain(new Timer());
        }

        [Test]
        public void GetMember_Null_ShouldThrowArgumentException()
        {
            var type = new TestComplex();

            FluentActions.Invoking(() => type.Members.Get(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetMember_Empty_ShouldThrowArgumentException()
        {
            var type = new TestComplex();

            FluentActions.Invoking(() => type.Members.Get(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void DeepGet_ImmediateMember_ShouldNotBeNull()
        {
            var type = new TestComplex();

            var member = type.Members.DeepGet("Nested");

            member.Should().NotBeNull();
        }

        [Test]
        public void DeepGet_NestedMember_ShouldNotBeNull()
        {
            var type = new TestComplex();

            var member = type.Members.DeepGet("Nested.M1");

            member.Should().NotBeNull();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var type = new TestComplex();

            var str = type.ToString();

            str.Should().Be(nameof(TestComplex));
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestComplex();
            var second = new TestComplex();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new TestComplex();
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new TestComplex();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestComplex();
            var second = new TestComplex();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new TestComplex();
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new TestComplex();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestComplex();
            var second = new TestComplex();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new TestComplex();
            var second = new TestComplex();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new TestComplex();

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}