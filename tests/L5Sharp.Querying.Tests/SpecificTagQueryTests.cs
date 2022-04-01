using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5Sharp.Querying.Tests.Content;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class SpecificTagQueryTests
    {
        [Test]
        public void Where_DimensionsIsEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.Tags().Where(t => t.Dimensions.IsEmpty);

            component.Should().NotBeEmpty();
        }

        [Test]
        public void Where_NameContainsString_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var components = context.Tags().Where(t => t.Name.Contains("Simple")).ToList();

            components.Should().NotBeEmpty();
            components.All(c => c.Name.Contains("Simple")).Should().BeTrue();
        }
    }
}