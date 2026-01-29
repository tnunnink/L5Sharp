using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
[NonParallelizable]
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
    public async Task MonitorTag_TagThatDoesNotExist_ShouldStillReturnWithErrors()
    {
        using var client = CreateClient();

        using var monitor = await client.MonitorTag<DINT>("FakeTag");

        monitor.IsActive.Should().BeFalse();
        monitor.HasErrors.Should().BeTrue();
        monitor.Status.Should().Be(TagStatus.NotFound);
        monitor.Timestamp.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        monitor.Rate.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
        monitor.Updates.Should().BeGreaterThan(0);
        monitor.Tags.Should().HaveCount(1);
        monitor.Errors.Should().HaveCount(1);
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
    public async Task MonitorTag_ValidTagInstance_ShouldReturnExpectedValue()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDINT");

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
    public async Task MonitorTag_WaitFewScans_ShouldHaveExpectedCounts()
    {
        using var client = CreateClient(x => x.PollRate = 50);
        var tag = Tag.New<TIMER>("Program:MainProgram.LocalTimer");

        using var monitor = await client.MonitorTag(tag);

        await Task.Delay(200);

        monitor.Updates.Should().BeGreaterThanOrEqualTo(4);
    }

    [Test]
    public async Task OnUpdate_GlobalCallback_ShouldGetInvokedWhenPolled()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnUpdate(_ => Interlocked.Increment(ref invoked));

        await Task.Delay(200);
        invoked.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task OnUpdate_TagNameCallback_ShouldGetInvokedWhenPolled()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnUpdate("TestTimer.DN", _ => Interlocked.Increment(ref invoked));

        await Task.Delay(200);
        invoked.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task OnUpdate_InvalidTagName_ShouldNotBeInvoked()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnUpdate("TstTimer.DN", _ => Interlocked.Increment(ref invoked));
        await Task.Delay(200);

        invoked.Should().Be(0);
    }

    [Test]
    public async Task OnChange_GlobalCallback_ShouldGetInvokedWhenAnyMemberChanges()
    {
        using var client = CreateClient(o => o.PollRate = 50);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        await client.WriteTag<DINT>("TestTimer.PRE", 123);
        await Task.Delay(200);

        monitor.OnChange(_ => Interlocked.Increment(ref invoked));

        await client.WriteTag<DINT>("TestTimer.PRE", 456);
        await Task.Delay(200);

        invoked.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task OnChange_TagNameCallback_ShouldGetInvokedWhenSpecificTagChanges()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange("TestTimer.PRE", _ => Interlocked.Increment(ref invoked));

        await client.UpdateTag<TIMER>("TestTimer", t => t.PRE = Random.Shared.Next());
        await Task.Delay(200);

        invoked.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task OnChange_TagNameCallback_ShouldNotGetInvokedWhenOtherMemberChanges()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange("TestTimer.PRE", _ => Interlocked.Increment(ref invoked));

        await client.WriteTag<DINT>("TestTimer.ACC", Random.Shared.Next());
        await Task.Delay(200);

        invoked.Should().Be(0);
    }

    [Test]
    public async Task OnChange_InvalidTagName_ShouldNotGetInvokedWhenChanged()
    {
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange("TstTimer.DN", _ => Interlocked.Increment(ref invoked));

        await Task.Delay(200);
        invoked.Should().Be(0);
    }

    [Test]
    public async Task OnChange_SuccessiveChanges_ShouldInvokeCallbackTwice()
    {
        // Use faster Monitor rate for testing
        using var client = CreateClient(o => o.PollRate = 50);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange(_ => { Interlocked.Increment(ref invoked); });

        await client.WriteTag<DINT>("TestTimer.PRE", Random.Shared.Next());
        await Task.Delay(200);
        await client.WriteTag<DINT>("TestTimer.PRE", Random.Shared.Next());
        await Task.Delay(200);

        invoked.Should().Be(2);
    }

    [Test]
    public async Task OnChange_WithTwoMemberUpdatesAndSimulatedWorkInCallback_ShouldInvokeCallbackTwice()
    {
        // Use faster Monitor rate for testing
        using var client = CreateClient(o => o.PollRate = 100);
        using var monitor = await client.MonitorTag<TIMER>("TestTimer");
        var invoked = 0;

        monitor.OnChange(_ =>
        {
            Thread.Sleep(200);
            Interlocked.Increment(ref invoked);
        });

        await client.WriteTag<TIMER>("TestTimer", t =>
        {
            t.PRE = Random.Shared.Next();
            t.ACC = Random.Shared.Next();
        });

        await Task.Delay(500);
        invoked.Should().BeGreaterOrEqualTo(2);
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

        invoked.Should().BeGreaterThan(0);
        invoked.Should().BeLessThan(6, "slow callbacks should throttle delivery if callbacks are serialized");
        overlapped.Should().Be(0, "callbacks should not run concurrently for the same monitor");
    }

    [Test]
    public async Task MonitorTags_LargeCollectionOfTags_ShouldHaveDesiredRateAndUpdated()
    {
        using var client = CreateClient();
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        using var monitor = await client.MonitorTags(tags);

        monitor.IsActive.Should().BeTrue();
        monitor.HasErrors.Should().BeFalse();
        monitor.Status.Should().Be(TagStatus.Ok);
        monitor.Timestamp.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        monitor.Rate.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
        monitor.Rate.Should().BeLessThan(TimeSpan.FromMilliseconds(1000));
        monitor.Updates.Should().BeGreaterThan(0);
        monitor.Tags.Should().HaveCount(1000);
        monitor.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task MonitorTags_LargeCollectionOfTagsWithFastPollRate_ShouldHaveDesiredRateAndUpdated()
    {
        using var client = CreateClient(x => x.PollRate = 100);
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        using var monitor = await client.MonitorTags(tags);

        // Wait a few cycles to get more stats
        await Task.Delay(5000);

        monitor.IsActive.Should().BeTrue();
        monitor.HasErrors.Should().BeFalse();
        monitor.Status.Should().Be(TagStatus.Ok);
        monitor.Timestamp.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        monitor.Rate.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
        monitor.Rate.Should().BeLessThan(TimeSpan.FromMilliseconds(500));
        monitor.Updates.Should().BeGreaterThan(0);
        monitor.Tags.Should().HaveCount(1000);
        monitor.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task MonitorTags_MixedValidAndInvalidTags_ShouldStillReturnWithErrorsAndUpdated()
    {
        using var client = CreateClient();
        var valid = Tag.New<DINT>("TestDint");
        var invalid = Tag.New<DINT>("FakeDint");

        using var monitor = await client.MonitorTags([valid, invalid]);

        monitor.IsActive.Should().BeTrue();
        monitor.HasErrors.Should().BeTrue();
        monitor.Status.Should().Be(TagStatus.NotFound);
        monitor.Timestamp.Should().BeAfter(DateTime.Now.AddSeconds(-1));
        monitor.Rate.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
        monitor.Updates.Should().BeGreaterThan(0);
        monitor.Tags.Should().HaveCount(2);
        monitor.Errors.Should().HaveCount(1);
    }
}