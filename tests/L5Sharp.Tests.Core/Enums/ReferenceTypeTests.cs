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
        type.Value.Should().Be("");
    }
    
    [Test]
    public void DataType_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.DataType;

        type.Should().NotBeNull();
        type.Name.Should().Be("DataType");
        type.Value.Should().Be("DataType");
    }

    [Test]
    public void Aoi_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Aoi;

        type.Should().NotBeNull();
        type.Name.Should().Be("AddOnInstruction");
        type.Value.Should().Be("AddOnInstructionDefinition");
    }

    [Test]
    public void Module_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Module;

        type.Should().NotBeNull();
        type.Name.Should().Be("Module");
        type.Value.Should().Be("Module");
    }
    
    [Test]
    public void Tag_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Tag;

        type.Should().NotBeNull();
        type.Name.Should().Be("Tag");
        type.Value.Should().Be("Tag");
    }

    [Test]
    public void Program_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Program;

        type.Should().NotBeNull();
        type.Name.Should().Be("Program");
        type.Value.Should().Be("Program");
    }

    [Test]
    public void Routine_WhenCalled_ShouldBeExpected()
    {
        var type = ReferenceType.Routine;

        type.Should().NotBeNull();
        type.Name.Should().Be("Routine");
        type.Value.Should().Be("Routine");
    }
}