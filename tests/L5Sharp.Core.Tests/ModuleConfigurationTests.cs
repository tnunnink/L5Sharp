using System.Net;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ModuleConfigurationTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var configuration = new ModuleConfiguration();

            configuration.Should().NotBeNull();
        }

        [Test]
        public void New_WhenCalled_ShouldHaveExpected()
        {
            var configuration = new ModuleConfiguration();

            configuration.Description.Should().BeEmpty();
            configuration.Inhibited.Should().BeFalse();
            configuration.MajorFault.Should().BeFalse();
            configuration.SafetyEnabled.Should().BeFalse();
            configuration.Keying.Should().Be(ElectronicKeying.CompatibleModule);
            configuration.Revision.Should().BeNull();
            configuration.Slot.Should().Be(0);
            configuration.IP.Should().Be(IPAddress.Any);
            configuration.ChassisSize.Should().Be(0);
        }

        [Test] public void Configure_ShouldHaveExpectedProperties()
        {
            var configuration = new ModuleConfiguration
            {
                Description = "This is a test",
                ChassisSize = 10,
                Keying = ElectronicKeying.Custom,
                Slot = 1,
                IP = IPAddress.Parse("192.168.1.1"),
                Inhibited = true,
                MajorFault = true,
                SafetyEnabled = true,
                Revision = new Revision(1, 1),
            };

            configuration.Description.Should().Be("This is a test");
            configuration.ChassisSize.Should().Be(10);
            configuration.Keying.Should().Be(ElectronicKeying.Custom);
            configuration.Slot.Should().Be(1);
            configuration.IP.Should().Be(IPAddress.Parse("192.168.1.1"));
            configuration.Inhibited.Should().BeTrue();
            configuration.SafetyEnabled.Should().BeTrue();
            configuration.MajorFault.Should().BeTrue();
            configuration.Revision.Should().Be(new Revision(1, 1));
        }
    }
}