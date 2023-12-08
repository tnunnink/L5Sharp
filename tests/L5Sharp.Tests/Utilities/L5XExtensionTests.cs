using FluentAssertions;

namespace L5Sharp.Tests.Utilities;

[TestFixture]
public class L5XExtensionTests
{
    [Test]
    public void L5XType_AddOnInstruction_ShouldBeExpectedValue()
    {
        var type = typeof(AddOnInstruction).L5XType();

        type.Should().Be("AddOnInstructionDefinition");
    }
    
    [Test]
    public void L5XType_DataType_ShouldBeExpectedValue()
    {
        var type = typeof(DataType).L5XType();

        type.Should().Be("DataType");
    }
    
    [Test]
    public void L5XType_Module_ShouldBeExpectedValue()
    {
        var type = typeof(Module).L5XType();

        type.Should().Be("Module");
    }
    
    [Test]
    public void L5XType_Program_ShouldBeExpectedValue()
    {
        var type = typeof(Program).L5XType();

        type.Should().Be("Program");
    }
    
    [Test]
    public void L5XType_Routine_ShouldBeExpectedValue()
    {
        var type = typeof(Routine).L5XType();

        type.Should().Be("Routine");
    }
    
    [Test]
    public void L5XType_Tag_ShouldBeExpectedValue()
    {
        var type = typeof(Tag).L5XType();

        type.Should().Be("Tag");
    }
    
    [Test]
    public void L5XType_Task_ShouldBeExpectedValue()
    {
        var type = typeof(Task).L5XType();

        type.Should().Be("Task");
    }

    [Test]
    public void L5XTypes_Tag_ShouldHaveExpectedCount()
    {
        var types = typeof(Tag).L5XTypes().ToList();

        types.Should().NotBeEmpty();
        types.Should().ContainSingle(s => s == "Tag");
        types.Should().ContainSingle(s => s == "LocalTag");
        types.Should().ContainSingle(s => s == "ConfigTag");
        types.Should().ContainSingle(s => s == "InputTag");
        types.Should().ContainSingle(s => s == "OutputTag");
    }

    [Test]
    public void L5XContainerType_Tag_ShouldHaveExpectedValue()
    {
        var type = typeof(Tag).L5XContainer();

        type.Should().Be("Tags");
    }
}