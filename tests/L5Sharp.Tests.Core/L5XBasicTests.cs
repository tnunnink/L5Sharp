using System.Xml.Linq;
using FluentAssertions;
using JetBrains.dotMemoryUnit;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XBasicTests
{
    [Test]
    public void Empty_NoOverride_ShouldBeExpected()
    {
        var content = L5X.Empty();

        content.Should().NotBeNull();
        content.Info.TargetName.Should().BeEmpty();
        content.Info.TargetType.Should().Be("Controller");
    }

    [Test]
    public Task New_ValidNameAndProcessor_ShouldBeVerified()
    {
        var content = L5X.New("Test", "1756-L83E");

        return VerifyXml(content.Serialize().ToString())
            .ScrubInlineDateTimes("ddd MMM d HH:mm:ss yyyy")
            .ScrubMember("Owner");
    }

    [Test]
    public void Parse_ValidContent_ShouldNotBeNull()
    {
        var xml = XDocument.Load(Known.Test).ToString();

        var content = L5X.Parse(xml);

        content.Should().NotBeNull();
        content.Info.SchemaRevision.Should().Be("1.0");
        content.Info.SoftwareRevision.Should().Be("32.02");
        content.Info.TargetName.Should().Be("TestController");
        content.Info.TargetType.Should().Be("Controller");
        content.Info.ContainsContext.Should().Be(false);
        content.Info.ExportDate.Should().BeAfter(default);
    }

    [Test]
    public void Info_ValidContent_ShouldHaveExpectedValues()
    {
        var content = L5X.Load(Known.Test);

        content.Should().NotBeNull();
        content.Info.SchemaRevision.Should().Be("1.0");
        content.Info.SoftwareRevision.Should().Be("32.02");
        content.Info.TargetName.Should().Be("TestController");
        content.Info.TargetType.Should().Be("Controller");
        content.Info.ContainsContext.Should().Be(false);
        content.Info.ExportDate.Should().BeAfter(default);
    }

    [Test]
    public void Components_WhenCalled_ShouldAllDeriveFromLogixComponent()
    {
        var content = L5X.Load(Known.Test);

        var components = content.Components().ToArray();

        components.Should().NotBeEmpty();
        components.Should().AllSatisfy(c => c.Should().BeAssignableTo<ILogixComponent>());
    }

    [Test]
    public void Components_WithPredicate_ShouldReturnExpected()
    {
        var content = L5X.Load(Known.Test);

        var components = content.Components(c => c.Name.Contains("Test")).ToArray();

        components.Should().NotBeEmpty();
        components.Should().AllSatisfy(c => c.Name.Should().Contain("Test"));
    }

    [Test]
    public void Code_WhenCalled_ShouldNotBeEmptyAndAssignableToLogixCode()
    {
        var content = L5X.Load(Known.Test);

        var code = content.Code().ToArray();

        code.Should().NotBeEmpty();
        code.Should().AllSatisfy(c => c.Should().BeAssignableTo<ILogixCode>());
    }

    [Test]
    public void Code_ValidPredicate_ShouldHaveExpectedResults()
    {
        var content = L5X.Load(Known.Test);

        var code = content.Code(c => c.Scope.IsLocalTo("MainProgram")).ToArray();

        code.Should().NotBeEmpty();
        code.Should().AllSatisfy(c => c.Reference.Container.Should().Be("MainProgram"));
    }

    [Test]
    public void Code_SelectUsagesInSpecificRoutine_ShouldReturnExpected()
    {
        var content = L5X.Load(Known.Example);

        var usages = content.Code().SelectMany(c => c.Usages()).Where(r => r.Routine == "Main").ToArray();

        usages.Should().NotBeEmpty();
        usages.Should().AllSatisfy(r => r.Routine.Should().Be("Main"));
    }

    [Test]
    public void Code_SelectUsagesWithStringArguments_ShouldReturnExpected()
    {
        var content = L5X.Load(Known.Example);

        var usages = content.Code()
            .SelectMany(c => c.Usages())
            .Where(r => r.Logic.Arguments.Any(a => a.Type == ArgumentType.String))
            .ToArray();

        usages.Should().NotBeEmpty();
    }

    [Test]
    public void Query_TypeNameOverload_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var tags = content.Query(nameof(Tag)).ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public void Query_TypeOverload_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var tags = content.Query(typeof(Tag)).ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public void Query_ContainsElement_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Query<Tag>().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Query_NoElement_ShouldBeEmpty()
    {
        var content = L5X.Load(Known.Empty);

        var results = content.Query<Tag>().ToList();

        results.Should().BeEmpty();
    }

    [Test]
    public void Query_PreidcateOverload_ShouldReturnExpected()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Query<Tag>(t => t.DataType == "TIMER").ToList();

        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(t => t.DataType.Should().Be("TIMER"));
    }

    [Test]
    public void Contains_KnownElement_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(Reference.To<Tag>(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains("/Tag/FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_KnownElementWithBuilder_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingWithBuilder_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag("FakeTag"));

        result.Should().BeFalse();
    }

    [Test]
    public void Get_KnownTagByReference_ShouldBeExpectedElement()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(Reference.To<Tag>(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_NullReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get((Reference)null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Get_EmptyReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Get_TypeAndName_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(Known.Tag);

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_BuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(x => x.Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_BuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(x => x.Tag(Known.Tag).In("MainProgram"));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_TypedBuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(x => x.Named(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_TypedBuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(x => x.Named(Known.Tag).In("MainProgram"));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }

    [Test]
    public void TryGet_NoTypeSpecified_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(Known.DataType, out var entity);

        result.Should().BeFalse();
        entity.Should().BeNull();
    }

    [Test]
    public void TryGet_KnownType_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(Reference.To<DataType>(Known.DataType), out var entity);

        result.Should().BeTrue();
        entity.Should().NotBeNull();
        entity.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_TypedKnownName_ShouldBeTrueAndExpectedComponent()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet<DataType>(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.DataType(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_TypedBuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet<DataType>(s => s.Named(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownTagElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.Tag(Known.Tag), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Add_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);
        var count = content.DataTypes.Count;
        var dataType = new DataType { Name = "TestAdd" };

        content.Add(dataType);

        content.DataTypes.Count.Should().Be(count + 1);
    }

    [Test]
    public Task Add_ValidComponent_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Test);
        var dataType = new DataType { Name = "TestAdd" };

        content.Add(dataType);

        return VerifyXml(content.DataTypes.Serialize().ToString()).ScrubMember("ExportDate");
    }

    [Test]
    public void Remove_ExistingComponent_ShouldNotExist()
    {
        var content = L5X.Load(Known.Test);

        content.Remove<Tag>(Known.Tag);

        content.TryGet<Tag>(Known.Tag, out _).Should().BeFalse();
    }

    [Test]
    public void Remove_ScopeBuilder_ShouldNotExist()
    {
        var content = L5X.Load(Known.Test);

        content.Remove(s => s.Tag(Known.Tag));

        content.TryGet<Tag>(Known.Tag, out _).Should().BeFalse();
    }

    [Test]
    public void Serialize_WhenCalled_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Empty);

        var result = content.Serialize();

        result.Should().NotBeNull();
    }

    [Test]
    public Task Serialize_WhenCalled_ShouldBeValid()
    {
        var content = L5X.Load(Known.Empty);

        var result = content.Serialize().ToString();

        return VerifyXml(result)
            .ScrubMember("ExportDate")
            .ScrubMember("Owner")
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate");
    }


    [DotMemoryUnit(FailIfRunWithoutSupport = false)]
    [Test]
    public void CheckForMemoryLeaksTest()
    {
        var isolator = new Action(() =>
        {
            // ReSharper disable once RedundantAssignment
            var content = L5X.Load(Known.Test);

            var tags = content.Query<Tag>().Where(t => t.TagName.Contains("Test"));
            tags.Should().NotBeEmpty();

            content = null;
            content.Should().BeNull();
        });

        isolator();

        GC.Collect();
        GC.WaitForFullGCComplete();

        // Assert L5X is removed from memory
        dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<L5X>()).ObjectsCount.Should().Be(0));
    }
}