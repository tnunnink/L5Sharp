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
        var xml = TestContent.Test.Content.Serialize().ToString();

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
        var content = TestContent.Test;

        content.Should().NotBeNull();
        content.Content.SchemaRevision.Should().Be("1.0");
        content.Content.SoftwareRevision.Should().Be("36.0");
        content.Content.TargetName.Should().Be("TestController");
        content.Content.TargetType.Should().Be("Controller");
        content.Content.ContainsContext.Should().Be(false);
        content.Content.ExportDate.Should().BeAfter(default);
    }

    [Test]
    public void Query_TypeNameOverload_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var tags = content.Query(ReferenceType.Tag).ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public void Query_ContainsElement_ShouldNotBeEmpty()
    {
        var content = TestContent.Test;

        var results = content.Query<Tag>().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void Query_NoElement_ShouldBeEmpty()
    {
        var content = TestContent.Empty;

        var results = content.Query<Tag>().ToList();

        results.Should().BeEmpty();
    }

    [Test]
    public void Query_PredicateOverload_ShouldReturnExpected()
    {
        var content = TestContent.Test;

        var results = content.Query<Tag>(t => t.DataType == "TIMER").ToList();

        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(t => t.DataType.Should().Be("TIMER"));
    }

    [Test]
    public void Query_ComponentInterface_ShouldHaveExpectedTypes()
    {
        var content = TestContent.Test;

        var results = content.Query<ILogixComponent>().ToList();

        results.Any(r => r is DataType).Should().BeTrue();
        results.Any(r => r is AddOnInstruction).Should().BeTrue();
        results.Any(r => r is Module).Should().BeTrue();
        results.Any(r => r is Program).Should().BeTrue();
        results.Any(r => r is Tag).Should().BeTrue();
        results.Any(r => r is Routine).Should().BeTrue();
        results.Any(r => r is LTask).Should().BeTrue();
    }

    [Test]
    public void Query_EntityInterface_ShouldHaveExpectedTypes()
    {
        var content = TestContent.Test;

        var results = content.Query<ILogixEntity>().ToList();

        results.Any(r => r is DataType).Should().BeTrue();
        results.Any(r => r is AddOnInstruction).Should().BeTrue();
        results.Any(r => r is Module).Should().BeTrue();
        results.Any(r => r is Program).Should().BeTrue();
        results.Any(r => r is Tag).Should().BeTrue();
        results.Any(r => r is Routine).Should().BeTrue();
        results.Any(r => r is LTask).Should().BeTrue();
        results.Any(r => r is Parameter).Should().BeTrue();
        results.Any(r => r is LocalTag).Should().BeTrue();
        results.Any(r => r is DataTypeMember).Should().BeTrue();
        results.Any(r => r is Rung).Should().BeTrue();
        results.Any(r => r is Line).Should().BeTrue();
        results.Any(r => r is Sheet).Should().BeTrue();
    }

    [Test]
    public void Contains_KnownElement_ShouldBeTrue()
    {
        var content = TestContent.Test;

        var result = content.Contains(Reference.To<Tag>(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = TestContent.Test;

        var result = content.Contains("tag://FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public void Get_KnownTagByReference_ShouldBeExpectedElement()
    {
        var content = TestContent.Test;

        var result = content.Get(Reference.To<Tag>(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingReference_ShouldThrowException()
    {
        var content = TestContent.Test;

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_NullReference_ShouldThrowException()
    {
        var content = TestContent.Test;

        FluentActions.Invoking(() => content.Get(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Get_EmptyReference_ShouldThrowException()
    {
        var content = TestContent.Test;

        FluentActions.Invoking(() => content.Get(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Get_TypeAndName_ShouldBeExpected()
    {
        var content = TestContent.Test;

        var result = content.Get<Tag>(Known.Tag);

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingName_ShouldThrowException()
    {
        var content = TestContent.Test;

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void TryGet_InvalidPathReference_ShouldThrowFormatException()
    {
        var content = TestContent.Test;

        FluentActions.Invoking(() => content.TryGet(Known.DataType, out _)).Should().Throw<FormatException>();
    }

    [Test]
    public void TryGet_ValidPathToKnownType_ShouldBeTrue()
    {
        var content = TestContent.Test;

        var result = content.TryGet($"datatype://{Known.DataType}", out var entity);

        result.Should().BeTrue();
        entity.Should().NotBeNull();
        entity.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_TypedKnownName_ShouldBeTrueAndExpectedComponent()
    {
        var content = TestContent.Test;

        var result = content.TryGet<DataType>(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void Add_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = TestContent.Test;
        var count = content.DataTypes.Count;
        var dataType = new DataType { Name = "TestAdd" };

        content.Add(dataType);

        content.DataTypes.Count.Should().Be(count + 1);
    }

    [Test]
    public Task Add_ValidComponent_ShouldBeVerified()
    {
        var content = TestContent.Test;
        var dataType = new DataType { Name = "TestAdd" };

        content.Add(dataType);

        return VerifyXml(content.DataTypes.Serialize().ToString()).ScrubMember("ExportDate");
    }

    [Test]
    public void Remove_ExistingComponent_ShouldReturnTrue()
    {
        var content = TestContent.Test;

        var result = content.Remove<Tag>(Known.Tag);

        result.Should().BeTrue();
    }

    [Test]
    public void Remove_ExistingComponent_ShouldNotExist()
    {
        var content = TestContent.Test;

        content.Remove<Tag>(Known.Tag);

        content.TryGet<Tag>(Known.Tag, out _).Should().BeFalse();
    }

    [Test]
    public void Remove_NonExistingComponent_ShouldReturnFalse()
    {
        var content = TestContent.Test;

        var result = content.Remove<Tag>("FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public Task ToString_WhenCalled_ShouldBeValid()
    {
        var content = TestContent.Empty;

        var result = content.ToString();

        return VerifyXml(result)
            .ScrubMember("ExportDate")
            .ScrubMember("Owner")
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate");
    }

    /// <summary>
    /// This was to test a bug I found in the code for creating new LogixContainer
    /// that need proper callback to add to the L5X tree to get parents.
    /// </summary>
    [Test]
    public void GetParent_FromConnection_ShouldNotBeNull()
    {
        var content = TestContent.Test;

        var connections = content.Modules.SelectMany(m => m.Connections).ToList();

        var invalid = connections.Where(c => c.GetParent<Module>() is null);

        invalid.Should().BeEmpty();
    }


    [DotMemoryUnit(FailIfRunWithoutSupport = false)]
    [Test]
    public void CheckForMemoryLeaksTest()
    {
        var isolator = new Action(() =>
        {
            // ReSharper disable once RedundantAssignment
            var content = TestContent.Test;

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