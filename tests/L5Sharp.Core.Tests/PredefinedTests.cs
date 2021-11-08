using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = new Undefined();

            type.Name.Should().Be("Undefined");
        }
        
        [Test]
        public void Description_GetValue_ShouldBeEmpty()
        {
            var type = new Undefined();

            type.Description.Should().BeEmpty();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = new Undefined();

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = new Undefined();

            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void GetMember_TypeWithMember_ShouldNotBeNull()
        {
            var type = new String();
            var member = type.GetMember("Data");
            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_TypeWithoutMember_ShouldBeNull()
        {
            var type = new Undefined();
            var member = type.GetMember("Member");
            member.Should().BeNull();
        }

        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)new Bool();

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

            private static IEnumerable<Member<IDataType>> GetMembers()
            {
                return new List<Member<IDataType>>
                {
                    new("Member01", new Bool()),
                    new("Member01", new Bool()),
                    new("Member03", new Int())
                };
            }
        }

        [Test]
        public void Equals_TypeOverloadEquals_ShouldBeTrue()
        {
            var type1 = new Bool();
            var type2 = new Bool();

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNotEquals_ShouldBeFalse()
        {
            var type1 = new Bool();
            var type2 = new Int();

            var result = type1.Equals(type2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var type = new Bool();

            var result = type.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_ObjectOverloadEquals_ShouldBeTrue()
        {
            var type1 = new Bool();
            var type2 = (object) new Bool();

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var type1 = new Bool();
            var type2 = (object) type1;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var type = new Bool();

            var result = type.Equals((object) null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var type1 = new Bool();

            var result = type1.Equals(type1);

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new Undefined();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var type1 = new Undefined();
            var type2 = new Undefined();

            var result = type1 == type2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var type1 = new Undefined();
            var type2 = new Undefined();

            var result = type1 != type2;

            result.Should().BeFalse();
        }
    }
}