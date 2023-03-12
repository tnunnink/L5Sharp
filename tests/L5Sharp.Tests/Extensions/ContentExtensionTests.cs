using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Tests.Extensions;

[TestFixture]
public class ContentExtensionTests
{
    [Test]
    public void Text_WhenCalled_ReturnsNotEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Text_ProgramScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic(Scope.Program, "MainProgram").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(10);
    }

    [Test]
    public void Text_RoutineScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic(Scope.Routine, "Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(11);
    }

    [Test]
    public void Text_ProgramAndRoutineScope_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic("NProgram", "Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(1);
    }

    [Test]
    public void Text_FilterFurther_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic().Where(t => t.ContainsKey("MOV")).ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void LookupTags_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var lookup = content.LookupTags();

        lookup.Should().NotBeEmpty();
    }

    [Test]
    public void LookupTags_FindModuleTags_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var moduleTags = content.Modules().SelectMany(m => m.Tags()).ToList();

        var lookup = content.LookupTags()
            .Where(t => moduleTags.Any(x => TagNameComparer.BaseName.Equals(x.TagName, t.Key)))
            .ToList();

        lookup.Should().NotBeEmpty();
    }
}