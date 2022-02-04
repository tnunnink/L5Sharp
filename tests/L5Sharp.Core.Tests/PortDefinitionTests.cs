using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortDefinitionTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var port = new PortDefinition(1, "ICP", true);

            port.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpected()
        {
            var port = new PortDefinition(1, "ICP", true);

            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeTrue();
            port.DownstreamOnly.Should().BeFalse();
            port.Address.Should().BeEmpty();
            port.BusSize.Should().Be(0);
        }

        [Test]
        public void New_Overrides_ShouldHaveExpectedProperties()
        {
            var port = new PortDefinition(2, "ICP", false, "0", 17, true);

            port.Id.Should().Be(2);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeFalse();
            port.DownstreamOnly.Should().BeTrue();
            port.Address.Should().Be("0");
            port.BusSize.Should().Be(17);
        }
    }
}