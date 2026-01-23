using FluentAssertions;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WaitTagTests : PlcTestBase
{
    [Test]
    public async Task WaitTag_ValidControllerDint_ShouldReturnExpected()
    {
        using var client = CreateClient();

        var result = await client.PollTag<DINT>("TestDint", d => d > 0);

        result.Success.Should().BeTrue();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task WaitTag_ValidControllerTimer_ShouldReturnExpected()
    {
        using var client = CreateClient();

        var result = await client.PollTag<TIMER>("TestTimer", d => d.DN);

        result.Success.Should().BeTrue();
        result.Tag.Value.As<TIMER>().DN.Should().NotBe(true);
    }

    [Test]
    public async Task WaitTag_InvalidTagName_ShouldReturnFailure()
    {
        using var client = CreateClient();

        var response = await client.PollTag<TIMER>("FakeTag", d => d.DN);

        response.Success.Should().BeFalse();
    }
}