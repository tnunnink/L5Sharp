using FluentAssertions;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Tests.Gateway.PlcClientTests;

[TestFixture]
public class PlcBuilderTests
{
    [Test]
    public void Testing()
    {
        using var client = Plc
            .ConnectTo("10.10.100.13")
            .Slot(1)
            .UseNativeService()
            .WithOptions(o =>
            {
                o.Timeout = 10000;
                o.ThrowOn.Add(TagStatus.Timeout);
                o.ThrowOn.Add(TagStatus.NoResources);
            })
            .Build();

        client.Should().NotBeNull();
    }
}