using FluentAssertions;
using L5Sharp.Samples;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Core.Tests;

[TestFixture]
public class LogixContentTagTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags.ToList();

        result.Should().NotBeEmpty();
    }
    
    [Test]
    public void AllTagsToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags.SelectMany(t => t.Members()).ToList();

        result.Should().NotBeEmpty();
    }
    
    [Test]
    public void Test()
    {
        var content = LogixContent.Load(Known.Test);

        var result = content.Tags.Get("DateTimeNs");

        result.Should().NotBeNull();
        result.Value.Should().BeOfType<LINT>();
    }
}