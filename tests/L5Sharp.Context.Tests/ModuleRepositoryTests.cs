using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class ModuleRepositoryTests
    {
        [Test]
        public void Contains_ValidComponent_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Modules.Contains("Local");

            result.Should().BeTrue();
        }

        [Test]
        public void Contains_InvalidComponent_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Modules.Contains("Fake");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Modules.Contains(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Find_ComponentName_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Modules.Find("Local");

            component.Should().NotBeNull();
        }

        [Test]
        public void Find_ComponentName_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Modules.Find("Fake");

            component.Should().BeNull();
        }

        [Test]
        public void Find_Predicate_HasComponents_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Modules.Find(t => t.CatalogNumber == "1756-L83E");

            component.Should().NotBeNull();
        }

        [Test]
        public void DeepFind_ExistingModule_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var module = context.Modules.DeepFind("Local_Mod_1");

            module.Should().NotBeNull();
        }
        
        [Test]
        public void DeepFind_ExistingModule_ShouldHaveExpectedChildren()
        {
            var context = L5XContext.Load(Known.L5X);

            var module = context.Modules.DeepFind("Local_Mod_1");

            module?.Ports.Local()?.Bus?.Count.Should().Be(4);
        }

        [Test]
        public void FindAll_Predicate_HasComponents_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var components = context.Modules.FindAll(t => t.Vendor == 1).ToList();

            components.Should().NotBeEmpty();
            components.All(c => c.Vendor == Vendor.Rockwell).Should().BeTrue();
        }

        [Test]
        public void Get_Existing_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Modules.Get("Local");

            component.Name.Should().Be("Local");
            component.CatalogNumber.Should().Be(new CatalogNumber("1756-L83E"));
            component.Vendor.Should().Be(Vendor.Rockwell);
            component.ProductType.Should().Be(ProductType.Controller);
            component.ProductCode.Should().Be(166);
            component.Revision.Should().Be(new Revision(32, 11));
            component.ParentModule.Should().Be("Local");
            component.ParentPortId.Should().Be(1);
            component.Inhibited.Should().BeFalse();
            component.MajorFault.Should().BeTrue();
            component.Ports.Should().HaveCount(2);
        }

        [Test]
        public void Get_EachName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);
            
            var names = context.Modules.Names();

            foreach (var name in names)
            {
                var module = context.Modules.Get(name);
                module.Should().NotBeNull();
            }
        }

        [Test]
        public void Get_NonExistingType_ShouldThrowComponentNotFoundException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.Modules.Get("Fake")).Should()
                .Throw<ComponentNotFoundException>();
        }

        [Test]
        public void GetAll_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var components = context.Modules.GetAll().ToList();

            components.Should().NotBeEmpty();
        }

        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var names = context.Modules.Names().ToList();

            names.Should().NotBeEmpty();
        }

        [Test]
        public void Add_NullComponent_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.Modules.Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new Module("Local", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            FluentActions.Invoking(() => context.Modules.Add(component)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ValidComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules.Add(component);

            context.Modules.Contains("Test").Should().BeTrue();
        }
        
        [Test]
        public void Add_HasChildModules_ShouldAddAllModules()
        {
            var context = L5XContext.Load(Known.L5X);

            var parent = new Module("Parent", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Ports.Local()?.Bus?.New("Child", "1756-IF8", 1);

            context.Modules.Add(parent);

            context.Modules.Contains("Parent").Should().BeTrue();
            context.Modules.Contains("Child").Should().BeTrue();
        }
        
        [Test]
        public void Remove_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.Modules.Remove(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_ValidElement_ShouldNoLongExist()
        {
            var context = L5XContext.Load(Known.L5X);

            context.Modules.Remove("Local");

            context.Modules.Contains("Local").Should().BeFalse();
        }
        
        [Test]
        public void Remove_HasChildModules_ShouldRemoveAllChildren()
        {
            var context = L5XContext.Load(Known.L5X);
            var parent = new Module("Test", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Ports.Local()?.Bus?.New("Child", "1756-IF8", 1);

            context.Modules.Update(parent);

            context.Modules.Contains("Test").Should().BeTrue();
            context.Modules.Contains("Child").Should().BeTrue();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.L5X);

            FluentActions.Invoking(() => context.Modules.Update(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Update_ExistingComponent_ShouldUpdateComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new Module("Local", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules.Update(component);

            var result = context.Modules.Get("Local");

            result.Should().NotBeNull();
            result.Name.Should().Be("Local");
        }

        [Test]
        public void Update_NonExistingComponent_ShouldContainNewComponent()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));

            context.Modules.Update(component);

            var result = context.Modules.Get("Test");
            
            result.Should().NotBeNull();
            result.Name.Should().Be("Test");
        }
        
        [Test]
        public void Update_HasChildModules_ShouldAddAllModules()
        {
            var context = L5XContext.Load(Known.L5X);

            var parent = new Module("Test", "1756-EN2T", IPAddress.Parse("192.168.1.1"));
            parent.Ports.Local()?.Bus?.New("Child", "1756-IF8", 1);

            context.Modules.Update(parent);

            context.Modules.Contains("Test").Should().BeTrue();
            context.Modules.Contains("Child").Should().BeTrue();
        }
    }
}