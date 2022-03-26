using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class ContextCreationTests
    {
        [Test]
        public void Create_ValidComponent_ShouldNotBeNull()
        {
            var component = new UserDefined("Test", "This is a test");

            var context = L5XContext.Create(component);

            context.Should().NotBeNull();
        }
    }
}