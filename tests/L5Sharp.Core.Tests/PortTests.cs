using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var port = new Port(1, "ICP");

            port.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpected()
        {
            var port = new Port(1, "ICP");

            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeTrue();
            port.Address.Should().BeEmpty();
            port.ModuleName.Should().BeEmpty();
            port.Bus.Should().BeNull();
        }

        [Test]
        public void New_Overrides_ShouldHaveExpectedProperties()
        {
            var port = new Port(2, "Ethernet", false, "192.168.1.2", 0, "Local");

            port.Id.Should().Be(2);
            port.Type.Should().Be("Ethernet");
            port.Address.Should().Be("192.168.1.2");
            port.Upstream.Should().Be(false);
            port.ModuleName.Should().Be("Local");
            port.Bus.Should().NotBeNull();
        }
    }
}