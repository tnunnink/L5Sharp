using FluentAssertions;
using L5Sharp.CLI.Schemas;
using L5Sharp.Core;

namespace L5Sharp.Tests.CLI.Projection;

[TestFixture]
public class DataTypeSchemaTests
{
    [Test]
    public void Emit_SingleElementAllFields_ShouldBeNotBeNull()
    {
        var projector = new DataTypeSchema();

        var result = projector.Map(new DataType());

        result.Should().NotBeNull();
    }

    [Test]
    public void Emit_SingleElementAllFields_ShouldHaveExpectedFields()
    {
        var projector = new DataTypeSchema();

        var result = projector.Map(new DataType
        {
            Name = "Test",
            Description = "this is a test",
            Class = DataTypeClass.Predefined,
            Members = [new DataTypeMember()],
            Use = Use.Context
        });

        result.Should().Contain("Name", "Test");
        result.Should().Contain("Description", "this is a test");
        result.Should().Contain("Class", DataTypeClass.Predefined);
        result.Should().Contain("Family", DataTypeFamily.None);
        result.Should().Contain("Members", 1);
        result.Should().Contain("Reference", "datatype://Test");
        result.Should().Contain("Use", Use.Context);
    }

    [Test]
    public void Emit_SingleElementSelectFields_ShouldContainExpectedFields()
    {
        var projector = new DataTypeSchema();

        var result = projector.Map(new DataType { Name = "Test" }, "name", "description", "reference");

        result.Should().HaveCount(3);
        result.Should().ContainKey("Name");
        result.Should().ContainKey("Description");
        result.Should().ContainKey("Reference");
    }
}