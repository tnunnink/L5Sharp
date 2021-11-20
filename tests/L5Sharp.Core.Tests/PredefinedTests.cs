using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{

    public class TestPredefined : Predefined
    {
        public TestPredefined() : base(nameof(TestPredefined))
        {
            RegisterMember(TestMember);
        }

        protected override IDataType New()
        {
            return new TestPredefined();
        }

        public IMember<Bool> TestMember => Member.Create<Bool>(nameof(TestMember));
    }
    
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = new TestPredefined();

            type.Name.ToString().Should().Be("TestPredefined");
        }
        
        [Test]
        public void Description_GetValue_ShouldBeNull()
        {
            var type = new TestPredefined();

            type.Description.Should().BeNull();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = new TestPredefined();

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = new TestPredefined();

            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)new TestPredefined();

            FluentActions.Invoking(() => (DataType)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_MyNullNamePredefined_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new MyNullNamePredefined()).Should().Throw<ArgumentException>();
        }
        
        private class MyNullNamePredefined : Predefined
        {
            public MyNullNamePredefined() : base(null)
            {
            }

            protected override IDataType New()
            {
                return new MyNullNamePredefined();
            }
        }

        [Test]
        public void Instantiate_WhenCalled_ReturnsNewInstanceWithEqualValue()
        {
            var type = new TestPredefined();

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
        
        private class MyInvalidMemberPredefined : Predefined
        {
            public MyInvalidMemberPredefined() :
                base(nameof(MyInvalidMemberPredefined))
            {
                RegisterMember(Member01);
                RegisterMember(Member02);
            }

            public IMember<Bool> Member01 => Member.Create<Bool>("Member01");
            public IMember<Bool> Member02 => Member.Create<Bool>("Member01");
            protected override IDataType New()
            {
                return new MyInvalidMemberPredefined();
            }
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestPredefined();
            var second = new TestPredefined();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new TestPredefined();
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new TestPredefined();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestPredefined();
            var second = new TestPredefined();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new TestPredefined();
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new TestPredefined();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TestPredefined();
            var second = new TestPredefined();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new TestPredefined();
            var second = new TestPredefined();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new TestPredefined();

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}