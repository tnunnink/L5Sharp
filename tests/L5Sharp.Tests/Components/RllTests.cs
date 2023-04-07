using FluentAssertions;
using L5Sharp.Components;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class RllTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var rll = new Rll { new() };

            rll.Should().NotBeNull();
        }
    }
}