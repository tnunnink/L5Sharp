using FluentAssertions;

namespace L5Sharp.Tests.Common;

[TestFixture]
public class ScopeTests
{
    [Test]
    public void Empty_WhenCalled_ShouldBeNullAndEmpty()
    {
        var scope = Scope.Empty;

        scope.Path.Should().BeEmpty();
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().BeEmpty();
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().BeEmpty();
        scope.Name.Should().BeEmpty();
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Of_InMemoryComponent_ShouldBeExpected()
    {
        var element = new Tag("Test", 100);

        var scope = Scope.Of(element);

        scope.Path.Should().Be("/Tag/Test");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().BeEmpty();
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("Test");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeFalse();
    }

    [Test]
    public void Of_InMemoryRun_ShouldBeExpected()
    {
        var element = new Rung("XIC(MyTag)OTE(OtherTag)");

        var scope = Scope.Of(element);

        scope.Path.Should().Be("/Rung/0");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().BeEmpty();
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Rung");
        scope.Name.Should().Be("0");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeFalse();
    }

    [Test]
    public void To_Null_ShouldBeExpectedScope()
    {
        FluentActions.Invoking(() => Scope.To(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void To_Empty_ShouldBeExpectedScope()
    {
        FluentActions.Invoking(() => Scope.To(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void To_Absolute1Parts_ShouldBeExpectedScope()
    {
        FluentActions.Invoking(() => Scope.To("MyController")).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void To_Absolute2Parts_ShouldThrowException()
    {
        FluentActions.Invoking(() => Scope.To("MyController/DataType")).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void To_Absolute3Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/Module/ModuleName");

        scope.Path.Should().Be("MyController/Module/ModuleName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Module");
        scope.Name.Should().Be("ModuleName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_Absolute4Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/Something/DontMatter/WhatItIs");

        scope.Path.Should().Be("MyController/Something/DontMatter/WhatItIs");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("Something");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("Something");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("DontMatter");
        scope.Name.Should().Be("WhatItIs");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Absolute5Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/Something/DontMatter/WhatItIs/Right?");

        scope.Path.Should().Be("MyController/Something/DontMatter/WhatItIs/Right?");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("DontMatter");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("Something");
        scope.Routine.Should().Be("DontMatter");
        scope.Type.Should().Be("WhatItIs");
        scope.Name.Should().Be("Right?");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Absolute6Parts_ShouldThrowException()
    {
        FluentActions.Invoking(() => Scope.To("First/Second/Thrid/Fourth/Fifth/Sixth"))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void To_Relative2Parts_ShouldThrowException()
    {
        FluentActions.Invoking(() => Scope.To("/Relative")).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void To_Relative3Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/Tag/MyTagName");

        scope.Path.Should().Be("/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().BeEmpty();
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("MyTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_Relative4PartsTagType_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/MyProgram/Tag/SomeTagName");

        scope.Path.Should().Be("/MyProgram/Tag/SomeTagName");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyProgram");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("SomeTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Relative4PartsRoutineType_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/MyProgram/Routine/MainRoutine");

        scope.Path.Should().Be("/MyProgram/Routine/MainRoutine");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyProgram");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Routine");
        scope.Name.Should().Be("MainRoutine");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Relative5Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/Something/DontMatter/WhatItIs/Right?");

        scope.Path.Should().Be("/Something/DontMatter/WhatItIs/Right?");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("DontMatter");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("Something");
        scope.Routine.Should().Be("DontMatter");
        scope.Type.Should().Be("WhatItIs");
        scope.Name.Should().Be("Right?");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void Build_TypeAndName_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Type("Tag").Named("MyTagName");

        scope.Path.Should().Be("MyController/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("MyTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_DataType_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").DataType("MyType");

        scope.Path.Should().Be("MyController/DataType/MyType");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("DataType");
        scope.Name.Should().Be("MyType");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Aoi_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Aoi("MyType");

        scope.Path.Should().Be("MyController/AddOnInstructionDefinition/MyType");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("AddOnInstructionDefinition");
        scope.Name.Should().Be("MyType");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Module_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Module("ModuleName");

        scope.Path.Should().Be("MyController/Module/ModuleName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Module");
        scope.Name.Should().Be("ModuleName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Tag_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Tag("MyTagName");

        scope.Path.Should().Be("MyController/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("MyTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Program_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Program("MainProgram");

        scope.Path.Should().Be("MyController/Program/MainProgram");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Program");
        scope.Name.Should().Be("MainProgram");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Task_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Task("TaskName");

        scope.Path.Should().Be("MyController/Task/TaskName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Task");
        scope.Name.Should().Be("TaskName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_ProgramTypeAndName_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").In("MyProgram").Type("Tag").Named("MyTagName");

        scope.Path.Should().Be("MyController/MyProgram/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyProgram");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("MyTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void Build_ProgramRoutineTypeAndName_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").In("MyProgram").In("MyRoutine").Type("Tag").Named("MyTagName");

        scope.Path.Should().Be("MyController/MyProgram/MyRoutine/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("MyRoutine");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().Be("MyRoutine");
        scope.Type.Should().Be("Tag");
        scope.Name.Should().Be("MyTagName");
        scope.Task.Should().BeEmpty();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void ToXPath_ControllerScopedTag_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Tag("MyTag");

        var path = scope.ToXPath();

        path.Should().Be("/Controller[@Name='MyController']/Tags/Tag[@Name='MyTag']");
    }

    [Test]
    public void ToXPath_ControllerScopedDataType_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").DataType("SimpleType");

        var path = scope.ToXPath();

        path.Should().Be("/Controller[@Name='MyController']/DataTypes/DataType[@Name='SimpleType']");
    }

    [Test]
    public void ToXPath_ControllerScopedAoi_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Aoi("aoiMyType");

        var path = scope.ToXPath();

        path
            .Should().Be(
                "/Controller[@Name='MyController']/AddOnInstructionDefinitions/AddOnInstructionDefinition[@Name='aoiMyType']");
    }

    [Test]
    public void ToXPath_ControllerScopedModule_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Module("SomeModule");

        var path = scope.ToXPath();

        path.Should().Be("/Controller[@Name='MyController']/Modules/Module[@Name='SomeModule']");
    }

    [Test]
    public void ToXPath_ControllerScopedProgram()
    {
        var scope = Scope.Build("MyController").Program("MyProgram");

        var path = scope.ToXPath();

        path.Should().Be("/Controller[@Name='MyController']/Programs/Program[@Name='MyProgram']");
    }

    [Test]
    public void ToXPath_ControllerScopedTask()
    {
        var scope = Scope.Build("MyController").Task("MainTask");

        var path = scope.ToXPath();

        path.Should().Be("/Controller[@Name='MyController']/Tasks/Task[@Name='MainTask']");
    }
}