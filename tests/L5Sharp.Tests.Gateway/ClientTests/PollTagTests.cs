using System.Diagnostics;
using FluentAssertions;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class PollTagTests : PlcTestBase
{
    [Test]
    public async Task PollTag_ValidDuration_ShouldReturnAfterPeriodExpires()
    {
        using var client = CreateClient();

        var timer = Stopwatch.StartNew();
        var result = await client.PollTag<DINT>("TestDint", TimeSpan.FromSeconds(2));
        timer.Stop();

        result.Success.Should().BeTrue();
        timer.Elapsed.Should().BeGreaterThan(TimeSpan.FromSeconds(2));
    }

    [Test]
    public async Task PollTag_ZeroDuration_ShouldReturnAfterPeriodExpires()
    {
        using var client = CreateClient();

        var timer = Stopwatch.StartNew();
        var result = await client.PollTag<DINT>("TestDint", TimeSpan.FromSeconds(0));
        timer.Stop();

        result.Success.Should().BeTrue();
        timer.Elapsed.Should().BeGreaterThan(TimeSpan.FromSeconds(0));
    }

    [Test]
    public async Task PollTag_ValidPredicateThatShouldBeTrue_ShouldReturnSuccess()
    {
        using var client = CreateClient();
        await client.WriteTag<DINT>("TestDint", 100);

        var result = await client.PollTag<DINT>("TestDint", d => d > 0);

        result.Success.Should().BeTrue();
        result.Tag.Value.Should().Be(100);
    }

    [Test]
    public async Task PollTag_ValidComplexTypePredicateThatIsTrue_ShouldReturnSuccessAndValue()
    {
        using var client = CreateClient();
        await client.WriteTag<TIMER>("TestTimer", t => t.DN = true);

        var result = await client.PollTag<TIMER>("TestTimer", d => d.DN);

        result.Success.Should().BeTrue();
        result.Tag.Value.As<TIMER>().DN.Should().Be(true);
    }

    [Test]
    public async Task PollTag_UnsatisfiedPredicateAndCancellationAfterPeriod_ShouldThrowOperationCancelled()
    {
        await FluentActions.Awaiting(async () =>
            {
                using var client = CreateClient(o => o.PollRate = 100);
                await client.WriteTag<DINT>("TestDint", 0);
                var cancellation = new CancellationTokenSource();
                cancellation.CancelAfter(TimeSpan.FromSeconds(1));
                await client.PollTag<DINT>("TestDint", d => d > 0, cancellation.Token);
            })
            .Should().ThrowAsync<OperationCanceledException>();
    }

    [Test]
    public async Task PollTag_InvalidTagName_ShouldFailAfterTimeout()
    {
        using var client = CreateClient();

        var result = await client.PollTag<TIMER>("FakeTag", d => d.DN, TimeSpan.FromSeconds(1));

        result.Success.Should().BeFalse();
    }
}