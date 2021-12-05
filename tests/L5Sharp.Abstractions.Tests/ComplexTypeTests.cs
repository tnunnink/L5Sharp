using System;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Abstractions.Tests
{
    public class TestComplex : ComplexType
    {
        public TestComplex() : base(nameof(TestComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New()
        {
            return new TestComplex();
        }

        public IMember<Bool> TestMember = Member.Create<Bool>(nameof(TestMember));
    }

    [TestFixture]
    public class ComplexTypeTests
    {
        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = new TestComplex();

            type.Name.Should().Be("TestPredefined");
        }

        [Test]
        public void Description_GetValue_ShouldBeNull()
        {
            var type = new TestComplex();

            type.Description.Should().BeNull();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = new TestComplex();

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)new TestComplex();

            FluentActions.Invoking(() => (UserDefined)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_MyNullNamePredefined_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new MyNullNamePredefined()).Should().Throw<ArgumentException>();
        }

        private class MyNullNamePredefined : ComplexType
        {
            public MyNullNamePredefined() : base((ComponentName)null)
            {
            }

            public override DataTypeClass Class { get; }

            protected override IDataType New()
            {
                return new MyNullNamePredefined();
            }
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
        public void New_InvalidMemberType_ShouldThrowComponentNameCollisionException()
        {
            FluentActions.Invoking(() => new MyInvalidMemberPredefined()).Should()
                .Throw<ComponentNameCollisionException>();
        }

        private class MyInvalidMemberPredefined : ComplexType
        {
            public MyInvalidMemberPredefined() :
                base(nameof(MyInvalidMemberPredefined))
            {
            }

            public IMember<Bool> Member01 = Member.Create<Bool>("Member01");
            public IMember<Bool> Member02 = Member.Create<Bool>("Member01");

            public override DataTypeClass Class { get; }

            protected override IDataType New()
            {
                return new MyInvalidMemberPredefined();
            }
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var type = new TestComplex();

            var str = type.ToString();

            str.Should().Be("TestPredefined");
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