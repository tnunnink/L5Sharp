using System.Net;
using FluentAssertions;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class PlcOptionsTests
{
    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var options = new PlcOptions();

        options.IP.Should().Be(IPAddress.Loopback.ToString());
        options.Slot.Should().Be(0);
        options.Timeout.Should().Be(5000);
        options.ReadInterval.Should().Be(1000);
        options.ThrowOn.Should().BeEmpty();
    }

    [Test]
    public void New_OverrideSettings_ShouldBeEpxected()
    {
        var options = new PlcOptions
        {
            IP = "10.10.10.10",
            Slot = 2,
            Timeout = 30000,
            ReadInterval = 100,
            ThrowOn = { TagStatus.BadData, TagStatus.BadConnection, TagStatus.Timeout }
        };

        options.IP.Should().Be("10.10.10.10");
        options.Slot.Should().Be(2);
        options.Timeout.Should().Be(30000);
        options.ReadInterval.Should().Be(100);
        options.ThrowOn.Should().HaveCount(3);
    }
}