using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class WriteTagTests : PlcTestBase
{
    [Test]
    public async Task WriteTag_ExistingNameAndValidData_ShouldReturnSuccess()
    {
        using var client = CreateClient();

        var result = await client.WriteTag<DINT>("TestDint", 123);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
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

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_DintType_ShouldGetSuccessfulResult()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");
        tag.Value = 123;

        var result = await client.WriteTag(tag);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task WriteTag_TimerType_ShouldGetSuccessAndExpectedMemberValue()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("TestTimer");
        tag["PRE"].Value = 10000;
        tag["DN"].Value = 1;

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
}