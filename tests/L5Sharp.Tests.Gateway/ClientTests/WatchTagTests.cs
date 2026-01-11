using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WatchTagTests : PlcTestBase
{
    [Test]
    public async Task WatchTag_ValidTags_ShouldReturnExpectedValueAfterFewSeconds()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDINT");

        using var writeToConsole = await client.WatchTag(tag, t =>
            Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
        );

        await Task.Delay(2000);
        tag.Value.Should().NotBe(0);
    }
}