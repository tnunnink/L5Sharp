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
        content.Content.TargetName.Should().BeEmpty();
        content.Content.TargetType.Should().Be("Controller");
    }

    [Test]
    public Task New_ValidNameAndProcessor_ShouldBeVerified()
    {
        var content = L5X.New("Test", "1756-L83E");

        return VerifyXml(content.ToString())
            .ScrubInlineDateTimes("ddd MMM d HH:mm:ss yyyy")
            .ScrubMember("Owner");
    }

    [Test]
    public void Parse_ValidContent_ShouldNotBeNull()
    {
        var xml = XDocument.Load(Known.Test).ToString();

        var content = L5X.Parse(xml);

        content.Should().NotBeNull();
        content.Content.SchemaRevision.Should().Be("1.0");
        content.Content.SoftwareRevision.Should().Be("36.0");
        content.Content.TargetName.Should().Be("TestController");
        content.Content.TargetType.Should().Be("Controller");
        content.Content.ContainsContext.Should().Be(false);
        content.Content.ExportDate.Should().BeAfter(default);
    }

    [Test]
    public void Info_ValidContent_ShouldHaveExpectedValues()
    {
        var content = L5X.Load(Known.Test);

        content.Should().NotBeNull();
        content.Content.SchemaRevision.Should().Be("1.0");
        content.Content.SoftwareRevision.Should().Be("36.0");
        content.Content.TargetName.Should().Be("TestController");
        content.Content.TargetType.Should().Be("Controller");
        content.Content.ContainsContext.Should().Be(false);
        content.Content.ExportDate.Should().BeAfter(default);
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

        var code = content.Code(c => c.Scope.IsIn("MainProgram")).ToArray();

        code.Should().NotBeEmpty();
        code.Should().AllSatisfy(c => c.Reference.Scope.Container.Should().Be("MainProgram"));
    }

    [Test]
    public void Query_TypeNameOverload_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var tags = content.Query(ReferenceType.Tag).ToList();

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
    public void Query_PredicateOverload_ShouldReturnExpected()
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

        var result = content.Contains("tag://FakeTag");

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

        FluentActions.Invoking(() => content.Get(null!)).Should().Throw<ArgumentNullException>();
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
    public void TryGet_InvalidPathReference_ShouldThrowFormatException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.TryGet(Known.DataType, out _)).Should().Throw<FormatException>();
    }

    [Test]
    public void TryGet_ValidPathToKnownType_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet($"datatype://{Known.DataType}", out var entity);

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
    public Task Add_ToScopedContainer_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Test);
        var tag = new Tag("TestAdd", 123);

        content.Add(tag, Known.Program);

        return VerifyXml(content.Content.Serialize().ToString())
            .ScrubMember("ExportDate")
            .ScrubMember("LastModifiedDate");
    }

    [Test]
    public void Remove_ExistingComponent_ShouldReturnTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Remove<Tag>(Known.Tag);

        result.Should().BeTrue();
    }

    [Test]
    public void Remove_ExistingComponent_ShouldNotExist()
    {
        var content = L5X.Load(Known.Test);

        content.Remove<Tag>(Known.Tag);

        content.TryGet<Tag>(Known.Tag, out _).Should().BeFalse();
    }

    [Test]
    public void Remove_NonExistingComponent_ShouldReturnFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Remove<Tag>("FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public Task ToString_WhenCalled_ShouldBeValid()
    {
        var content = L5X.Load(Known.Empty);

        var result = content.ToString();

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