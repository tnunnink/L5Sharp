using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class TagWatchTests
{
    [Test]
    public async Task Start_ValidTags_ShouldStartBackgroundTask()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.WatchTags([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        watch.Start();
        await Task.Delay(2000);

        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task RunFor_ValidTag_ShouldRunForExpectedDuration()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.WatchTags([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.RunFor(3000);

        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task RunWhile_ValidTag_ShouldRunForExpectedDuration()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.WatchTags([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.RunWhile(t => t.Value != 0);

        tag.Value.Should().NotBe(0);
    }
}