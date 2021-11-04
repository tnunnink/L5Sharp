using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class AtomicTests
    {
        [Test]
        public void Name_GetValue_ShouldBeEmpty()
        {
            var type = new Bool();

            type.Name.Should().BeEmpty();
        }

        [Test]
        public void Description_GetValue_ShouldBeEmpty()
        {
            var type = new Bool();

            type.Description.Should().BeEmpty();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = new Bool();

            type.Class.Should().Be(DataTypeClass.Atomic);
        }

        [Test]
        public void DefaultValue_ValidType_ShouldReturnExpected()
        {
            var type = new Bool();

            type.DefaultValue.Should().Be(false);
        }

        [Test]
        public void DefaultRadix_ValidType_ShouldReturnExpected()
        {
            var type = new Bool();

            type.DefaultRadix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = new Bool();

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
            //todo are we saying atomics have members?
            /*var type = new Bool();
            
            var member = type.GetMember("Member");
            
            member.Should().BeNull();*/
        }

        

        [Test]
        public void SupportsRadix_Supported_ShouldBeTrue()
        {
            var type = new Bool();

            var result = type.SupportsRadix(Radix.Binary);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_NotSupported_ShouldBeFalse()
        {
            var type = new Bool();

            var result = type.SupportsRadix(Radix.Exponential);

            result.Should().BeFalse();
        }
        
        [Test]
        public void SupportsRadix_NonOverriddenSupported_ShouldBeTrue()
        {
            var type = new Int();

            var result = type.SupportsRadix(Radix.Hex);

            result.Should().BeTrue();
        }

        [Test]
        public void Atomic_WhenCastedToIUserDefined_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)Logix.DataType.Bool;

            FluentActions.Invoking(() => (IUserDefined)atomic).Should().Throw<InvalidCastException>();
        }
        
        [Test]
        public void Atomic_WhenCastedToIPredefined_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)Logix.DataType.Bool;

            FluentActions.Invoking(() => (IPredefined)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_PredefinedTypeName_ShouldThrowComponentNameCollisionException()
        {
            FluentActions.Invoking(() => new DataType("Undefined")).Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void MyAtomic_new()
        {
            var type = new MyAtomic();
            type.Should().BeNull();
            
            
        }

        private class MyAtomic : Atomic
        {
            public MyAtomic() :
                base(nameof(MyAtomic))
            {
            }

            public override bool IsValidValue(object value)
            {
                throw new NotImplementedException();
            }

            public override object ParseValue(string value)
            {
                throw new NotImplementedException();
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
            var type2 = new Bool();

            var result = type1.Equals((object) type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var type1 = new Bool();

            var result = type1.Equals((object) type1);

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
            var type = new Bool();

            var result = type.Equals(type);

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new Bool();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var type1 = new Bool();
            var type2 = new Bool();

            var result = type1 == type2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var type1 = new Bool();
            var type2 = new Bool();

            var result = type1 != type2;

            result.Should().BeFalse();
        }
    }
}