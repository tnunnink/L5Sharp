using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Exceptions;
using L5Sharp.L5X;
using L5Sharp.Repositories.Tests.Content;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Repositories.Tests
{
    [TestFixture]
    public class DataTypeRepositoryTests
    {
        [Test]
        public void Add_NullComponent_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new UserDefined("SimpleType", "This is a test type");

            FluentActions.Invoking(() => context.DataTypes().Add(component)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ValidComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new UserDefined("TestType", "This is a test type");

            context.DataTypes().Add(component);

            context.DataTypes().Any("TestType").Should().BeTrue();
        }
        
        [Test]
        public void Add_NestedUserDefined_ShouldOnlyContainParent()
        {
            var context = L5XContext.Load(Known.Test);

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
            context.DataTypes().Any("ChildType").Should().BeFalse();
        }
        
        [Test]
        public void Remove_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Remove(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_ValidElement_ShouldNoLongExist()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.DataTypes().Named("SimpleType");

            context.DataTypes().Remove(component);

            context.DataTypes().Any("SimpleType").Should().BeFalse();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Update(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Update_ExistingComponent_ShouldUpdateComponent()
        {
            var context = L5XContext.Load(Known.Test);

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

            var result = context.DataTypes().Named("SimpleType");

            result.Should().NotBeNull();
            result?.Name.Should().Be("SimpleType");
            result?.Description.Should().Be("This is a test type");
            result?.Members.Should().HaveCount(6);
            result?.Members.Should().Contain(m => m.Name == "M1");
            result?.Members.Should().Contain(m => m.Name == "M2");
            result?.Members.Should().Contain(m => m.Name == "M3");
            result?.Members.Should().Contain(m => m.Name == "M4");
            result?.Members.Should().Contain(m => m.Name == "M5");
            result?.Members.Should().Contain(m => m.Name == "M6");
        }

        [Test]
        public void Update_NonExistingComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new UserDefined("TestType", "This is a test type", new List<IMember<IDataType>>
            {
                Member.Create<DINT>("Member01"),
                Member.Create<STRING>("Member02"),
                Member.Create<STRING>("Member03")
            });

            context.DataTypes().Update(component);

            var result = context.DataTypes().Named("TestType");
            
            result.Should().NotBeNull();
            result?.Name.Should().Be("TestType");
            result?.Description.Should().Be("This is a test type");
            result?.Members.Should().HaveCount(3);
            result?.Members.Should().Contain(m => m.Name == "Member01");
            result?.Members.Should().Contain(m => m.Name == "Member02");
            result?.Members.Should().Contain(m => m.Name == "Member03");
        }
        
        [Test]
        public void Update_NestedUserDefined_ShouldContainBothTypes()
        {
            var context = L5XContext.Load(Known.Test);

            var child = new UserDefined("ChildType", "This is a child type", new List<IMember<IDataType>>
            {
                Member.Create<BOOL>("M1"),
                Member.Create<DINT>("M2"),
                Member.Create<REAL>("M3")
            });

            var parent = (IUserDefined)context.DataTypes().Named("ComplexType");
            parent?.Members.Add(Member.Create("Child", child));

            context.DataTypes().Update(parent);
            
            context.DataTypes().Any("ChildType").Should().BeTrue();

            var result = context.DataTypes().Named("ComplexType")?.Members.FirstOrDefault(m => m.Name == "Child");
            result.Should().NotBeNull();
        }
    }
}