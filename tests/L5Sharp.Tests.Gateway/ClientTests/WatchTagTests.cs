using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.PlcClientTests;

[TestFixture]
public class WatchTagTests
{
    private const string TestIp = "10.11.19.204";
    private const int TestSlot = 1;

    [Test]
    public async Task WatchTag_ValidTags_ShouldReturnExpectedValueAfterFewSeconds()
    {
        using var client = new PlcClient(TestIp, TestSlot);
        var tag = Tag.New<DINT>("TestDINT");

        using var writeToConsole = await client.WatchTag(tag, t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await Task.Delay(2000);
        tag.Value.Should().NotBe(0);
    }

    /*
    [Test]
    public async Task RunFor_ValidTag_ShouldRunForExpectedDuration()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.Watch([tag]);
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
        using var watch = client.Watch([tag]);
        using var writeToConsole = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.RunWhile(t => t.Value != 0);

        tag.Value.Should().NotBe(0);
    }*/
}