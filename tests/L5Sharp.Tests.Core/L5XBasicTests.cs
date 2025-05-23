﻿using System.Xml.Linq;
using FluentAssertions;
using JetBrains.dotMemoryUnit;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XBasicTests
{
    [Test]
    public void New_WithControllerAndProcessorNames_ShouldNotBeNullAndExpectedValues()
    {
        var content = L5X.New("ControllerName", "1756-L83E", new Revision(33, 1));

        content.Should().NotBeNull();
        content.Controller.Name.Should().Be("ControllerName");
        content.Controller.ProcessorType.Should().Be("1756-L83E");
        content.Controller.Revision.Should().BeEquivalentTo(new Revision(33, 1));
    }

    [Test]
    public Task New_ValidValues_ShouldBeVerified()
    {
        var content = L5X.New("ControllerName", "1756-L83E", 34.11);

        return VerifyXml(content.Serialize().ToString())
            .ScrubMember("ExportDate")
            .ScrubMember("Owner")
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate");
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => L5X.New(null!, "Test", new Revision())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullProcessor_ShouldThrowException()
    {
        FluentActions.Invoking(() => L5X.New("Test", null!, new Revision())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullRevision_ShouldThrowException()
    {
        FluentActions.Invoking(() => L5X.New("Test", "1756-L83E", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public Task New_ValidComponent_ShouldBeVerified()
    {
        var component = new DataType
        {
            Name = "TestType",
            Description = "This is a test component",
        };

        var content = L5X.New(component);

        return VerifyXml(content.Serialize().ToString())
            .ScrubMember("ExportDate")
            .ScrubMember("Owner")
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate");
    }

    [Test]
    public void New_NullComponent_shouldThrowException()
    {
        FluentActions.Invoking(() => L5X.New(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Empty_NoOverride_ShouldBeExpected()
    {
        var content = L5X.Empty();

        content.Should().NotBeNull();
        content.Info.TargetName.Should().BeEmpty();
        content.Info.TargetType.Should().Be("Controller");
    }

    [Test]
    public void Parse_ValidContent_ShouldNotBeNull()
    {
        var xml = XDocument.Load(Known.Test).ToString();

        var content = L5X.Parse(xml);

        content.Info.Should().NotBeNull();
        content.Info.SchemaRevision.Should().Be("1.0");
        content.Info.SoftwareRevision.Should().Be("32.02");
        content.Info.TargetName.Should().Be("TestController");
        content.Info.TargetType.Should().Be("Controller");
        content.Info.ContainsContext.Should().Be(false);
        content.Info.ExportDate.Should().NotBeNull();
    }

    [Test]
    public void Info_ValidContent_ShouldHaveExpectedValues()
    {
        var content = L5X.Load(Known.Test);

        content.Info.Should().NotBeNull();
        content.Info.SchemaRevision.Should().Be("1.0");
        content.Info.SoftwareRevision.Should().Be("32.02");
        content.Info.TargetName.Should().Be("TestController");
        content.Info.TargetType.Should().Be("Controller");
        content.Info.ContainsContext.Should().Be(false);
        content.Info.ExportDate.Should().NotBeNull();
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