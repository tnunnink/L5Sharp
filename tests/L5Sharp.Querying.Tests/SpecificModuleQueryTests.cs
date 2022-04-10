using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class SpecificModuleQueryTests
    {
        [Test]
        public void Local_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.Modules().Local();

            component.Should().NotBeNull();
        }

        [Test]
        public void WithParent_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules().WithParent(null!).Select()).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void WithParent_NonExistingParent_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var modules = context.Modules().WithParent("fake").Select().ToList();

            modules.Should().BeEmpty();
        }

        [Test]
        public void WithParent_ValidParent_ShouldAllHaveSpecifiedParent()
        {
            var context = L5XContext.Load(Known.Test);

            var modules = context.Modules().WithParent("Local_Mod_1").Select().ToList();

            modules.Should().NotBeEmpty();
            modules.Should().AllSatisfy(m => m.ParentModule.Should().Be("Local_Mod_1"));
        }

        [Test]
        public void Named_Existing_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.Modules().Named("Local");

            component?.Name.Should().Be("Local");
            component?.CatalogNumber.Should().Be(new CatalogNumber("1756-L83E"));
            component?.Vendor.Should().Be(Vendor.Rockwell);
            component?.ProductType.Should().Be(ProductType.Controller);
            component?.ProductCode.Should().Be(166);
            component?.Revision.Should().Be(new Revision(32, 11));
            component?.ParentModule.Should().Be("Local");
            component?.ParentPortId.Should().Be(1);
            component?.Inhibited.Should().BeFalse();
            component?.MajorFault.Should().BeTrue();
            component?.Ports.Should().HaveCount(2);
        }

        [Test]
        public void First_OnCatalogNumber_ShouldHaveExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.Modules().First(t => t.CatalogNumber == "1756-L83E");

            component.CatalogNumber.Should().Be(new CatalogNumber("1756-L83E"));
        }

        [Test]
        public void Where_VendorEqualsOne_ShouldHaveExpectedVendor()
        {
            var context = L5XContext.Load(Known.Test);

            var components = context.Modules().Where(t => t.Vendor == 1).ToList();

            components.All(c => c.Vendor == Vendor.Rockwell).Should().BeTrue();
        }
    }
}