using System.Diagnostics;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class ReferenceTests
{
    [Test]
    public void TagScope_InMemory_ShouldBeExpected()
    {
        var component = new Tag("Test", 100);

        var reference = component.Reference;

        reference.Path.Should().Be("Tag[@Name='Test']");
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("Test");
    }

    [Test]
    public void RungScope_InMemory_ShouldBeExpected()
    {
        var element = new Rung("XIC(MyTag)OTE(OtherTag)");

        var reference = element.Reference;

        reference.Path.Should().Be("Rung[@Number='0']");
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Rung);
        reference.Location.Should().Be("0");
    }

    [Test]
    public void To_Null_ShouldThrowException()
    {
        FluentActions.Invoking(() => Reference.To((string)null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void To_Empty_ShouldThrowException()
    {
        FluentActions.Invoking(() => Reference.To(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void To_RelativeTargetPath_ShouldBeExpected()
    {
        var reference = Reference.To("/Tags/Tag[@Name='my_tag_name']");


        reference.Path.Should().Be("/Tags/Tag[@Name='my_tag_name']");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("my_tag_name");
        reference.Container.Should().BeEmpty();
        reference.Routine.Should().BeEmpty();
    }

    [Test]
    public void To_PathWithDataTypeTarget_ShouldBeExpectedScope()
    {
        const string path = "Controller/DataTypes/DataType[@Name='TypeName']";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.DataType);
        reference.Location.Should().Be("TypeName");
        reference.IsGlobal.Should().BeTrue();
    }

    [Test]
    public void To_PathWithModuleTarget_ShouldBeExpectedScope()
    {
        const string path = "Controller/Modules/Module[@Name='ModuleName']";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Module);
        reference.Location.Should().Be("ModuleName");
        reference.IsGlobal.Should().BeTrue();
    }

    [Test]
    public void To_PathWithInstructionTarget_ShouldBeExpectedScope()
    {
        const string path = "Controller/AddOnInstructionDefinitions/AddOnInstructionDefinition[@Name='TypeName']";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Aoi);
        reference.Location.Should().Be("TypeName");
    }

    [Test]
    public void To_PathWithControllerTagTarget_ShouldBeExpectedScope()
    {
        const string path = "Controller/Tags/Tag[@Name='myTag.Member.1']";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("myTag.Member.1");
    }

    [Test]
    public void To_PathWithProgramTagTarget_ShouldBeExpectedScope()
    {
        const string path = "Controller/Programs/Program[@Name='MyProgram']/Tags/Tag[@Name='TagName.Member.Something']";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().Be("MyProgram");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("TagName.Member.Something");
    }

    [Test]
    public void To_InstructionTextTarget_ShouldBeExpectedScope()
    {
        const string path =
            "Controller/Programs/Program[@Name='MyProgram']/Routines/Routine[@Name='MyRoutine']/RLLContent/Rung[@Number='1']/Text[contains(text(), 'XIC(LocalTag)')]";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Container.Should().Be("MyProgram");
        reference.Routine.Should().Be("MyRoutine");
        reference.Type.Should().Be(ReferenceType.Rung);
        reference.Location.Should().Be("1");
        reference.IsLocal.Should().BeTrue();
        reference.IsLogic.Should().BeTrue();
        reference.Logic.Should().NotBeNull();
        reference.Logic.ToString().Should().Be("XIC(LocalTag)");
        reference.Logic.Tags.Should().Contain("LocalTag");
    }

    [Test]
    public void To_GenericDataTypeTarget_ShouldBeExpected()
    {
        var reference = Reference.To<DataType>("TargetName");

        reference.Path.Should().Be("Controller/DataTypes/DataType[@Name='TargetName']");
        reference.Type.Should().Be(ReferenceType.DataType);
        reference.Location.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericModuleTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Module>("TargetName");

        reference.Path.Should().Be("Controller/Modules/Module[@Name='TargetName']");
        reference.Type.Should().Be(ReferenceType.Module);
        reference.Location.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericAoiTarget_ShouldBeExpected()
    {
        var reference = Reference.To<AddOnInstruction>("TargetName");

        reference.Path.Should()
            .Be("Controller/AddOnInstructionDefinitions/AddOnInstructionDefinition[@Name='TargetName']");
        reference.Type.Should().Be(ReferenceType.Aoi);
        reference.Location.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericControllerTagTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TargetName");

        reference.Path.Should().Be("Controller/Tags/Tag[@Name='TargetName']");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("TargetName");
        reference.Container.Should().BeEmpty();
    }

    [Test]
    public void To_GenericProgramTagTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TargetName", "MyProgram");

        reference.Path.Should().Be("Controller/Programs/Program[@Name='MyProgram']/Tags/Tag[@Name='TargetName']");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Container.Should().Be("MyProgram");
        reference.Location.Should().Be("TargetName");
    }

    [Test]
    public void ScopeTo_GlobalTagToLocalTag_ShouldBeExpected()
    {
        var global = Reference.To<Tag>("TagName");
        var local = Reference.To<Tag>("SomeOtherTag", "MyProgram");

        var result = global.ToScope(local);

        result.Path.Should().Be("Controller/Programs/Program[@Name='MyProgram']/Tags/Tag[@Name='TagName']");
        result.IsLocal.Should().BeTrue();
        result.Container.Should().Be(local.Container);
        result.Type.Should().Be(ReferenceType.Tag);
        result.Location.Should().Be("TagName");
    }

    [Test]
    public void ScopeTo_RoutineToScopeOfCode_ShouldBeExpected()
    {
        var code = Reference.To<Rung>(1, "MyProgram", "MyRoutine");
        var routine = Reference.To<Routine>("MyRoutine");

        var result = routine.ToScope(code);

        result.Type.Should().Be(ReferenceType.Routine);
        result.Location.Should().Be("MyRoutine");
        result.Container.Should().Be("MyProgram");
        result.Routine.Should().BeEmpty();
        result.IsLocal.Should().BeTrue();
    }

    [Test]
    public void ScopeTo_CodeScopedToScopedCode_ShouldBeExpected()
    {
        var local = Reference.To<Rung>(1, "MyProgram", "MyRoutine");
        var unscoped = Reference.To("Rung[@Number='2']");

        var result = unscoped.ToScope(local);

        result.Type.Should().Be(ReferenceType.Rung);
        result.Location.Should().Be("2");
        result.Container.Should().Be("MyProgram");
        result.Routine.Should().Be("MyRoutine");
        result.IsLocal.Should().BeTrue();
        result.IsLogic.Should().BeTrue();
    }

    [Test]
    public void ScopeTo_LocalToGlobal_ShouldBeExpected()
    {
        var global = Reference.To<Tag>("TagName");
        var local = Reference.To<Tag>("SomeOtherTag", "MyProgram");

        var result = local.ToScope(global);

        result.Path.Should().Be("Controller/Tags/Tag[@Name='SomeOtherTag']");
        result.IsGlobal.Should().BeTrue();
        result.Container.Should().BeEmpty();
        result.Routine.Should().BeEmpty();
        result.Type.Should().Be(ReferenceType.Tag);
        result.Location.Should().Be("SomeOtherTag");
    }

    [Test]
    public void ScopeTo_ValidScopeOverload_ShouldBeExpected()
    {
        var tag = new Tag("Test", 123);
        var target = Reference.To<Tag>("SomeOtherTag", "MyProgram");

        var result = target.ToScope(tag.Scope);

        result.IsGlobal.Should().BeTrue();
        result.Container.Should().BeEmpty();
        result.Type.Should().Be(ReferenceType.Tag);
        result.Location.Should().Be("SomeOtherTag");
    }

    [Test]
    public void Build_TypeAndName_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Tag("MyTagName"));

        reference.Path.Should().Be("Controller/Tags/Tag[@Name='MyTagName']");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("MyTagName");
    }

    [Test]
    public void Build_DataType_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.DataType("MyType"));

        reference.Path.Should().Be("Controller/DataTypes/DataType[@Name='MyType']");
        reference.Type.Should().Be(ReferenceType.DataType);
        reference.Location.Should().Be("MyType");
    }

    [Test]
    public void Build_Aoi_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Aoi("MyType"));

        reference.Path.Should().Be("Controller/AddOnInstructionDefinitions/AddOnInstructionDefinition[@Name='MyType']");
        reference.Type.Should().Be(ReferenceType.Aoi);
        reference.Location.Should().Be("MyType");
    }

    [Test]
    public void Build_Module_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Module("ModuleName"));

        reference.Path.Should().Be("Controller/Modules/Module[@Name='ModuleName']");
        reference.Type.Should().Be(ReferenceType.Module);
        reference.Location.Should().Be("ModuleName");
    }

    [Test]
    public void Build_Tag_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Tag("MyTagName"));

        reference.Path.Should().Be("Controller/Tags/Tag[@Name='MyTagName']");
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("MyTagName");
    }

    [Test]
    public void Build_Program_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Program("MainProgram"));

        reference.Path.Should().Be("Controller/Programs/Program[@Name='MainProgram']");
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Program);
        reference.Location.Should().Be("MainProgram");
    }

    [Test]
    public void Build_Task_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b.Task("TaskName"));

        reference.Path.Should().Be("Controller/Tasks/Task[@Name='TaskName']");
        reference.Container.Should().BeEmpty();
        reference.Type.Should().Be(ReferenceType.Task);
        reference.Location.Should().Be("TaskName");
    }

    [Test]
    public void Build_ProgramTypeAndName_ShouldBeExpected()
    {
        var reference = Reference.Build(b => b
            .Type(ReferenceType.Tag)
            .Named("MyTagName")
            .In("MyProgram")
        );

        reference.Path.Should().Be("Controller/Programs/Program[@Name='MyProgram']/Tags/Tag[@Name='MyTagName']");
        reference.Container.Should().Be("MyProgram");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Location.Should().Be("MyTagName");
    }

    [Test]
    public void Build_ProgramRoutineTypeAndName_ShouldBeExpected()
    {
        const string expected =
            "Controller/Programs/Program[@Name='MyProgram']/Routines/Routine[@Name='MyRoutine']/RLLContent/Rung[@Number='23']";

        var reference = Reference.Build(b => b
            .Type(ReferenceType.Rung)
            .Number(23)
            .In("MyProgram")
            .In("MyRoutine")
        );

        reference.Path.Should().Be(expected);
        reference.Container.Should().Be("MyProgram");
        reference.Type.Should().Be(ReferenceType.Rung);
        reference.Location.Should().Be("23");
    }

    [Test]
    public void PerformanceOfGettingAllTagMemberScopesShouldBeNotTerrible()
    {
        var content = L5X.Load(Known.Example);

        var stopwatch = Stopwatch.StartNew();

        var references = content.Query<Tag>()
            .SelectMany(t => t.Members())
            .Select(t => t.Reference)
            .ToList();

        stopwatch.Stop();

        references.Should().NotBeEmpty();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    [Test]
    public void AllTagMembersShouldHaveTypeAndNameScopedProperty()
    {
        var content = L5X.Load(Known.Test);

        var references = content.Query<Tag>()
            .SelectMany(t => t.Members())
            .Select(t => t.Reference)
            .ToList();

        references.Should().NotBeEmpty();
        references.Should().AllSatisfy(s => s.Type.Should().Be(ReferenceType.Tag));
        references.Should().AllSatisfy(s => s.Location.Should().NotBe(TagName.Empty));
        Console.WriteLine(references.Count);
    }
}