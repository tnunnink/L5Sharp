using System;
using System.Net;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.L5X;
using L5Sharp.Repositories.Tests.Content;
using NUnit.Framework;

namespace L5Sharp.Repositories.Tests
{
    [TestFixture]
    public class ModuleRepositoryTests
    {
        [Test]
        public void Add_NullComponent_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules().Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new Module("Local", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            FluentActions.Invoking(() => context.Modules().Add(component)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ValidComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules().Add(component);

            context.Modules().Any("Test").Should().BeTrue();
        }
        
        [Test]
        public void Add_HasChildModules_ShouldAddAllModules()
        {
            var context = L5XContext.Load(Known.Test);

            var parent = new Module("Parent", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Bus.Backplane()?.Create("Child", "1756-IF8", 1);

            context.Modules().Add(parent);

            context.Modules().Any("Parent").Should().BeTrue();
            context.Modules().Any("Child").Should().BeTrue();
        }
        
        [Test]
        public void Remove_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules().Remove(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_ValidElement_ShouldNoLongExist()
        {
            var context = L5XContext.Load(Known.Test);
            
            var component = context.Modules().Named("Local");

            context.Modules().Remove(component);

            context.Modules().Any("Local").Should().BeFalse();
        }
        
        [Test]
        public void Remove_HasChildModules_ShouldRemoveAllChildren()
        {
            var context = L5XContext.Load(Known.Test);
            var parent = new Module("Test", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Bus.Backplane()?.Create("Child", "1756-IF8", 1);

            context.Modules().Update(parent);

            context.Modules().Any("Test").Should().BeTrue();
            context.Modules().Any("Child").Should().BeTrue();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules().Update(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Update_ExistingComponent_ShouldUpdateComponent()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new Module("Local", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules().Update(component);

            var result = context.Modules().Named("Local");

            result.Should().NotBeNull();
            result?.Name.Should().Be("Local");
        }

        [Test]
        public void Update_NonExistingComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.Test);

            var component = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules().Update(component);

            var result = context.Modules().Named("Test");
            
            result.Should().NotBeNull();
            result?.Name.Should().Be("Test");
        }
        
        [Test]
        public void Update_HasChildModules_ShouldAddAllModules()
        {
            var context = L5XContext.Load(Known.Test);

            var parent = new Module("Test", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Bus.Backplane()?.Create("Child", "1756-IF8", 1);

            context.Modules().Update(parent);

            context.Modules().Any("Test").Should().BeTrue();
            context.Modules().Any("Child").Should().BeTrue();
        }
    }
}