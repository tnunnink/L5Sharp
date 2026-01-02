using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class PlcClientTests
{
    [Test]
    public void New_ValidAddress_ShouldNotBeNull()
    {
        using var client = new PlcClient("10.10.38.32", 1);

        Assert.That(client, Is.Not.Null);
    }

    [Test]
    public void New_ValidParameters_ShouldHaveExpectedProperties()
    {
        using var client = new PlcClient("10.10.38.32", 1);
    }

    [Test]
    public async Task PingAsync_ValidReachableAddress_ShouldBeTrue()
    {
        using var client = new PlcClient("10.10.38.32", 1);

        var result = await client.PingAsync();

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ReadAsync_Ex01_ShouldGetSuccessfulResult()
    {
        using var client = new PlcClient("10.10.38.32", 1);
        var tag = Tag.New<DINT>("SomeDINT");

        var result = await client.ReadAsync(tag);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(TagResult.Ok));
            Assert.That(tag.Value, Is.Not.EqualTo(new DINT(0)));
        });
    }

    [Test]
    public async Task ReadAsync_Ex02_ShouldGetSuccessfulResult()
    {
        using var client = new PlcClient("10.10.38.32", 1);
        var tag = Tag.New<TIMER>("SomeTimer");

        var result = await client.ReadAsync(tag);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(TagResult.Ok));
            //Assert.That(tag.Value.As<TIMER>().PRE, Is.EqualTo(new DINT(5000)));
            //todo we need to get source generator mappings complete to read the correct byte stream from complex predefined types.
        });
    }

    [Test]
    public void CreateWatch_ValidTag_ShouldHaveExpectedDefaults()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);

        Assert.Multiple(() =>
        {
            Assert.That(watch.IsRunning, Is.False);
            Assert.That(watch.RefreshRate, Is.EqualTo(1000));
        });
    }

    [Test]
    public void CreateWatch_SingleTags_ShouldContainsExpectedCount()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([tag]);

        Assert.That(watch.Tags.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CreateWatch_MultipleTags_ShouldContainsExpectedCount()
    {
        var t1 = Tag.New<DINT>("SomeDINT");
        var t2 = Tag.New<DINT>("SomeTimer");
        var t3 = Tag.New<DINT>("SomeString");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.CreateWatch([t1, t2, t3]);

        Assert.That(watch.Tags.Count(), Is.EqualTo(3));
    }
}