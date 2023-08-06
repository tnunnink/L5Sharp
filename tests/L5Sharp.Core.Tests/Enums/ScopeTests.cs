using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests.Enums
{
    [TestFixture]
    public class ScopeTests
    {
        [Test]
        public void New_Controller_ShouldBeExpected()
        {
            var sut = Scope.Controller;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("ControllerScope");
        }
        
        [Test]
        public void New_Program_ShouldBeExpected()
        {
            var sut = Scope.Program;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("ProgramScope");
        }
        
        [Test]
        public void New_Routine_ShouldBeExpected()
        {
            var sut = Scope.Routine;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("RoutineScope");
        }
        
        [Test]
        public void New_Null_ShouldBeExpected()
        {
            var sut = Scope.Null;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("NullScope");
        }
        
        [Test]
        public void New_Instruction_ShouldBeExpected()
        {
            var sut = Scope.Instruction;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("InstructionScope");
        }
    }
}