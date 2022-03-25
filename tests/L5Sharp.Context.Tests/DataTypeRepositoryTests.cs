using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class DataTypeRepositoryTests
    {
        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var components = context.DataTypes().All().ToList();
            
            components.Should().NotBeNull();
            components.Should().NotBeEmpty();
        }

        [Test]
        public void Any_ValidComponent_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.DataTypes().Any("SimpleType");

            result.Should().BeTrue();
        }

        [Test]
        public void Any_InvalidComponent_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.DataTypes().Any("FakeType");

            result.Should().BeFalse();
        }

        [Test]
        public void Any_Null_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.DataTypes().Any(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Find_ComponentName_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.DataTypes().First("SimpleType");

            component.Should().NotBeNull();
        }

        [Test]
        public void Find_ComponentName_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.DataTypes().First("FakeType");

            component.Should().BeNull();
        }

        [Test]
        public void Find_Predicate_HasComponents_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.DataTypes().First(t => t.Family == DataTypeFamily.String);

            component.Should().NotBeNull();
        }

        [Test]
        public void FindAll_Predicate_HasComponents_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var components = context.DataTypes().Where(t => !string.IsNullOrEmpty(t.Description)).ToList();

            components.Should().NotBeEmpty();
            components.All(c => !string.IsNullOrEmpty(c.Description)).Should().BeTrue();
        }

        [Test]
        public void Get_ExistingType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.DataTypes().Single("SimpleType");

            component.Name.Should().Be("SimpleType");
            component.Description.Should()
                .Be("This is a test data type that Any simple atomic types with an updated description");
            component.Class.Should().Be(DataTypeClass.User);
            component.Family.Should().Be(DataTypeFamily.None);
            component.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Get_NonExistingType_ShouldThrowComponentNotFoundException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.DataTypes().Single("FakeType")).Should()
                .Throw<ComponentNotFoundException>();
        }

        [Test]
        public void Get_ComplexType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.DataTypes().Single("ComplexType");

            component.Name.Should().Be("ComplexType");
            component.Description.Should().Be("Test data type with more complex members");
            component.Class.Should().Be(DataTypeClass.User);
            component.Family.Should().Be(DataTypeFamily.None);
            component.Members.Should().NotBeEmpty();

            var simple = component.Members.FirstOrDefault(m => m.Name == "SimpleMember");
            simple?.Name.Should().Be("SimpleMember");
            simple?.DataType.Should().BeOfType<UserDefined>();
            simple?.DataType.As<IUserDefined>().Members.Should().NotBeEmpty();
        }

        [Test]
        public void Add_NullComponent_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.DataTypes().Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new UserDefined("SimpleType", "This is a test type");

            FluentActions.Invoking(() => context.DataTypes().Add(component)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ValidComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new UserDefined("TestType", "This is a test type");

            context.DataTypes().Add(component);

            context.DataTypes().Any("TestType").Should().BeTrue();
        }
        
        [Test]
        public void Add_NestedUserDefined_ShouldContainBothTypes()
        {
            var context = L5XContext.Load(Known.L5X);

            var child = new UserDefined("ChildType", "This is a child type", new List<IMember<IDataType>>
            {
                Member.Create<BOOL>("M1"),
                Member.Create<DINT>("M2"),
                Member.Create<REAL>("M3")
            });
            
            var parent = new UserDefined("ParentType", "This is a parent type", new List<IMember<IDataType>>
            {
                Member.Create("Child", child)
            });

            context.DataTypes().Add(parent);

            context.DataTypes().Any("ParentType").Should().BeTrue();
            context.DataTypes().Any("ChildType").Should().BeTrue();
        }
        
        [Test]
        public void Remove_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.DataTypes().Remove(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_ValidElement_ShouldNoLongExist()
        {
            var context = L5XContext.Load(Known.L5X);

            context.DataTypes().Remove("SimpleType");

            context.DataTypes().Any("SimpleType").Should().BeFalse();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.DataTypes().Update(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Update_ExistingComponent_ShouldUpdateComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new UserDefined("SimpleType", "This is a test type", new List<IMember<IDataType>>
            {
                Member.Create<BOOL>("M1"),
                Member.Create<SINT>("M2"),
                Member.Create<INT>("M3"),
                Member.Create<DINT>("M4"),
                Member.Create<LINT>("M5"),
                Member.Create<REAL>("M6")
            });

            context.DataTypes().Update(component);

            var result = context.DataTypes().Single("SimpleType");

            result.Should().NotBeNull();
            result.Name.Should().Be("SimpleType");
            result.Description.Should().Be("This is a test type");
            result.Members.Should().HaveCount(6);
            result.Members.Should().Contain(m => m.Name == "M1");
            result.Members.Should().Contain(m => m.Name == "M2");
            result.Members.Should().Contain(m => m.Name == "M3");
            result.Members.Should().Contain(m => m.Name == "M4");
            result.Members.Should().Contain(m => m.Name == "M5");
            result.Members.Should().Contain(m => m.Name == "M6");
        }

        [Test]
        public void Update_NonExistingComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new UserDefined("TestType", "This is a test type", new List<IMember<IDataType>>
            {
                Member.Create<DINT>("Member01"),
                Member.Create<STRING>("Member02"),
                Member.Create<STRING>("Member03")
            });

            context.DataTypes().Update(component);

            var result = context.DataTypes().Single("TestType");
            
            result.Should().NotBeNull();
            result.Name.Should().Be("TestType");
            result.Description.Should().Be("This is a test type");
            result.Members.Should().HaveCount(3);
            result.Members.Should().Contain(m => m.Name == "Member01");
            result.Members.Should().Contain(m => m.Name == "Member02");
            result.Members.Should().Contain(m => m.Name == "Member03");
        }
        
        [Test]
        public void Update_NestedUserDefined_ShouldContainBothTypes()
        {
            var context = L5XContext.Load(Known.L5X);

            var child = new UserDefined("ChildType", "This is a child type", new List<IMember<IDataType>>
            {
                Member.Create<BOOL>("M1"),
                Member.Create<DINT>("M2"),
                Member.Create<REAL>("M3")
            });

            var parent = (IUserDefined)context.DataTypes().Single("ComplexType");
            parent.Members.Add(Member.Create("Child", child));

            context.DataTypes().Update(parent);
            
            context.DataTypes().Any("ChildType").Should().BeTrue();

            var result = context.DataTypes().Single("ComplexType").Members.FirstOrDefault(m => m.Name == "Child");
            result.Should().NotBeNull();
        }
    }
}