using System.Diagnostics;
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
        scope.Type.Should().Be(ScopeType.Null);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsRelative.Should().BeFalse();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void TagScope_InMemeory_ShouldBeExpected()
    {
        var element = new Tag("Test", 100);

        var scope = element.Scope;

        scope.Path.Should().Be("/Tag/Test");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("/Tag/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("Test");
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void RungScope_InMemory_ShouldBeExpected()
    {
        var element = new Rung("XIC(MyTag)OTE(OtherTag)");

        var scope = element.Scope;

        scope.Path.Should().Be("/Rung/0");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("/Rung/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Rung);
        scope.Name.Should().Be("0");
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
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
    public void To_SeparatorOnly_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/");

        scope.Should().NotBeNull();
        scope.Path.Should().Be("/");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Null);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsRelative.Should().BeTrue();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_NoSeparator_ShouldBeExpectedScope()
    {
        var scope = Scope.To("Testing");

        scope.Should().NotBeNull();
        scope.Path.Should().Be("Testing");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().BeEmpty();
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Null);
        scope.Name.Should().Be("Testing");
        scope.IsRelative.Should().BeFalse();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_SingleSeparatorWithValidTypeName_ShouldBeExpectedScope()
    {
        var scope = Scope.To("Tag/MyTagName");

        scope.Should().NotBeNull();
        scope.Path.Should().Be("Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("Tag/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
        scope.IsRelative.Should().BeFalse();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_InvalidTypeName_ShouldHaveNullScopeType()
    {
        var scope = Scope.To("Fake/MyTagName");

        scope.Type.Should().Be(ScopeType.Null);
    }

    [Test]
    public void To_Absolute3PartsDoubleSlash_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController//");

        scope.Path.Should().Be("MyController//");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController//");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Null);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_Absolute3Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/Module/ModuleName");

        scope.Path.Should().Be("MyController/Module/ModuleName");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController/Module/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Module);
        scope.Name.Should().Be("ModuleName");
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_Absolute4Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/MyProgram/Tag/TagName.Member.Something");

        scope.Path.Should().Be("MyController/MyProgram/Tag/TagName.Member.Something");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyController/MyProgram/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("TagName.Member.Something");
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Absolute5Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/MyProgram/MainRoutine/Rung/23");

        scope.Path.Should().Be("MyController/MyProgram/MainRoutine/Rung/23");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("MyController/MyProgram/MainRoutine/Rung/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().Be("MainRoutine");
        scope.Type.Should().Be(ScopeType.Rung);
        scope.Name.Should().Be("23");
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Relative3PartsDoubleSlash_ShouldBeExpectedScope()
    {
        var scope = Scope.To("//Something");

        scope.Path.Should().Be("//Something");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("//");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Null);
        scope.Name.Should().Be("Something");
        scope.IsRelative.Should().BeTrue();
        scope.IsScoped.Should().BeFalse();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_Relative3Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/Tag/MyTagName");

        scope.Path.Should().Be("/Tag/MyTagName");
        scope.Level.Should().Be(ScopeLevel.Null);
        scope.Container.Should().Be("/Tag/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
        scope.IsRelative.Should().BeTrue();
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
        scope.Container.Should().Be("/MyProgram/Tag/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("SomeTagName");
        scope.IsRelative.Should().BeTrue();
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
        scope.Container.Should().Be("/MyProgram/Routine/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Routine);
        scope.Name.Should().Be("MainRoutine");
        scope.IsRelative.Should().BeTrue();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_Relative5Parts_ShouldBeExpectedScope()
    {
        var scope = Scope.To("/MyProgram/MainRoutine/Rung/23");

        scope.Path.Should().Be("/MyProgram/MainRoutine/Rung/23");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("/MyProgram/MainRoutine/Rung/");
        scope.Controller.Should().BeEmpty();
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().Be("MainRoutine");
        scope.Type.Should().Be(ScopeType.Rung);
        scope.Name.Should().Be("23");
        scope.IsRelative.Should().BeTrue();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_ContainerControllerType_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/Tag/");

        scope.Path.Should().Be("MyController/Tag/");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsRelative.Should().BeFalse();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void To_ContainerControllerProgramType_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/MyProgram/Tag/");

        scope.Path.Should().Be("MyController/MyProgram/Tag/");
        scope.Level.Should().Be(ScopeLevel.Program);
        scope.Container.Should().Be("MyController/MyProgram/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsRelative.Should().BeFalse();
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void To_ContainerControllerProgramRoutineType_ShouldBeExpectedScope()
    {
        var scope = Scope.To("MyController/MyProgram/MyRoutine/Rung/");

        scope.Path.Should().Be("MyController/MyProgram/MyRoutine/Rung/");
        scope.Level.Should().Be(ScopeLevel.Routine);
        scope.Container.Should().Be("MyController/MyProgram/MyRoutine/Rung/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().Be("MyRoutine");
        scope.Type.Should().Be(ScopeType.Rung);
        scope.Name.Should().Be(TagName.Empty);
        scope.IsRelative.Should().BeFalse();
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
        scope.Container.Should().Be("MyController/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
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
        scope.Container.Should().Be("MyController/DataType/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.DataType);
        scope.Name.Should().Be("MyType");
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeTrue();
        scope.IsLocal.Should().BeFalse();
    }

    [Test]
    public void Build_Aoi_ShouldBeExpected()
    {
        var scope = Scope.Build("MyController").Instruction("MyType");

        scope.Path.Should().Be("MyController/AddOnInstructionDefinition/MyType");
        scope.Level.Should().Be(ScopeLevel.Controller);
        scope.Container.Should().Be("MyController/AddOnInstructionDefinition/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Instruction);
        scope.Name.Should().Be("MyType");
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
        scope.Container.Should().Be("MyController/Module/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Module);
        scope.Name.Should().Be("ModuleName");
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
        scope.Container.Should().Be("MyController/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
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
        scope.Container.Should().Be("MyController/Program/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Program);
        scope.Name.Should().Be("MainProgram");
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
        scope.Container.Should().Be("MyController/Task/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().BeEmpty();
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Task);
        scope.Name.Should().Be("TaskName");
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
        scope.Container.Should().Be("MyController/MyProgram/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().BeEmpty();
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
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
        scope.Container.Should().Be("MyController/MyProgram/MyRoutine/Tag/");
        scope.Controller.Should().Be("MyController");
        scope.Program.Should().Be("MyProgram");
        scope.Routine.Should().Be("MyRoutine");
        scope.Type.Should().Be(ScopeType.Tag);
        scope.Name.Should().Be("MyTagName");
        scope.IsScoped.Should().BeTrue();
        scope.IsGlobal.Should().BeFalse();
        scope.IsLocal.Should().BeTrue();
    }

    [Test]
    public void Append_ControllerRootAndTypeNamePath_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller//");
        var second = Scope.To("/Tag/MyTagName");

        var result = first.Append(second);

        result.Path.Should().Be("Controller/Tag/MyTagName");
        result.Controller.Should().Be("Controller");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be("MyTagName");
    }

    [Test]
    public void Append_ChainMultipleNames_ShouldBeExpectedScope()
    {
        var result = Scope.To("Root").Append("Tag").Append("MyTagName");

        result.Path.Should().Be("Root/Tag/MyTagName");
        result.Controller.Should().Be("Root");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be("MyTagName");
    }
    
    [Test]
    public void Merge_SingleNameWithAbsolutePath_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller/Tag/MyTag");
        var second = Scope.To("MyTagName");

        var result = first.Merge(second);

        result.Path.Should().Be("Controller/Tag/MyTagName");
        result.Controller.Should().Be("Controller");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be("MyTagName");
    }
    
    [Test]
    public void Merge_TypeAndNameWithAbsolutePath_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller/Tag/MyTag");
        var second = Scope.To("/Tag/MyTagName");

        var result = first.Merge(second);

        result.Path.Should().Be("Controller/Tag/MyTagName");
        result.Controller.Should().Be("Controller");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be("MyTagName");
    }
    
    [Test]
    public void Merge_DifferentTypeAndNameWithAbsolutePath_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller/Tag/MyTag");
        var second = Scope.To("/Module/MyModule");

        var result = first.Merge(second);

        result.Path.Should().Be("Controller/Module/MyModule");
        result.Controller.Should().Be("Controller");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Module);
        result.Name.Should().Be("MyModule");
    }
    
    [Test]
    public void Merge_AbsoluteWithAbsolutePath_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller/Tag/MyTag");
        var second = Scope.To("NewController/Module/MyModule");

        var result = first.Merge(second);

        result.Path.Should().Be("NewController/Module/MyModule");
        result.Controller.Should().Be("NewController");
        result.Program.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Module);
        result.Name.Should().Be("MyModule");
    }
    
    [Test]
    public void Merge_ProgramScopeWithAbsolute_ShouldBeExpectedScope()
    {
        var first = Scope.To("Controller/Tag/MyTag");
        var second = Scope.To("/MyProgram/Tag/");

        var result = first.Merge(second);

        result.Path.Should().Be("Controller/MyProgram/Tag/MyTag");
        result.Controller.Should().Be("Controller");
        result.Program.Should().Be("MyProgram");
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be("MyTag");
    }
    
    [Test]
    public void Merge_RelativeProgramScopeWithRelativeType_ShouldBeExpectedScope()
    {
        var first = Scope.To("/Tag/");
        var second = Scope.To("/MyProgram//");

        var result = first.Merge(second);

        result.Path.Should().Be("/MyProgram/Tag/");
        result.Controller.Should().BeEmpty();
        result.Program.Should().Be("MyProgram");
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ScopeType.Tag);
        result.Name.Should().Be(TagName.Empty);
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
        var scope = Scope.Build("MyController").Instruction("aoiMyType");

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

    [Test]
    public void PerformanceOfGettingAllTagMemberScopesShouldBeNotTerrible()
    {
        var content = L5X.Load(Known.Example);

        var stopwatch = Stopwatch.StartNew();

        var scopes = content.Query<Tag>()
            .SelectMany(t => t.Members())
            .Select(t => t.Scope)
            .ToList();

        stopwatch.Stop();


        scopes.Should().NotBeEmpty();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    [Test]
    public void AllTagMembersShouldHaveTypeAndNameScopedProperty()
    {
        var content = L5X.Load(Known.Example);

        var scopes = content.Query<Tag>()
            .SelectMany(t => t.Members())
            .Select(t => t.Scope)
            .ToList();

        scopes.Should().NotBeEmpty();
        scopes.Should().AllSatisfy(s => s.Name.Should().NotBe(TagName.Empty));
        scopes.Should().AllSatisfy(s => s.Type.Should().Be(ScopeType.Tag));
        Console.WriteLine(scopes.Count);
    }
}