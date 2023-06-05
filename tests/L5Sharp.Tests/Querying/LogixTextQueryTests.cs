using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Tests.Querying;

[TestFixture]
public class LogixTextQueryTests
{
    [Test]
    public void Logic_WhenCalled_ReturnsNotEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Text_ProgramScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().In("MainProgram").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(10);
    }

    [Test]
    public void Text_RoutineScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().In("Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(11);
    }

    [Test]
    public void Text_ProgramAndRoutineScope_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().In("NProgram", "Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(1);
    }

    [Test]
    public void Text_WithInstruction_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Test);
        
        var results = content.Text().WithInstruction(Instruction.XIC).ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Text_FilterFurther_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().Where(t => t.ContainsKey("MOV")).ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Flatten_WhenCalled_ShouldWOrk()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Text().Flatten().ToList();

        results.Should().NotBeEmpty();
    }
    
    [Test]
    public void Flatten_AgainstLargerFile_ShouldWork()
    {
        var content = LogixContent.Load(Known.Template);

        var results = content.Text().Flatten().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void TagLookup_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var lookup = content.TagLookup();

        lookup.Should().NotBeEmpty();
    }
}