using FluentAssertions;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Services;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Gateway.ServiceTests;

[TestFixture]
public class VirtualTagServiceTests
{
    private const string BasePath = "protocol=ab_eip&gateway=127.0.0.1&path=1,0&plc=controllogix";

    [Test]
    public void Create_ValidPath_ShouldReturnHandleAboveTen()
    {
        var service = VirtualTagService.Upload(Known.Simple);
        const string path = $"{BasePath}&name=TestDINT";

        var handle = service.Create(path, (_, _, _, _) => { }, IntPtr.Zero, 5000);

        handle.Should().BeGreaterThan(10);
    }

    [Test]
    public void Create_TwiceForSameTag_ShouldReturnSuccessiveHandles()
    {
        var service = VirtualTagService.Upload(Known.Simple);
        const string path = $"{BasePath}&name=TestDINT";

        var first = service.Create(path, (_, _, _, _) => { }, IntPtr.Zero, 5000);
        var second = service.Create(path, (_, _, _, _) => { }, IntPtr.Zero, 5000);

        first.Should().Be(11);
        second.Should().Be(12);
    }

    [Test]
    public async Task Status_ExistingHandle_ShouldReturnExpectedStatus()
    {
        var service = VirtualTagService.Upload(Known.Simple);
        const string path = $"{BasePath}&name=TestDINT";
        var handle = service.Create(path, (_, _, _, _) => { }, IntPtr.Zero, 5000);
        await Task.Delay(100);

        var status = service.Status(handle);

        status.AsStatus().Should().Be(TagStatus.Ok);
    }
}