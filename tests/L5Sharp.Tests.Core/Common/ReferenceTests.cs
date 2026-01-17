using System.Diagnostics;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class ReferenceTests
{
    [Test]
    public void TagScope_ControllerTag_ShouldBeExpected()
    {
        var component = new Tag("Test", 100);

        var reference = component.Reference;

        reference.Path.Should().Be("tag://Test");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Should().Be(Scope.None);
        reference.Id.Should().Be("Test");
    }
    
    [Test]
    public void TagScope_ProgramTag_ShouldBeExpected()
    {
        //This factory method will create the tag inside a virtual program container.
        var component = Tag.New<DINT>("Program:SomeProgram.MyTagName");

        var reference = component.Reference;

        reference.Path.Should().Be("tag://SomeProgram/MyTagName");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Level.Should().Be(ScopeLevel.Program);
        reference.Scope.Container.Should().Be("SomeProgram");
        reference.Id.Should().Be("MyTagName");
    }

    [Test]
    public void RungScope_InMemory_ShouldBeExpected()
    {
        var element = new Rung("XIC(MyTag)OTE(OtherTag)");

        var reference = element.Reference;

        reference.Path.Should().Be("rung://0");
        reference.Type.Should().Be(ReferenceType.Rung);
        reference.Scope.Should().Be(Scope.None);
        reference.Id.Should().Be("0");
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
    public void To_ControllerScopeTag_ShouldBeExpected()
    {
        var reference = Reference.To("tag://my_tag_name");

        reference.Path.Should().Be("tag://my_tag_name");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("my_tag_name");
    }

    [Test]
    public void To_PathWithDataTypeTarget_ShouldBeExpectedScope()
    {
        const string path = "datatype://TypeName";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.DataType);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TypeName");
    }

    [Test]
    public void To_PathWithModuleTarget_ShouldBeExpectedScope()
    {
        const string path = "module://ModuleName";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.Module);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("ModuleName");
    }

    [Test]
    public void To_PathWithInstructionTarget_ShouldBeExpectedScope()
    {
        const string path = "aoi://TypeName";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.Aoi);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TypeName");
    }

    [Test]
    public void To_PathWithControllerTagTarget_ShouldBeExpectedScope()
    {
        const string path = "tag://MyTag";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("MyTag");
    }

    [Test]
    public void To_PathWithProgramTagTarget_ShouldBeExpectedScope()
    {
        const string path = "tag://MyProgram/TagName.Member.Something";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Level.Should().Be(ScopeLevel.Program);
        reference.Scope.Container.Should().Be("MyProgram");
        reference.Id.Should().Be("TagName.Member.Something");
    }

    [Test]
    public void To_PathWithRungTarget_ShouldBeExpected()
    {
        const string path = "rung://MyProgram/MyRoutine/12";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.Should().Be(ReferenceType.Rung);
        reference.Scope.Level.Should().Be(ScopeLevel.Program);
        reference.Scope.Container.Should().Be("MyProgram");
        reference.Id.Should().Be("12");
        reference.Fragment.Should().BeNull();
    }

    [Test]
    public void To_RungWithInstructionFragment_ShouldBeExpectedScope()
    {
        const string path = "rung://MyProgram/MyRoutine/1#XIC(LocalTag)";

        var reference = Reference.To(path);

        reference.Path.Should().Be(path);
        reference.Type.IsLogic.Should().BeTrue();
        reference.Scope.Container.Should().Be("MyProgram");
        reference.Scope.Routine.Should().Be("MyRoutine");
        reference.Id.Should().Be("1");
        reference.Fragment.Should().Be("XIC(LocalTag)");
    }

    [Test]
    public void To_GenericDataTypeTarget_ShouldBeExpected()
    {
        var reference = Reference.To<DataType>("TargetName");

        reference.Path.Should().Be("datatype://TargetName");
        reference.Type.Should().Be(ReferenceType.DataType);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericModuleTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Module>("TargetName");

        reference.Path.Should().Be("module://TargetName");
        reference.Type.Should().Be(ReferenceType.Module);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericAoiTarget_ShouldBeExpected()
    {
        var reference = Reference.To<AddOnInstruction>("TargetName");

        reference.Path.Should().Be("aoi://TargetName");
        reference.Type.Should().Be(ReferenceType.Aoi);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericControllerTagTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TargetName");

        reference.Path.Should().Be("tag://TargetName");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Should().Be(Scope.Controller);
        reference.Id.Should().Be("TargetName");
    }

    [Test]
    public void To_GenericProgramTagTarget_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TargetName", Scope.Program("MyProgram"));

        reference.Path.Should().Be("tag://MyProgram/TargetName");
        reference.Type.Should().Be(ReferenceType.Tag);
        reference.Scope.Container.Should().Be("MyProgram");
        reference.Scope.Level.Should().Be(ScopeLevel.Program);
        reference.Id.Should().Be("TargetName");
    }

    [Test]
    public void ToScope_GlobalToLocal_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TagName");

        var result = reference.ToScope(Scope.Program("MyProgram"));

        result.Path.Should().Be("tag://MyProgram/TagName");
        result.Type.Should().Be(ReferenceType.Tag);
        result.Scope.Container.Should().Be("MyProgram");
        result.Scope.Level.Should().Be(ScopeLevel.Program);
        result.Id.Should().Be("TagName");
    }

    [Test]
    public void ToScope_LocalToGlobal_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TagName", Scope.Program("SomeProgram"));

        var result = reference.ToScope(Scope.Controller);

        result.Path.Should().Be("tag://TagName");
        result.Type.Should().Be(ReferenceType.Tag);
        result.Scope.Container.Should().BeEmpty();
        result.Scope.Level.Should().Be(ScopeLevel.Controller);
        result.Id.Should().Be("TagName");
    }

    [Test]
    public void ToScope_LocalToOtherLocal_ShouldBeExpected()
    {
        var reference = Reference.To<Tag>("TagName", Scope.Program("SomeProgram"));

        var result = reference.ToScope(Scope.Program("MyProgram"));

        result.Path.Should().Be("tag://MyProgram/TagName");
        result.Type.Should().Be(ReferenceType.Tag);
        result.Scope.Container.Should().Be("MyProgram");
        result.Scope.Level.Should().Be(ScopeLevel.Program);
        result.Id.Should().Be("TagName");
    }

    [Test]
    public void ToScope_RungToDifferentRoutine_ShouldBeExpected()
    {
        var reference = Reference.To<Rung>(1, "MyProgram", "MyRoutine");

        var result = reference.ToScope(Scope.Program("OtherProgram", "OtherRoutine"));

        result.Type.Should().Be(ReferenceType.Rung);
        result.Scope.Level.Should().Be(ScopeLevel.Program);
        result.Scope.Container.Should().Be("OtherProgram");
        result.Scope.Routine.Should().Be("OtherRoutine");
        result.Id.Should().Be("1");
    }

    [Test]
    public void PerformanceOfGettingAllTagMemberReferencesShouldBeNotTerrible()
    {
        var content = L5X.Load(Known.Example);

        var stopwatch = Stopwatch.StartNew();

        var references = content.Query<Tag>()
            .SelectMany(t => t.Members())
            .Select(t => t.Reference)
            .ToList();

        stopwatch.Stop();

        references.Should().NotBeEmpty();
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(1));
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
        references.Should().AllSatisfy(s => s.Id.Should().NotBe(TagName.Empty));
        Console.WriteLine(references.Count);
    }
}