using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class ReferenceTypeTests
{
    [Test]
    public void Empty_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Null;

        type.Should().NotBeNull();
        type.Name.Should().Be("Null");
        type.Value.Should().Be("");
    }

    [Test]
    public void DataType_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.DataType;

        type.Should().NotBeNull();
        type.Name.Should().Be("datatype");
        type.Value.Should().Be("DataType");
    }

    [Test]
    public void Aoi_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Aoi;

        type.Should().NotBeNull();
        type.Name.Should().Be("aoi");
        type.Value.Should().Be("AddOnInstructionDefinition");
    }
    
    [Test]
    public void Parameter_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Parameter;

        type.Should().NotBeNull();
        type.Name.Should().Be("parameter");
        type.Value.Should().Be("Parameter");
    }
    
    [Test]
    public void LocalTag_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.LocalTag;

        type.Should().NotBeNull();
        type.Name.Should().Be("localtag");
        type.Value.Should().Be("LocalTag");
    }

    [Test]
    public void Module_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Module;

        type.Should().NotBeNull();
        type.Name.Should().Be("module");
        type.Value.Should().Be("Module");
    }

    [Test]
    public void Tag_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Tag;

        type.Should().NotBeNull();
        type.Name.Should().Be("tag");
        type.Value.Should().Be("Tag");
    }

    [Test]
    public void Program_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Program;

        type.Should().NotBeNull();
        type.Name.Should().Be("program");
        type.Value.Should().Be("Program");
    }

    [Test]
    public void Routine_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Routine;

        type.Should().NotBeNull();
        type.Name.Should().Be("routine");
        type.Value.Should().Be("Routine");
    }
    
    [Test]
    public void Rung_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Rung;

        type.Should().NotBeNull();
        type.Name.Should().Be("rung");
        type.Value.Should().Be("Rung");
    }
    
    [Test]
    public void Line_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Line;

        type.Should().NotBeNull();
        type.Name.Should().Be("line");
        type.Value.Should().Be("Line");
    }
    
    [Test]
    public void Sheet_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Sheet;

        type.Should().NotBeNull();
        type.Name.Should().Be("sheet");
        type.Value.Should().Be("Sheet");
    }

    [Test]
    public void FromType_DataType_ShouldBeExpected()
    {
        var type = ReferenceType.FromType<DataType>();

        type.Should().Be(ReferenceType.DataType);
    }
}