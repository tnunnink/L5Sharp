using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = Logix.DataType.Undefined;

            type.Name.Should().Be("Undefined");
        }
        
        [Test]
        public void Description_GetValue_ShouldBeEmpty()
        {
            var type = Logix.DataType.Undefined;

            type.Description.Should().BeEmpty();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = Logix.DataType.Undefined;

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = Logix.DataType.Undefined;

            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void GetMember_TypeWithMember_ShouldNotBeNull()
        {
            var type = Logix.DataType.String;
            var member = type.GetMember("Data");
            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_TypeWithoutMember_ShouldBeNull()
        {
            var type = Logix.DataType.Undefined;
            var member = type.GetMember("Member");
            member.Should().BeNull();
        }

        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)Logix.DataType.Bool;

            FluentActions.Invoking(() => (DataType)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_PredefinedTypeName_ShouldThrowComponentNameCollisionException()
        {
            FluentActions.Invoking(() => new DataType("Undefined")).Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void New_MyPredefined_ShouldNotBeNull()
        {
            var type = new MyPredefined();
            type.Should().NotBeNull();
        }

        private class MyPredefined : Predefined
        {
            public MyPredefined() :
                base(nameof(MyPredefined), DataTypeFamily.None)
            {
            }
        }
        
        [Test]
        public void New_MyNullNamePredefined_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new MyNullNamePredefined()).Should().Throw<ArgumentException>();
        }
        
        private class MyNullNamePredefined : Predefined
        {
            public MyNullNamePredefined() :
                base(null, DataTypeFamily.None)
            {
            }
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
                base(nameof(MyInvalidMemberPredefined), DataTypeFamily.None, GetMembers())
            {
            }

            private static IEnumerable<Member> GetMembers()
            {
                return new List<Member>
                {
                    new("Member01", Logix.DataType.Bool),
                    new("Member01", Logix.DataType.Bool),
                    new("Member03", Logix.DataType.Int)
                };
            }
        }

        [Test]
        public void Equals_TypeOverloadEquals_ShouldBeTrue()
        {
            var type1 = Logix.DataType.Bool;
            var type2 = Logix.DataType.Bool;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNotEquals_ShouldBeFalse()
        {
            var type1 = Logix.DataType.Bool;
            var type2 = Logix.DataType.Int;

            var result = type1.Equals(type2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var result = type.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_ObjectOverloadEquals_ShouldBeTrue()
        {
            var type1 = Logix.DataType.Bool;
            var type2 = (object) Logix.DataType.Bool;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var type1 = Logix.DataType.Bool;
            var type2 = (object) type1;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var type = Logix.DataType.Bool;

            var result = type.Equals((object) null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var type1 = Logix.DataType.Bool;

            var result = type1.Equals(type1);

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = Logix.DataType.Undefined;

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var type1 = Logix.DataType.Undefined;
            var type2 = Logix.DataType.Undefined;

            var result = type1 == type2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var type1 = Logix.DataType.Undefined;
            var type2 = Logix.DataType.Undefined;

            var result = type1 != type2;

            result.Should().BeFalse();
        }
    }
}