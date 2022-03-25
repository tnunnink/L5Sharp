using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class TagRepositoryTests
    {
        [Test]
        public void All_WhenCalled_shouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var tags = context.Tags().All().ToList();

            tags.Should().NotBeEmpty();
        }

        [Test]
        public void Any_ValidComponent_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Tags().Any("SimpleDint");

            result.Should().BeTrue();
        }

        [Test]
        public void Any_InvalidComponent_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Tags().Any("FakeTag");

            result.Should().BeFalse();
        }

        [Test]
        public void Any_Null_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.L5X);

            var result = context.Tags().Any(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void Named_ComponentName_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Tags().Named("SimpleDint");

            component.Should().NotBeNull();
        }

        [Test]
        public void Named_ComponentName_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Tags().Named("FakeType");

            component.Should().BeNull();
        }

        [Test]
        public void Where_DimensionsIsEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var component = context.Tags().Where(t => t.Dimensions.IsEmpty);

            component.Should().NotBeEmpty();
        }

        [Test]
        public void Where_NameContainsString_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.L5X);

            var components = context.Tags().Where(t => t.Name.Contains("Simple")).ToList();

            components.Should().NotBeEmpty();
            components.All(c => c.Name.Contains("Simple")).Should().BeTrue();
        }

        [Test]
        public void GetAutoSamplerTypeTest()
        {
            var context = L5XContext.Load(Known.Template);

            var tag = context.Tags().Named("Auto_Sampler_01");

            tag.Should().NotBeNull();
        }
    }
}