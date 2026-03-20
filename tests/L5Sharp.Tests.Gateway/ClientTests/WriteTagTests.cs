using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WriteTagTests : PlcTestBase
{
    [Test]
    [NonParallelizable]
    [TestCase("TestDint")]
    [TestCase("TestTimer.PRE")]
    [TestCase("Program:MainProgram.LocalDint")]
    [TestCase("Program:MainProgram.LocalTimer.ACC")]
    public async Task WriteTag_ValidDintTagName_ShouldReturnSuccess(string tagName)
    {
        using var client = CreateClient();

        var result = await client.WriteTag<DINT>(tagName, Random.Shared.Next());

        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    [TestCase("TestTimer")]
    [TestCase("Program:MainProgram.LocalTimer")]
    public async Task WriteTag_ValidTimerTagName_ShouldReturnSuccess(string tagName)
    {
        using var client = CreateClient();

        // Only sets a few members but note that all data will be overwritten, not just member set here.
        var timer = new TIMER
        {
            PRE = Random.Shared.Next(),
            ACC = Random.Shared.Next(),
            DN = true
        };

        var result = await client.WriteTag(tagName, timer);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_TagDoesNotExist_ShouldReturnFailedWithError()
    {
        using var client = CreateClient();

        var result = await client.WriteTag<DINT>("FakeTag", 123);

        result.Success.Should().BeFalse();
        result.Status.Should().Be(TagStatus.NotFound);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().HaveCount(1);
    }

    [Test]
    public async Task WriteTag_ExistingNameAndValidType_ShouldReturnSuccess()
    {
        using var client = CreateClient();

        var result = await client.WriteTag<TIMER>("TestTimer", d =>
        {
            d.PRE = 12345;
            d.ACC = 123;
            d.EN = true;
        });

        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_DintTagInstanceOverload_ShouldGetSuccessfulResult()
    {
        using var client = CreateClient();
        var tag = Tag.Named("TestDint").WithValue(123).Build();

        var result = await client.WriteTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_TimerTagInstanceOverload_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = CreateClient();

        var tag = Tag
            .Named("TestTimer")
            .WithValue<TIMER>(t =>
            {
                t.PRE = 10000;
                t.DN = 1;
            })
            .Build();

        var result = await client.WriteTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_AlarmAnalogType_ShouldReturnSuccess()
    {
        using var client = CreateClient();

        var result = await client.WriteTag<ALARM_ANALOG>("TestAlarm", d =>
        {
            d.HHEnabled = true;
            d.HHLimit = 1234;
            d.LLEnabled = true;
            d.LLLimit = -123;
            d.LLMinDurationEnable = true;
            d.LLSeverity = 1000;
        });

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_SameTagMultipleTimes_ShouldBeFinalValue()
    {
        using var client = CreateClient();

        for (var i = 0; i < 10; i++)
        {
            await client.WriteTag<DINT>("TestTimer.PRE", i);
        }

        var result = await client.ReadTag<DINT>("TestTimer.PRE");

        result.Success.Should().BeTrue();
        result.Tag.Value.Should().Be(9);
    }
}