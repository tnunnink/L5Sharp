using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Extensions.Tests
{
    [TestFixture]
    public class DataTypeExtensionTests
    {
        [Test]
        public void IsValueType_AtomicType_ShouldBeTrue()
        {
            var type = new Dint();

            type.IsValueType().Should().BeTrue();
        }

        [Test]
        public void IsValueType_Predefined_ShouldBeFalse()
        {
            var type = new Timer();

            type.IsValueType().Should().BeFalse();
        }

        [Test]
        public void AreSameClass_AreSame_ShouldBeTrue()
        {
            var type1 = new Counter();
            var type2 = new Timer();

            type1.AreSameClass(type2).Should().BeTrue();
        }
        
        [Test]
        public void AreSameClass_AreNotSame_ShouldBeFalse()
        {
            var type1 = new Counter();
            var type2 = new Int();

            type1.AreSameClass(type2).Should().BeFalse();
        }

        [Test]
        public void GetMember_Predefined_ShouldNotBeNull()
        {
            var type = new Timer();

            var member = type.GetMember("PRE");

            member.Should().NotBeNull();
        }
        
        [Test]
        public void GetMember_UserDefined_ShouldNotBeNull()
        {
            var type = new DataType("UserType", new List<IMember<IDataType>>()
            {
                Member.Create<Bool>("Test")
            });

            var member = type.GetMember("Test");

            member.Should().NotBeNull();
        }
        
        [Test]
        public void GetDependentTypes_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = new Timer();

            type.GetDependentTypes().Should().NotBeEmpty();
        }

        [Test]
        public void GetDependentTypes_TypeWithNoMembers_ShouldBeEmpty()
        {
            var type = new Undefined();

            type.GetDependentTypes().Should().BeEmpty();
        }

        [Test]
        public void StructureEquals_SameTypeDifferentReference_ShouldBeTrue()
        {
            var target = new Timer();
            var source = new Timer();

            target.StructureEquals(source).Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_SameReference_ShouldBeTrue()
        {
            var target = new Timer();

            target.StructureEquals(target).Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_Null_ShouldBeFalse()
        {
            var target = new Timer();

            target.StructureEquals(null).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_AtomicSource_ShouldBeFalse()
        {
            var target = new Timer();
            var source = new Bool();
            
            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_AtomicTarget_ShouldBeFalse()
        {
            var target = new Bool();
            var source = new Counter();
            
            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_Atomics_ShouldBeTrue()
        {
            var target = new Bool();
            var source = new Bool();
            
            target.StructureEquals(source).Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_DifferentPredefined_ShouldBeFalse()
        {
            var target = new Message();
            var source = new Counter();
            
            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedDifferentNamesSameMembers_ShouldBeFalse()
        {
            var target = new DataType("TargetType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("SourceType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedDifferentType_ShouldBeFalse()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedDifferentMemberNames_ShouldBeFalse()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member05", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member05", new Int()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedDifferentDimensions_ShouldBeFalse()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool(), new Dimensions(2)),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedDifferentNumberOfMembers_ShouldBeFalse()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer()),
                Member.Create("Member04", new Dint())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedMembersDifferentOrder_ShouldBeFalse()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member02", new Dint()),
                Member.Create("Member01", new Bool()),
                Member.Create("Member03", new Timer()),
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeFalse();
        }
        
        [Test]
        public void StructureEquals_UserDefinedSame_ShouldBeTrue()
        {
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_SameMemberReference_ShouldBeTrue()
        {
            var member = Member.Create("Member01", new Bool());
            
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                member,
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                member,
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeTrue();
        }
        
        [Test]
        public void StructureEquals_UserDefinedNestedUserDefinedAreAllSame_ShouldBeTrue()
        {
            var nested = new DataType("NestedType", new List<IMember<IDataType>>
            {
                Member.Create("Member05", new Sint()),
                Member.Create("Member06", new Real()),
                Member.Create("Member07", new Message())
            });
            
            var target = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", nested),
                Member.Create("Member03", new Timer())
            });
            
            var source = new DataType("UserType", new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", nested),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeTrue();
        }
    }
}