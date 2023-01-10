using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class ModuleQueryTests
    {
        [Test]
        public void WithParent_Null_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules(q => q.WithParent(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void WithParent_NonExistingParent_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.Modules(q => q.WithParent("Fake")).ToList();

            results.Should().BeEmpty();
        }

        [Test]
        public void WithParent_ValidParent_ShouldAllHaveSpecifiedParent()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.Modules(q => q.WithParent("Local_Mod_1")).ToList();

            results.Should().NotBeEmpty();
            results.Should().AllSatisfy(m => m.ParentModule.Should().Be("Local_Mod_1"));
        }

        [Test]
        public void WithCatalog_NullPredicate_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.Modules(q => q.WithCatalog(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void WithCatalog_ExistingCatalog_ShouldAllSatisfyQuery()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.Modules(q => q.WithCatalog(c => c.Equals("1756-EN2T"))).ToList();

            results.Should().NotBeEmpty();
            results.Should().AllSatisfy(m => m.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T")));
        }
        
        [Test]
        public void WithCatalog_CatalogContains_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.Modules(q => q.WithCatalog(c => c.ToString().Contains("5094"))).ToList();

            results.Should().NotBeEmpty();
            results.Should().AllSatisfy(m => m.CatalogNumber.ToString().Should().StartWith("5094"));
        }
    }
}