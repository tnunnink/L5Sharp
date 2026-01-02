using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class WatchTests
{
    [Test]
    public async Task StartAsync_ValidTags_ShouldStartBackgroundTask()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);
        using var subscription = watch.Subscribe(t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await watch.StartAsync();

        await Task.Delay(5000);

        var status = tag.GetStatus();

        Assert.Multiple(() =>
        {
            Assert.That(tag.Value, Is.Not.EqualTo(new DINT(0)));
            Assert.That(status.Result, Is.EqualTo(TagResult.Ok));
            Assert.That(status.IsGood, Is.True);
            Assert.That(status.IsBad, Is.False);
            Assert.That(status.Timestamp, Is.AtLeast(DateTime.UtcNow.AddSeconds(-1)));
        });
    }
}