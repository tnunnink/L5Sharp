using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Tests.Extensions;

[TestFixture]
public class ContentLogicExtensionsTests
{
    [Test]
    public void Logic_WhenCalled_ReturnsNotEmpty()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Logic_ProgramScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.LogicIn(Scope.Program, "MainProgram").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(10);
    }

    [Test]
    public void Logic_RoutineScope_ShouldReturnExpectedText()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.LogicIn(Scope.Routine, "Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(11);
    }

    [Test]
    public void Logic_ProgramAndRoutineScope_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.LogicIn("NProgram", "Main").ToList();

        results.Should().NotBeEmpty();
        results.Should().HaveCount(1);
    }

    [Test]
    public void Logic_FilterFurther_ShouldHaveCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Logic().Where(t => t.ContainsKey("MOV")).ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void LogicFlatten_WhenCalled_ShouldWOrk()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.LogicFlatten().ToList();

        results.Should().NotBeEmpty();
    }
    
    [Test]
    public void LogicFlatten_AgainstLargerFile_ShouldWOrk()
    {
        var content = LogixContent.Load(Known.Template);

        var results = content.LogicFlatten().ToList();

        results.Should().NotBeEmpty();
    }
}