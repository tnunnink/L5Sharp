using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class ScopeTests
{
    [Test]
    public void None_WhenCalled_ShouldHaveExpectedLevelAndContainer()
    {
        var scope = Scope.None;

        scope.Level.Should().Be(ScopeLevel.None);
        scope.Container.Should().BeEmpty();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
        scope.IsProgram.Should().BeFalse();
        scope.IsDefinition.Should().BeFalse();
        scope.IsCode.Should().BeFalse();
    }

    [Test]
    public void Controller_WhenCalled_ShouldHaveExpectedLevelAndContainer()
    {
        var scope = Scope.Controller;

        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().BeEmpty();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
        scope.IsProgram.Should().BeFalse();
        scope.IsDefinition.Should().BeFalse();
        scope.IsCode.Should().BeFalse();
    }

    [Test]
    public void Program_ValidName_ShouldHaveExpectedProperties()
    {
        var scope = Scope.Program("MyProgram");

        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyProgram");
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
        scope.IsProgram.Should().BeTrue();
        scope.IsDefinition.Should().BeFalse();
        scope.IsCode.Should().BeFalse();
    }

    [Test]
    public void Definition_ValidName_ShouldHaveExpectedProperties()
    {
        var scope = Scope.Definition("MyDefinition");

        scope.Level.Should().Be(ScopeLevel.Definition);
        scope.Container.Should().Be("MyDefinition");
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
        scope.IsProgram.Should().BeFalse();
        scope.IsDefinition.Should().BeTrue();
    }

    [Test]
    public void IsIn_MatchingContainer_ShouldBeTrue()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsIn("MyProgram");

        result.Should().BeTrue();
    }

    [Test]
    public void IsIn_DifferentContainer_ShouldBeFalse()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsIn("OtherProgram");

        result.Should().BeFalse();
    }

    [Test]
    public void IsIn_ControllerInProgram_ShouldBeFalse()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsIn(string.Empty);

        result.Should().BeFalse();
    }

    [Test]
    public void IsIn_ProgramInController_ShouldBeFalse()
    {
        var scope = Scope.Controller;

        var result = scope.IsIn("MyProgram");

        result.Should().BeFalse();
    }

    [Test]
    public void IsIn_ControllerInController_ShouldBeTrue()
    {
        var scope = Scope.Controller;

        var result = scope.IsIn(string.Empty);

        result.Should().BeTrue();
    }

    [Test]
    public void IsVisibleTo_ControllerToController_ShouldBeTrue()
    {
        var scope = Scope.Controller;

        var result = scope.IsVisibleTo(Scope.Controller);

        result.Should().BeTrue();
    }

    [Test]
    public void IsVisibleTo_ControllerToProgram_ShouldBeTrue()
    {
        var scope = Scope.Controller;

        var result = scope.IsVisibleTo(Scope.Program("DoesNotMatter"));

        result.Should().BeTrue();
    }

    [Test]
    public void IsVisibleTo_ProgramToController_ShouldBeTrue()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsVisibleTo(Scope.Controller);

        result.Should().BeTrue();
    }

    [Test]
    public void IsVisibleTo_ProgramToSameProgramScope_ShouldBeTrue()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsVisibleTo(Scope.Program("MyProgram"));

        result.Should().BeTrue();
    }

    [Test]
    public void IsVisibleTo_DifferentScope_ShouldBeFalse()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsVisibleTo(Scope.Program("OtherProgram"));

        result.Should().BeFalse();
    }

    [Test]
    public void IsLocalTo_SameProgram_ShouldBeTrue()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsLocalTo(Scope.Program("MyProgram"));

        result.Should().BeTrue();
    }

    [Test]
    public void IsLocalTo_DifferentProgram_ShouldBeFalse()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsLocalTo(Scope.Program("OtherProgram"));

        result.Should().BeFalse();
    }

    [Test]
    public void IsLocalTo_Controller_ShouldBeFalse()
    {
        var scope = Scope.Program("MyProgram");

        var result = scope.IsLocalTo(Scope.Controller);

        result.Should().BeFalse();
    }
}