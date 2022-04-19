using FluentAssertions;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class L5XContextTests
    {
        [Test]
        public void Load_ValidL5X_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            context.Should().NotBeNull();
        }
    }
}