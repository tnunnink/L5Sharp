using FluentAssertions;
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
    public async Task Ping_ValidReachableAddress_ShouldBeTrue()
    {
        using var client = new PlcClient("10.10.38.32", 1);

        var result = await client.Ping();

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Read_SimpleAtomicTag_ShouldGetSuccessfulResult()
    {
        var tag = Tag.New<DINT>("RTC.MONTH");
        using var client = new PlcClient("10.10.38.32", 1);

        var response = await client.ReadTag(tag);

        response.Tag.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagResult.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Errors.Should().BeEmpty();
        tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task Read_SimpleAtomicInLoopToEnsureCachedHandle_ShouldReadInExpectedTime()
    {
        var tag = Tag.New<DINT>("RTC.MONTH");
        using var client = new PlcClient("10.10.38.32", 1);

        for (var i = 0; i < 10; i++)
        {
            var response = await client.ReadTag(tag);
            response.Success.Should().BeTrue();
            Console.WriteLine($"{response.Timestamp} | {tag.Value}");
        }
    }

    [Test]
    public async Task Read_ComplexPredefinedTag_ShouldGetSuccessfulResult()
    {
        var tag = Tag.New<TIMER>("RTC");
        using var client = new PlcClient("10.10.38.32", 1);

        var response = await client.ReadTag(tag);

        response.Tag.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagResult.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Errors.Should().BeEmpty();
        tag.Value.As<TIMER>().PRE.Should().NotBe(0);
    }

    [Test]
    public async Task Read_ComplexAlarmType_ShouldGetSuccessfulResult()
    {
        var tag = Tag.New<ALARM>("SomeAlarm");
        using var client = new PlcClient("10.10.38.32", 1);

        var response = await client.ReadTag(tag);

        response.Tag.Should().Be(new TagName("SomeAlarm"));
        response.Success.Should().BeTrue();
        response.Status.Should().Be(TagResult.Ok);
        response.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        response.Errors.Should().BeEmpty();
        tag.Value.As<ALARM>().In.Should().Be(1.23f);
    }

    [Test]
    public void CreateWatch_ValidTag_ShouldHaveExpectedDefaults()
    {
        var tag = Tag.New<DINT>("SomeDINT");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.WatchTags([tag]);

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
        using var watch = client.WatchTags([tag]);

        Assert.That(watch.Tags.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CreateWatch_MultipleTags_ShouldContainsExpectedCount()
    {
        var t1 = Tag.New<DINT>("SomeDINT");
        var t2 = Tag.New<DINT>("SomeTimer");
        var t3 = Tag.New<DINT>("SomeString");
        using var client = new PlcClient("10.10.38.32", 1);
        using var watch = client.WatchTags([t1, t2, t3]);

        Assert.That(watch.Tags.Count(), Is.EqualTo(3));
    }
}