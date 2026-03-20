using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class UpdateTagTests : PlcTestBase
{
    [Test]
    [TestCase("TestTimer")]
    [TestCase("Program:MainProgram.LocalTimer")]
    public async Task UpdateTag_ValidTimerTagNamesAndMemberCollection_ShouldBeSuccess(string tagName)
    {
        using var client = CreateClient();

        var result = await client.UpdateTag<TIMER>(tagName,
        [
            ("PRE", Random.Shared.Next()),
            ("TT", 1)
        ]);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Should().NotBeNull();
        result.Tag.Value.As<TIMER>().PRE.Should().NotBe(0);
        result.Tag.Value.As<TIMER>().TT.Should().Be(1);
    }

    [Test]
    [TestCase("TestTimer")]
    [TestCase("Program:MainProgram.LocalTimer")]
    public async Task UpdateTag_ValidTimerTagNamesUpdateAction_ShouldBeSuccess(string tagName)
    {
        using var client = CreateClient();

        var result = await client.UpdateTag<TIMER>(tagName, t =>
        {
            t.PRE = Random.Shared.Next();
            t.DN = 1;
        });

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Should().NotBeNull();
        result.Tag.Value.As<TIMER>().PRE.Should().NotBe(0);
        result.Tag.Value.As<TIMER>().DN.Should().Be(1);
    }
}