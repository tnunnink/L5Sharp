using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Samples;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Core.Tests;

[TestFixture]
public class ProofTests
{
    [Test]
    public void SampleQuery001()
    {
        var content = LogixContent.Load(Known.Template);

        var results = content.Find<Tag>()
            .SelectMany(t => t.Members())
            .Where(t => t.DataType == "TIMER")
            .Select(t => new { t.TagName, t.Description, Preset = t.Value.As<TIMER>().PRE })
            .OrderBy(v => v.TagName)
            .ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void SampleQuery002()
    {
        var content = LogixContent.Load(Known.Template);

        var results = content.Find<Tag>()
            .SelectMany(t => t.Members())
            .Where(t => t.DataType == "AnalogInput")
            .OrderBy(v => v.TagName)
            .ToList();

        results.Should().NotBeEmpty();
    }
    
    [Test]
    public void SampleQuery003()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Find<Tag>().Where(t => t.Scope() == Scope.Program && t.DataType == "DINT");

        results.Should().NotBeEmpty();
    }
}