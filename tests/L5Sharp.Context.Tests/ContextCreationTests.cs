using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class ContextCreationTests
    {
        [Test]
        public void Create_ValidComponent_ShouldNotBeNull()
        {
            var controller = new Controller("Test", "1756-L83E");

            var context = L5XContext.Create(controller);

            context.Should().NotBeNull();
        }
    }
}