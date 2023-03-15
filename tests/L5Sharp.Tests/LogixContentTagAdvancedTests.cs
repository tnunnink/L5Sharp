using FluentAssertions;
using L5Sharp.Components;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentTagAdvancedTests
{
    [Test]
    public void FilterByScope()
    {
        var content = LogixContent.Load(Known.Test);

        var tags = content.Query<Tag>().Where(t => t.Container == "MainProgram").ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public void GetTagsAcrossFileToLookup()
    {
        
    }
}