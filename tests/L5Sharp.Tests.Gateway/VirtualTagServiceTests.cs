using FluentAssertions;
using L5Sharp.Gateway.Common;
using L5Sharp.Gateway.Services;
using L5Sharp.Samples;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class VirtualTagServiceTests
{
    private const string BasePath = "protocol=ab_eip&gateway=127.0.0.1&path=1,0&plc=controllogix";
    private const int Timeout = 5000;
    private static readonly Action<int, int, int, IntPtr> EmptyCallback = (_, _, _, _) => { };

    #region CreateTests

    [Test]
    [TestCase("TestBool")]
    [TestCase("TestDint")]
    [TestCase("TestTimer")]
    public void Create_ValidPath_ShouldReturnHandleAboveTen(string tagName)
    {
        var path = $"{BasePath}&name={tagName}";
        var service = VirtualTagService.Upload(Known.Simple);

        var handle = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        handle.Should().BeGreaterThan(10);
    }

    [Test]
    public void Create_ValidPath_ShouldBeAbleToGetStatus()
    {
        const string path = $"{BasePath}&name=TestDint";
        var service = VirtualTagService.Upload(Known.Simple);

        var handle = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        var status = service.Status(handle).AsStatus();
        status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public void Create_FakeTagName_ShouldReturnNotFound()
    {
        const string path = $"{BasePath}&name=FakeTag";
        var service = VirtualTagService.Upload(Known.Simple);

        var handle = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        handle.AsStatus().Should().Be(TagStatus.NotFound);
    }

    [Test]
    public void Create_WithCallback_ShouldGetInvokedExpectedNumberOfTimes()
    {
        const string path = $"{BasePath}&name=TestDint";
        var service = VirtualTagService.Upload(Known.Simple);
        var count = 0;

        service.Create(path, Callback, IntPtr.Zero, Timeout);

        count.Should().Be(2);
        return;

        void Callback(int handle, int eventId, int statusId, IntPtr userData)
        {
            count++;
        }
    }

    [Test]
    public void Create_TwiceForSameTag_ShouldReturnSuccessiveHandles()
    {
        const string path = $"{BasePath}&name=TestDint";
        var service = VirtualTagService.Upload(Known.Simple);

        var first = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);
        var second = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        first.Should().Be(11);
        second.Should().Be(12);
    }

    #endregion

    #region StatusTests

    [Test]
    public void Status_ExistingHandle_ShouldReturnExpectedStatus()
    {
        const string path = $"{BasePath}&name=TestDint";
        var service = VirtualTagService.Upload(Known.Simple);
        var handle = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        var status = service.Status(handle);

        status.AsStatus().Should().Be(TagStatus.Ok);
    }

    #endregion

    #region ReadTests

    [Test]
    public void Read_ExistingTagWithData_ShouldReturnOk()
    {
        const string path = $"{BasePath}&name=TestDint";
        var service = VirtualTagService.Upload(Known.Simple);
        var handle = service.Create(path, EmptyCallback, IntPtr.Zero, Timeout);

        var status = service.Read(handle, 1000);

        status.AsStatus().Should().Be(TagStatus.Ok);
    }

    #endregion
}