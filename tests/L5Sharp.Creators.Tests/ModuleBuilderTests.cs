using System;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Creators.Tests
{
    [TestFixture]
    public class ModuleBuilderTests
    {
        [Test]
        public void Build_ValidNameAndCatalog_ShouldNotBeNull()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .Create();

            module.Should().NotBeNull();
        }

        [Test]
        public void Build_WithDescription_ShouldHaveExpectedDescription()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithDescription("This is a test module")
                .Create();

            module.Should().NotBeNull();
            module.Description.Should().Be("This is a test module");
        }

        [Test]
        public void Build_WithValidRevision_ShouldHaveExpectedRevision()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithRevision(new Revision(11, 1))
                .Create();

            module.Revision.Should().Be(new Revision(11, 1));
        }

        [Test]
        public void Build_WithInvalidRevision_ShouldThrowInvalidOperationException()
        {
            var builder = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithRevision(new Revision(123, 456));

            FluentActions.Invoking(() => builder.Create()).Should().Throw<InvalidOperationException>();
        }
    }
}