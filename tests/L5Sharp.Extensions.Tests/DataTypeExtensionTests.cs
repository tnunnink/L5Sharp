using System.Collections.Generic;
using System.Linq;
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
            var type = new UserDefined("UserType", members: new List<IMember<IDataType>>()
            {
                Member.Create<Bool>("Test")
            });

            var member = type.GetMember("Test");

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMemberNames_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = UserDefined.Create("TypeWithMembers", members: new List<IMember<IDataType>>
            {
                Member.Create<Dint>("Member01")
            });

            var names = type.GetDeepMemberNames();

            names.Should().NotBeEmpty();
        }

        [Test]
        public void GetMemberNames_TypeWithArrayMember_ShouldHaveAllElementNames()
        {
            var type = UserDefined.Create("TypeWithMembers", members: new List<IMember<IDataType>>
            {
                Member.Create<Dint>("Member01", new Dimensions(5))
            });

            var names = type.GetDeepMemberNames().ToList();

            names.Should().NotBeEmpty();
            names.Should().HaveCount(6);
            names.Should().Contain("Member01");
            names.Should().Contain("Member01[0]");
            names.Should().Contain("Member01[1]");
            names.Should().Contain("Member01[2]");
            names.Should().Contain("Member01[3]");
            names.Should().Contain("Member01[4]");
        }

        
        [Test]
        public void GetDependentTypes_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = new Timer();

            type.GetDependentTypes().Should().NotBeEmpty();
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
            var target = new UserDefined("TargetType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("SourceType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member05", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Dint()),
                Member.Create("Member03", new Timer()),
                Member.Create("Member04", new Dint())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member02", new Dint()),
                Member.Create("Member01", new Bool()),
                Member.Create("Member03", new Timer()),
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                member,
                Member.Create("Member02", new Int()),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
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
            var nested = new UserDefined("NestedType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member05", new Sint()),
                Member.Create("Member06", new Real()),
                Member.Create("Member07", new Message())
            });
            
            var target = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", nested),
                Member.Create("Member03", new Timer())
            });
            
            var source = new UserDefined("UserType", members: new List<IMember<IDataType>>
            {
                Member.Create("Member01", new Bool()),
                Member.Create("Member02", nested),
                Member.Create("Member03", new Timer())
            });

            target.StructureEquals(source).Should().BeTrue();
        }
    }
}