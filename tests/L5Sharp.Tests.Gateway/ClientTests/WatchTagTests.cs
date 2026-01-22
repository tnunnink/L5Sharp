using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WatchTagTests : PlcTestBase
{
    [Test]
    public async Task WatchTag_ValidTypeAndName_ShouldReturnExpectedSubscription()
    {
        using var client = CreateClient();

        using var subscription = await client.WatchTag<DINT>("TestDint");

        subscription.IsActive.Should().BeTrue();
        subscription.HasErrors.Should().BeFalse();
        subscription.Status.Should().Be(TagStatus.Ok);
        subscription.LastUpdate.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        subscription.UpdateCount.Should().BeGreaterThan(0);
        subscription.Tags.Should().HaveCount(1);
        subscription.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WatchTag_ValidTypeAndName_ShouldHaveExpectedTagValue()
    {
        using var client = CreateClient();

        using var subscription = await client.WatchTag<DINT>("TestDint");

        var tag = subscription.GetTag("TestDint");
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task WatchTag_ValidTags_ShouldReturnExpectedValueAfterFewSeconds()
    {
        var tag = Tag.New<DINT>("TestDINT");
        using var client = CreateClient();

        using var subscription = await client.WatchTag(tag);
        
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task WatchTag_ConfigureOnChange_ShouldInvokeCallback()
    {
        // Use faster poll rate for testing
        using var client = CreateClient(o => o.PollRate = 100);
        var subscription = await client.WatchTag<TIMER>("TestTimer");
        var invoked = 0;

        subscription.OnChange(t =>
        {
            Console.WriteLine($"Tag: {t.TagName} | Value: {t.Value}");
            invoked++;
        });

        // Have to wait for the next poll rate.
        await Task.Delay(100);

        invoked.Should().BeGreaterThan(0);
    }
}