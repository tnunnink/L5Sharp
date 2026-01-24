using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class MonitorTagTests : PlcTestBase
{
    [Test]
    public async Task MonitorTag_ValidTypeAndName_ShouldReturnExpectedSubscription()
    {
        using var client = CreateClient();

        using var monitor = await client.MonitorTag<DINT>("TestDint");

        monitor.IsActive.Should().BeTrue();
        monitor.HasErrors.Should().BeFalse();
        monitor.Status.Should().Be(TagStatus.Ok);
        monitor.Timestamp.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        monitor.Rate.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
        monitor.Updates.Should().BeGreaterThan(0);
        monitor.Tags.Should().HaveCount(1);
        monitor.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task MonitorTag_ValidTypeAndName_ShouldHaveExpectedTagValue()
    {
        using var client = CreateClient();

        using var monitor = await client.MonitorTag<DINT>("TestDint");

        var tag = monitor.Tags.First(t => t.TagName == "TestDint");
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task MonitorTag_ValidTags_ShouldReturnExpectedValueAfterFewSeconds()
    {
        var tag = Tag.New<DINT>("TestDINT");
        using var client = CreateClient();

        using var monitor = await client.MonitorTag(tag);

        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task MonitorTag_ProgramTagTimer_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("Program:MainProgram.LocalTimer");

        using var monitor = await client.MonitorTag(tag);

        monitor.Tags.Should().HaveCount(1);
    }

    [Test]
    public async Task MonitorTag_OnChangeWithTwoMemberUpdates_ShouldInvokeCallbackTwice()
    {
        // Use faster Monitor rate for testing
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange(_ => { invoked++; });

        await client.WriteTag<TIMER>("TestTimer", t =>
        {
            t.PRE = Random.Shared.Next();
            t.ACC = Random.Shared.Next();
        });

        await Task.Delay(200);
        invoked.Should().Be(2);
    }

    [Test]
    public async Task MonitorTag_OnChangeWithTwoMemberUpdatesAndSimulatedWorkInCallback_ShouldInvokeCallbackTwice()
    {
        // Use faster Monitor rate for testing
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange(_ =>
        {
            Thread.Sleep(200);
            invoked++;
        });

        await client.WriteTag<TIMER>("TestTimer", t =>
        {
            t.PRE = Random.Shared.Next();
            t.ACC = Random.Shared.Next();
        });

        await Task.Delay(500);
        invoked.Should().Be(2);
    }

    [Test]
    public async Task MonitorTag_ConfigureSlowCallback_NotSureWhatWillHappen()
    {
        // Use faster Monitor rate for testing
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");

        var inCallback = 0;
        var overlapped = 0;
        var invoked = 0;

        monitor.OnUpdate(_ =>
        {
            if (Interlocked.Increment(ref inCallback) > 1)
                Interlocked.Exchange(ref overlapped, 1);

            try
            {
                Thread.Sleep(500);
                Interlocked.Increment(ref invoked);
            }
            finally
            {
                Interlocked.Decrement(ref inCallback);
            }
        });

        // Have to wait for the next Monitor interval.
        await Task.Delay(1500);

        monitor.Updates.Should().Be(3);
        invoked.Should().BeGreaterThan(0);
        invoked.Should().BeLessThan(6, "slow callbacks should throttle delivery if callbacks are serialized");
        overlapped.Should().Be(0, "callbacks should not run concurrently for the same monitor");
    }
}