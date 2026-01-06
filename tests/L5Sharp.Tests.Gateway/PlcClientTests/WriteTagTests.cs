using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.PlcClientTests;

[TestFixture]
public class WriteTagTests
{
    private const string TestIp = "10.11.19.204";
    private const int TestSlot = 1;

    [Test]
    public async Task WriteTag_TestDintTag_ShouldGetSuccessfulResult()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<DINT>("TestDint");
        tag.Value = 123;

        var response = await client.WriteTag(tag);

        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }
    
    [Test]
    public async Task WriteTag_TimerType_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<TIMER>("TestTimer");
        tag["PRE"].Value = 10000;
        tag["DN"].Value = 1;

        var response = await client.WriteTag(tag);

        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }
}