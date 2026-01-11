using FluentAssertions;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Services;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class PlcBuilderTests
{
    [Test]
    public void Testing()
    {
        using var client = Plc
            .ConnectTo("10.10.100.13")
            .Slot(1)
            .WithOptions(o =>
            {
                o.Timeout = 10000;
                o.ReadInterval = 500;
                o.ThrowOn.Add(TagStatus.Timeout);
                o.ThrowOn.Add(TagStatus.NoResources);
            })
            .UseTagService(() => VirtualTagService.Upload(Known.Test))
            .Build();

        client.Should().NotBeNull();
    }
}