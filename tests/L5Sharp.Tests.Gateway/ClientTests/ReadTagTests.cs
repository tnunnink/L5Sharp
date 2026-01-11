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

        var response = await client.ReadTag<DINT>("TestDint");

        response.Success.Should().BeTrue();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
        response.Tags.First().Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ControllerTagDint_ShouldGetSuccessfulResult()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_ProgramTagDint_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var program = new Program("MainProgram");
        var tag = Tag.New<DINT>("LocalDint");
        program.Tags.Add(tag); //This should get the correct scope for the tag.

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task ReadTag_SimpleAtomicInLoopToEnsureCachedHandle_ShouldReadInExpectedTime()
    {
        using var client = CreateClient();
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
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("TestTimer");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeTrue();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().BeEmpty();
        tag.Value.As<TIMER>().PRE.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_TagWithNoData_ShouldReturnNoDataResponse()
    {
        using var client = CreateClient();
        var tag = new Tag { Name = "TestDint" };

        var response = await client.ReadTag(tag);

        response.Success.Should().BeFalse();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().Be(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().HaveCount(1);
        response.HasError(TagStatus.NoData).Should().BeTrue();
    }

    [Test]
    public async Task ReadTag_TagDoesNotExist_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("Fake");

        var response = await client.ReadTag(tag);

        response.Success.Should().BeFalse();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Tags.Should().HaveCount(1);
        response.Errors.Should().HaveCount(1);
        response.HasError(TagStatus.NotFound).Should().BeTrue();
    }

    [Test]
    public async Task ReadTags_LargeSetOfAtomicTag_ShouldReturnSuccessAndNotTakeForever()
    {
        using var client = CreateClient();
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        var response = await client.ReadTags(tags);

        response.Success.Should().BeTrue();
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        response.Duration.Should().BeLessThan(TimeSpan.FromSeconds(1));
        response.Tags.Should().HaveCount(1000);
        response.Errors.Should().BeEmpty();
    }
}