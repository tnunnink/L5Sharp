using FluentAssertions;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Services;

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
            .UseTagService(() => VirtualTagService.Upload(@"C:\users\username\projects\MyProject.L5X"))
            .Build();

        client.Should().NotBeNull();
    }
}