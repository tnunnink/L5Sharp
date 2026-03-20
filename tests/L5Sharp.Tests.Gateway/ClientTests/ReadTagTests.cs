using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway.Common;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class ReadTagTests : PlcTestBase
{
    #region BasicTests

    [Test]
    public async Task ReadTag_TagExists_ShouldReturnSuccessAndExpectedData()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<DINT>("TestDint");

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Errors.Should().BeEmpty();
        result.Tag.Value.Should().NotBe(0);
    }

    [Test]
    public async Task ReadTag_TagDoesNotExist_ShouldReturnNotFoundResponse()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("Fake");

        var result = await client.ReadTag(tag);

        result.Success.Should().BeFalse();
        result.Status.Should().Be(TagStatus.NotFound);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Tag.Should().NotBeNull();
        result.Errors.Should().HaveCount(1);
        result.HasError(TagStatus.NotFound).Should().BeTrue();
    }

    [Test]
    public async Task ReadTag_TagWithNoData_ShouldThrowException()
    {
        using var client = CreateClient();
        var tag = new Tag { Name = "TestDint" };

        // ReSharper disable once AccessToDisposedClosure just for testing purposes
        await FluentActions.Awaiting(() => client.ReadTag(tag))
            .Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task ReadTag_LoopToEnsureCachedHandle_ShouldReadInExpectedTime()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");

        for (var i = 0; i < 10; i++)
        {
            var result = await client.ReadTag(tag);
            result.Success.Should().BeTrue();
            Console.WriteLine($"{result.Timestamp} | {tag.Value}");
        }
    }

    #endregion

    #region AtomicTests

    [Test]
    public async Task ReadTag_AtomicControllerTagByNameAndType_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<DINT>("TestDint");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicControllerTagInstance_ShouldReturnOk()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("TestDint");

        var result = await client.ReadTag(tag);

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicProgramTagByNameAndType_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<DINT>("Program:MainProgram.LocalDint");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicProgramTagInstance_ShouldReturnOk()
    {
        using var client = CreateClient();
        var tag = Tag.New<DINT>("Program:MainProgram.LocalDint");

        var result = await client.ReadTag(tag);

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicBOOL_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<BOOL>("TestBool");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicSINT_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<SINT>("TestSint");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicINT_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<INT>("TestInt");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_AtomicREAL_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<REAL>("TestReal");

        result.Status.Should().Be(TagStatus.Ok);
    }

    #endregion

    #region ComplexTests

    [Test]
    public async Task ReadTag_ComplexControllerTagTimerInstance_ShouldReturnOk()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("TestTimer");

        var result = await client.ReadTag(tag);

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_ComplexControllerTagTimerByNameAndType_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<TIMER>("TestTimer");

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_ComplexProgramTagTimerInstance_ShouldReturnOk()
    {
        using var client = CreateClient();
        var tag = Tag.New<TIMER>("Program:MainProgram.LocalTimer");

        var result = await client.ReadTag(tag);

        result.Status.Should().Be(TagStatus.Ok);
    }

    [Test]
    public async Task ReadTag_ComplexControllerTagAlarmType_ShouldReturnOkWithDataValue()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<ALARM_ANALOG>("TestAlarm");

        result.Status.Should().Be(TagStatus.Ok);
        result.Tag.Value.As<ALARM_ANALOG>().HHLimit.Should().NotBe(0);
    }

    #endregion

    #region ArrayTests

    [Test]
    public async Task ReadTag_ArrayControllerTagAtomicArrayInstance_ShouldReturnOkWithDataValues()
    {
        using var client = CreateClient();
        var tag = new Tag { Name = "TestArray", Value = new ArrayData<DINT>(5) };

        var result = await client.ReadTag(tag);

        result.Status.Should().Be(TagStatus.Ok);
        result.Tag.Value.Members.Should().AllSatisfy(m => m.Value.Should().NotBe(0));
    }

    [Test]
    public async Task ReadTag_ArrayControllerTagAtomicArrayByNameAndType_ShouldReturnOk()
    {
        using var client = CreateClient();

        var result = await client.ReadTag<DINT>("TestArray");//todo 5);

        result.Status.Should().Be(TagStatus.Ok);
        //result.Tag.Value.Members.Should().AllSatisfy(m => m.Value.Should().NotBe(0));
    }


    #endregion

    #region CollectionTests

    [Test]
    public async Task ReadTags_LargeSetOfAtomicTag_ShouldReturnSuccessAndNotTakeForever()
    {
        using var client = CreateClient();
        var tags = Enumerable.Range(0, 1000).Select(i => new Tag($"Tag_{i}", new DINT())).ToList();

        var result = await client.ReadTags(tags);

        result.Success.Should().BeTrue();
        result.Status.Should().Be(TagStatus.Ok);
        result.Timestamp.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        result.Duration.Should().BeGreaterThan(TimeSpan.Zero);
        result.Duration.Should().BeLessThan(TimeSpan.FromSeconds(2));
        result.Tags.Should().HaveCount(1000);
        result.Errors.Should().BeEmpty();
    }

    #endregion

    
}