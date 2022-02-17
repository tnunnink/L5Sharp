using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Extensions.Tests
{
    [TestFixture]
    public class DataTypeExtensionsTests
    {
        [Test]
        public void StructureEquals_SameAtomic_ShouldBeTrue()
        {
            var t1 = new Bool();
            var t2 = new Bool();

            var result = t1.StructureEquals(t2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_SameAtomicDifferentValues_ShouldBeTrue()
        {
            var t1 = new Int();
            var t2 = new Int(32);

            var result = t1.StructureEquals(t2);

            result.Should().BeTrue();
        }

        [Test]
        public void StructureEquals_DifferentAtomics_ShouldBeFalse()
        {
            var t1 = new Int();
            var t2 = new Dint();
            
            var result = t1.StructureEquals(t2);

            result.Should().BeFalse();
        }

        [Test]
        public void StructureEquals_SameComplex_ShouldBeTure()
        {
            var t1 = new Timer();
            var t2 = new Timer();
            
            var result = t1.StructureEquals(t2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_DifferentComplex_ShouldBeFalse()
        {
            var t1 = new Timer();
            var t2 = new Counter();
            
            var result = t1.StructureEquals(t2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_SameArray_ShouldBeTure()
        {
            var t1 = new ArrayType<Dint>(10);
            var t2 = new ArrayType<Dint>(10);
            
            var result = t1.StructureEquals(t2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_DifferentArrayLength_ShouldBeFalse()
        {
            var t1 = new ArrayType<Dint>(10);
            var t2 = new ArrayType<Dint>(11);
            
            var result = t1.StructureEquals(t2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_DifferentArrayType_ShouldBeFalse()
        {
            var t1 = new ArrayType<Dint>(5);
            var t2 = new ArrayType<Int>(5);
            
            var result = t1.StructureEquals(t2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_SameCustomType_ShouldBeTure()
        {
            var t1 = new MyNestedType();
            var t2 = new MyNestedType();
            
            var result = t1.StructureEquals(t2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_DifferentCustomType_ShouldBeFalse()
        {
            var t1 = new MyNestedType();
            var t2 = new MyOtherNestedType();
            
            var result = t1.StructureEquals(t2);

            result.Should().BeFalse();
        }

        [Test]
        public void StructureEquals_SameUserDefined_ShouldBeTrue()
        {
            var t1 = new UserDefined("Test", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create<Bool>("Member01"),
                Member.Create<Int>("Member02"),
                Member.Create<Real>("Member03"),
            });
            
            var t2 = new UserDefined("Test", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create<Bool>("Member01"),
                Member.Create<Int>("Member02"),
                Member.Create<Real>("Member03"),
            });

            t1.StructureEquals(t2).Should().BeTrue();
        }
        
        [Test]
        public void GetMembers_Atomic_ShouldBeEmpty()
        {
            var type = new Int();

            var members = type.GetMembers();

            members.Should().BeEmpty();
        }

        [Test]
        public void GetMembers_Complex_ShouldNotBeEmpty()
        {
            var type = new Timer();

            var members = type.GetMembers();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetMembers_ArrayOfAtomic_ShouldNotBeEmpty()
        {
            var type = new ArrayType<Bool>(5);

            var members = type.GetMembers();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetMembers_ArrayOfComplex_ShouldNotBeEmpty()
        {
            var type = new ArrayType<String>(5);

            var members = type.GetMembers();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetMember_Atomic_ShouldBeNull()
        {
            var type = new Bool();

            var member = type.GetMember("Child");

            member.Should().BeNull();
        }

        [Test]
        public void GetMember_Complex_ShouldNotBeNull()
        {
            var type = new Timer();

            var member = type.GetMember("PRE");

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_ArrayOfAtomic_ShouldNotBeNull()
        {
            var type = new ArrayType<Bool>(5);

            var member = type.GetMember("[2]");

            member.Should().NotBeNull();
        }

        [Test]
        public void GetTagNames_Atomic_ShouldBeEmpty()
        {
            var type = new Int();

            var members = type.GetTagNames();

            members.Should().BeEmpty();
        }

        [Test]
        public void GetTagNames_Complex_ShouldContainExpectedTagNames()
        {
            var type = new Timer();

            var members = type.GetTagNames().ToList();

            members.Should().Contain("PRE");
            members.Should().Contain("ACC");
            members.Should().Contain("EN");
            members.Should().Contain("DN");
            members.Should().Contain("TT");
        }

        [Test]
        public void GetTagNames_ArrayOfAtomic_ShouldContainExpectedTagNames()
        {
            var type = new ArrayType<Bool>(5);

            var members = type.GetTagNames().ToList();

            members.Should().Contain("[0]");
            members.Should().Contain("[1]");
            members.Should().Contain("[2]");
            members.Should().Contain("[3]");
            members.Should().Contain("[4]");
        }

        [Test]
        public void GetTagNames_ArrayOfComplex_ShouldContainExpectedTagNames()
        {
            var type = new ArrayType<String>(5);

            var members = type.GetTagNames().ToList();

            members.Should().Contain("[0]");
            members.Should().Contain("[0].LEN");
            members.Should().Contain("[0].DATA");
            members.Should().Contain("[1]");
            members.Should().Contain("[1].LEN");
            members.Should().Contain("[1].DATA");
            members.Should().Contain("[2]");
            members.Should().Contain("[2].LEN");
            members.Should().Contain("[2].DATA");
            members.Should().Contain("[3]");
            members.Should().Contain("[3].LEN");
            members.Should().Contain("[3].DATA");
            members.Should().Contain("[4]");
            members.Should().Contain("[4].LEN");
            members.Should().Contain("[4].DATA");
        }

        [Test]
        public void GetDependentTypes_Atomic_ShouldBeEmpty()
        {
            var type = new Int();

            var types = type.GetDependentTypes();

            types.Should().BeEmpty();
        }

        [Test]
        public void GetDependentTypes_Complex_ShouldHaveExpectedTypes()
        {
            var type = new Timer();

            var types = type.GetDependentTypes().ToList();

            types.Should().HaveCount(2);
            types.Should().Contain(t => t.Name == "BOOL");
            types.Should().Contain(t => t.Name == "DINT");
        }

        [Test]
        public void GetDependentTypes_Array_ShouldHaveExpectedTypes()
        {
            var type = new ArrayType<Timer>(3);

            var types = type.GetDependentTypes().ToList();

            types.Should().HaveCount(3);
            types.Should().Contain(t => t.Name == "TIMER");
            types.Should().Contain(t => t.Name == "BOOL");
            types.Should().Contain(t => t.Name == "DINT");
        }

        [Test]
        public void ContainsMember_Null_ShouldThrowArgumentNullException()
        {
            var type = new Timer();

            FluentActions.Invoking(() => type.HasMember(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ContainsMember_Atomic_ShouldBeFalse()
        {
            var type = new Bool();

            var result = type.HasMember("MemberName");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsMember_ComplexAndExists_ShouldBeTrue()
        {
            var type = new Timer();

            var result = type.HasMember("PRE");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsMember_ComplexAndDoesNotExist_ShouldBeFalse()
        {
            var type = new Timer();

            var result = type.HasMember("PRESET");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsMember_ArrayAndExists_ShouldBeTrue()
        {
            var type = new ArrayType<Bool>(5);

            var result = type.HasMember("[3]");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsMember_ArrayAndDoesNotExist_ShouldBeFalse()
        {
            var type = new ArrayType<Bool>(5);

            var result = type.HasMember("[5]");


            result.Should().BeFalse();
        }
        
        [Test]
        public void GetMembersTo_Null_ShouldThrowArgumentException()
        {
            var type = new Bool();

            FluentActions.Invoking(() => type.GetMembersTo(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetMembersTo_Atomic_ShouldBeEmpty()
        {
            var type = new Bool();

            var members = type.GetMembersTo("MemberName");

            members.Should().BeEmpty();
        }

        [Test]
        public void GetMembersTo_ComplexAndInvalidPath_ShouldBeEmpty()
        {
            var type = new Timer();

            var members = type.GetMembersTo("PRESET");

            members.Should().BeEmpty();
        }

        [Test]
        public void GetMembersTo_ComplexAndValidPath_ShouldHaveExpectedCount()
        {
            var type = new Timer();

            var members = type.GetMembersTo("PRE").ToList();

            members.Should().HaveCount(1);
            members.Should().Contain(m => m.Name == "PRE");
        }

        [Test]
        public void GetMembersTo_MyNestedType_ShouldHaveExpectedMembers()
        {
            var type = new MyNestedType();

            var members = type.GetMembersTo("Tmr.PRE").ToList();

            members.Should().HaveCount(2);
            members.Should().Contain(m => m.Name == "Tmr");
            members.Should().Contain(m => m.Name == "PRE");
        }

        [Test]
        public void GetMembersTo_MyNestedArrayMember_ShouldHaveExpectedMembers()
        {
            var type = new MyNestedType();

            var members = type.GetMembersTo("Counters[1].ACC").ToList();

            members.Should().HaveCount(3);
            members.Should().Contain(m => m.Name == "Counters");
            members.Should().Contain(m => m.Name == "[1]");
            members.Should().Contain(m => m.Name == "ACC");
        }
    }
    
    public class MyNestedType : ComplexType
    {
        public MyNestedType() : base(nameof(MyNestedType))
        {
        }

        public IMember<Bool> Indy = Member.Create<Bool>(nameof(Indy));
        public IMember<String> Str = Member.Create<String>(nameof(Str));
        public IMember<Timer> Tmr = Member.Create<Timer>(nameof(Tmr));
        public IMember<IArrayType<Counter>> Counters = Member.Create<Counter>(nameof(Counters), 5);
        
        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MyNestedType();
    }
    
    public class MyOtherNestedType : ComplexType
    {
        public MyOtherNestedType() : base(nameof(MyOtherNestedType))
        {
        }

        public IMember<Bool> Indy = Member.Create<Bool>(nameof(Indy));
        public IMember<String> Str = Member.Create<String>(nameof(Str));

        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MyNestedType();
    }
}