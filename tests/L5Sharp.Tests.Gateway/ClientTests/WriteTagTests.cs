using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WriteTagTests : PlcTestBase
{
    [Test]
    public async Task WriteTag_ExistingNameAndValidType_ShouldReturnSuccess()
    {
        using var client = CreateClient();

        var response = await client.WriteTag<TIMER>("TestTimer", d =>
        {
            d.PRE = 12345;
            d.ACC = 123;
            d.EN = true;
        });

        response.Success.Should().BeTrue();
        response.Result.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_DintType_ShouldGetSuccessfulResult()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");
        tag.Value = 123;

        var response = await client.WriteTag(tag);

        response.Success.Should().BeTrue();
        response.Result.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_TimerType_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("TestTimer");
        tag["PRE"].Value = 10000;
        tag["DN"].Value = 1;

        var response = await client.WriteTag(tag);

        response.Success.Should().BeTrue();
        response.Result.Should().Be(TagStatus.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }
}