using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class ReadTagTests : PlcTestBase
{
    [Test]
    public async Task ReadTag_ValidNameAndType_ShouldGetSuccessfulResultAndExpectedTagData()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<DINT>("TestDint");

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ControllerTagDint_ShouldGetSuccessfulResult()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ProgramTagDint_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("Program:MainProgram.LocalDint");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ControllerTagTimer_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("TestTimer");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task ReadTag_ProgramTagTimer_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("Program:MainProgram.LocalTimer");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag["PRE"].Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_SimpleAtomicInLoopToEnsureCachedHandle_ShouldReadInExpectedTime()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");

        for (var i = 0; i < 10; i++)
        {
            var result = await client.ReadTag(tag);
            result.Success.Should().BeTrue();
            Console.WriteLine($"{result.Timestamp} | {tag.Value}");
        }
    }

    [Test]
    public async Task ReadTag_AlarmType_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = CreateClient();
        var tag = Tag.New<ALARM_ANALOG>("TestAlarm");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Value.As<ALARM_ANALOG>().HHLimit.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_TagWithNoData_ShouldThrowException()
    {
        using var client = CreateClient();
        var tag = new Tag { Name = "TestDint" };

        // ReSharper disable once AccessToDisposedClosure just for testing purposes
        await FluentActions.Awaiting(() => client.ReadTag(tag))
            .Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task ReadTag_TagDoesNotExist_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("Fake");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeFalse();
        result.Status.Should().Be(TagStatus.NotFound);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().HaveCount(1);
        result.HasError(TagStatus.NotFound).Should().BeTrue();
    }

    [Test]
    public async Task ReadTags_LargeSetOfAtomicTag_ShouldReturnSuccessAndNotTakeForever()
    {
        using var client = CreateClient();
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        var result = await client.ReadTags(tags);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Duration.Should().BeLessThan(TimeSpan.FromSeconds(2));
        result.Tags.Should().HaveCount(1000);
        result.Errors.Should().BeEmpty();
    }
}