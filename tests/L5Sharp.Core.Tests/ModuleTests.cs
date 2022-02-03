using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ModuleTests
    {
        [Test]
        public void New_ValidParameters_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-L83E");

            module.Should().NotBeNull();
        }
    }
}