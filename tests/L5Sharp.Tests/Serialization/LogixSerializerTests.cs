using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Serialization;
using Task = L5Sharp.Components.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class LogixSerializerTests
    {
        [Test]
        public void GetSerializer_DataType_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<DataType>();

            serializer.Should().BeOfType<DataTypeSerializer>();
        }
        
        [Test]
        public void GetSerializer_AddOnInstruction_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<AddOnInstruction>();

            serializer.Should().BeOfType<AddOnInstructionSerializer>();
        }
        
        [Test]
        public void GetSerializer_Module_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<Module>();

            serializer.Should().BeOfType<ModuleSerializer>();
        }
        
        [Test]
        public void GetSerializer_Tag_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<Tag>();

            serializer.Should().BeOfType<TagSerializer>();
        }
        
        [Test]
        public void GetSerializer_Program_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<Program>();

            serializer.Should().BeOfType<ProgramSerializer>();
        }
        
        [Test]
        public void GetSerializer_Routine_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<Routine>();

            serializer.Should().BeOfType<RoutineSerializer>();
        }
        
        [Test]
        public void GetSerializer_Task_ShouldBeExpectedType()
        {
            var serializer = LogixSerializer.GetSerializer<Task>();

            serializer.Should().BeOfType<TaskSerializer>();
        }
    }
}