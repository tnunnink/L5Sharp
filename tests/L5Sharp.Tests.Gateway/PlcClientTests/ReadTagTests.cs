using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.PlcClientTests;

[TestFixture]
public class ReadTagTests
{
    private const string TestIp = "10.11.19.204";
    private const int TestSlot = 1;

    [Test]
    public async Task ReadTag_ControllerTagDint_ShouldGetSuccessfulResult()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<DINT>("TestDint");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ProgramTagDint_ShouldReturnNotFoundResponse()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var program = new Program("MainProgram");
        var tag = Tag.New<DINT>("LocalDint");
        program.Tags.Add(tag); //This should get the correct scope for the tag.

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task ReadTag_SimpleAtomicInLoopToEnsureCachedHandle_ShouldReadInExpectedTime()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<DINT>("TestDint");

        for (var i = 0; i < 10; i++)
        {
            var response = await client.ReadTag(tag);
            response.Success.Should().BeTrue();
            Console.WriteLine($"{response.Timestamp} | {tag.Value}");
        }
    }
    
    [Test]
    public async Task ReadTag_TimerType_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<TIMER>("TestTimer");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(5);
        response.Errors.Should().BeEmpty();
        tag.Value.As<TIMER>().PRE.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_TagWithNoData_ShouldReturnNoDataResponse()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = new Tag { Name = "TestDint" };

        var response = await client.ReadTag(tag);

        response.Success.Should().BeFalse();
        response.Status.Should().Be(TagStatus.NoData);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().Be(TimeSpan.Zero);
        response.Tags.Should().BeEmpty();
        response.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task ReadTag_TagDoesNotExist_ShouldReturnNotFoundResponse()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<DINT>("Fake");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeFalse();
        response.Status.Should().Be(TagStatus.NotFound);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task ReadTags_LargeSetOfAtomicTag_ShouldReturnSuccessAndNotTakeForever()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        var response = await client.ReadTags(tags);
        
        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Duration.Should().BeLessThan(TimeSpan.FromSeconds(3));
        response.Tags.Should().HaveCount(1000);
        response.Errors.Should().BeEmpty();
    }
}