using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void Types_WhenCalled_ShouldNotBeEmpty()
        {
            var predefined = Predefined.Types;

            predefined.Should().NotBeEmpty();
        }

        [Test]
        public void Atomics_WhenCalled_ShouldNotBeEmpty()
        {
            var atomic = Predefined.Atomics.ToList();

            atomic.Should().NotBeEmpty();
            atomic.Should().Contain(x => x.Name == "BOOL");
            atomic.Should().Contain(x => x.Name == "SINT");
            atomic.Should().Contain(x => x.Name == "INT");
            atomic.Should().Contain(x => x.Name == "DINT");
            atomic.Should().Contain(x => x.Name == "LINT");
            atomic.Should().Contain(x => x.Name == "REAL");
        }

        [Test]
        public void Description_GetValue_ShouldBeEmpty()
        {
            var type = Predefined.Undefined;

            type.Description.Should().BeEmpty();
        }

        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Undefined;

            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void DefaultValue_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Undefined;

            type.DefaultValue.Should().Be(null);
        }

        [Test]
        public void DefaultRadix_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Undefined;

            type.DefaultRadix.Should().Be(Radix.Null);
        }

        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Undefined;

            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void GetMember_TypeWithMember_ShouldNotBeNull()
        {
            var type = Predefined.String;
            var member = type.GetMember("Data");
            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_TypeWithoutMember_ShouldBeNull()
        {
            var type = Predefined.Bool;
            var member = type.GetMember("Member");
            member.Should().BeNull();
        }

        [Test]
        public void GetDependentTypes_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = Predefined.String;

            type.GetDependentTypes().Should().NotBeEmpty();
        }

        [Test]
        public void GetDependentTypes_TypeWithNoMembers_ShouldBeEmpty()
        {
            var type = Predefined.Undefined;

            type.GetDependentTypes().Should().BeEmpty();
        }

        [Test]
        public void ContainsType_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Predefined.ContainsType("BOOL").Should().BeTrue();
        }

        [Test]
        public void ContainsType_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Predefined.ContainsType("TEMP").Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_NonAtomicValidRadixForType_ShouldBeTrue()
        {
            var type = Predefined.Undefined;

            var result = type.SupportsRadix(Radix.Null);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_NonAtomicInvalidRadixForType_ShouldBeFalse()
        {
            var type = Predefined.Undefined;

            var result = type.SupportsRadix(Radix.Decimal);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_AtomicValidRadixForType_ShouldBeTrue()
        {
            var type = Predefined.Dint;

            var result = type.SupportsRadix(Radix.Decimal);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_AtomicInvalidRadixForType_ShouldBeFalse()
        {
            var type = Predefined.Dint;

            var result = type.SupportsRadix(Radix.Float);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidValue_ValidValueForType_ShouldBeTrue()
        {
            var type = Predefined.Undefined;

            var result = type.IsValidValue(null);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidValue_InvalidValueForType_ShouldBeFalse()
        {
            var type = Predefined.Undefined;

            var result = type.IsValidValue(true);

            result.Should().BeFalse();
        }

        [Test]
        public void ParseValue_UndefinedTypeTrueString_ShouldBeNull()
        {
            var type = Predefined.Undefined;

            var result = type.ParseValue("true");

            result.Should().Be(null);
        }

        [Test]
        public void ParseValue_UndefinedTypeNullString_ShouldBeNull()
        {
            var type = Predefined.Undefined;

            var result = type.ParseValue("null");

            result.Should().Be(null);
        }

        [Test]
        public void ParseValue_UndefinedTypeNullValue_ShouldBeNull()
        {
            var type = Predefined.Undefined;

            var result = type.ParseValue(null);

            result.Should().Be(null);
        }

        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType)Predefined.Bool;

            FluentActions.Invoking(() => (DataType)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void New_PredefinedTypeName_ShouldThrowPredefinedCollisionException()
        {
            FluentActions.Invoking(() => new DataType("Undefined")).Should().Throw<PredefinedCollisionException>();
        }

        [Test]
        public void ParseType_RegisteredType_ShouldNotBeNull()
        {
            var type = Predefined.ParseType("Bool");
            type.Should().NotBeNull();
            type.Should().Be(Predefined.Bool);
        }

        [Test]
        public void ParseType_StaticField_ShouldNotBeNull()
        {
            var type = Predefined.ParseType("bit");
            type.Should().NotBeNull();
            type.Should().Be(Predefined.Bit);
            type.Name.Should().Be("BOOL");
        }

        [Test]
        public void ParseType_AssemblyValidType_ShouldNotBeExpected()
        {
            var type = Predefined.ParseType("MyPredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("MyPredefined");
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void ParseType_AssemblyInvalidType_ShouldNotBeUndefined()
        {
            var type = Predefined.ParseType("MyNullNamePredefined");
            type.Should().NotBeNull();
            type.Should().Be(Predefined.Undefined);
        }
        
        [Test]
        public void ParseType_NonExistingType_ShouldNotBeUndefined()
        {
            var type = Predefined.ParseType("Invalid");
            type.Should().NotBeNull();
            type.Should().Be(Predefined.Undefined);
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

            private static IEnumerable<ReadOnlyMember> GetMembers()
            {
                return new List<ReadOnlyMember>
                {
                    ReadOnlyMember.New("Member01", Bool),
                    ReadOnlyMember.New("Member01", Bool),
                    ReadOnlyMember.New("Member03", Int)
                };
            }
        }

        [Test]
        public void Equals_TypeOverloadEquals_ShouldBeTrue()
        {
            var type1 = Predefined.Bool;
            var type2 = Predefined.Bool;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNotEquals_ShouldBeFalse()
        {
            var type1 = Predefined.Bool;
            var type2 = Predefined.Int;

            var result = type1.Equals(type2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var type = Predefined.Bool;

            var result = type.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_ObjectOverloadEquals_ShouldBeTrue()
        {
            var type1 = Predefined.Bool;
            var type2 = (object) Predefined.Bool;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var type1 = Predefined.Bool;
            var type2 = (object) type1;

            var result = type1.Equals(type2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var type = Predefined.Bool;

            var result = type.Equals((object) null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var type1 = Predefined.Bool;

            var result = type1.Equals(type1);

            result.Should().BeTrue();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = Predefined.Undefined;

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var type1 = Predefined.Undefined;
            var type2 = Predefined.Undefined;

            var result = type1 == type2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var type1 = Predefined.Undefined;
            var type2 = Predefined.Undefined;

            var result = type1 != type2;

            result.Should().BeFalse();
        }
    }
}