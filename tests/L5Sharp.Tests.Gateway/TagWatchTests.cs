using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class TagWatchTests
{
    [Test]
    public async Task StartAsync_ValidTags_ShouldStartBackgroundTask()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.StartAsync();
        await Task.Delay(2000);

        //Get the latest status for the tag read from PLC
        var status = tag.GetStatus();

        tag.Value.Should().NotBe(0);
        status.IsGood.Should().BeTrue();
        status.Handle.Should().NotBe(0);
        status.TagName.Should().Be(new TagName("SomeDINT"));
        status.Result.Should().Be(TagResult.Ok);
        status.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
    }

    [Test]
    public async Task RunForAsync_ValidTag_ShouldRunForExpectedDuration()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.RunForAsync(3000);

        var status = tag.GetStatus();
        tag.Value.Should().NotBe(0);
        status.Result.Should().Be(TagResult.Ok);
    }

    [Test]
    public async Task RunWhileAsync_ValidTag_ShouldRunForExpectedDuration()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.RunWhileAsync(t => t.Value != 0);

        var status = tag.GetStatus();
        tag.Value.Should().NotBe(0);
        status.Result.Should().Be(TagResult.Ok);
    }
}