using FluentAssertions;

namespace L5Sharp.Tests.Enums;

[TestFixture]
public class ScopeTypeTests
{
    [Test]
    public void Empty_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Empty;

        type.Should().NotBeNull();
        type.Value.Should().Be("");
    }
    
    [Test]
    public void DataType_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.DataType;

        type.Should().NotBeNull();
        type.Name.Should().Be("DataType");
        type.Value.Should().Be("DataType");
    }

    [Test]
    public void Instruction_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Instruction;

        type.Should().NotBeNull();
        type.Name.Should().Be("Instruction");
        type.Value.Should().Be("AddOnInstructionDefinition");
    }

    [Test]
    public void Module_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Module;

        type.Should().NotBeNull();
        type.Name.Should().Be("Module");
        type.Value.Should().Be("Module");
    }
    
    [Test]
    public void Tag_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Tag;

        type.Should().NotBeNull();
        type.Name.Should().Be("Tag");
        type.Value.Should().Be("Tag");
    }

    [Test]
    public void Program_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Program;

        type.Should().NotBeNull();
        type.Name.Should().Be("Program");
        type.Value.Should().Be("Program");
    }

    [Test]
    public void Routine_WhenCalled_ShouldBeExpected()
    {
        var type = ScopeType.Routine;

        type.Should().NotBeNull();
        type.Name.Should().Be("Routine");
        type.Value.Should().Be("Routine");
    }
}