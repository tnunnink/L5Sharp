using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enumerations.Tests
{
    [TestFixture]
    public class ScopeTests
    {
        [Test]
        public void New_Controller_ShouldBeExpected()
        {
            var sut = Scope.Controller;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("ControllerScope");
        }
        
        [Test]
        public void New_Program_ShouldBeExpected()
        {
            var sut = Scope.Program;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("ProgramScope");
        }
        
        [Test]
        public void New_Routine_ShouldBeExpected()
        {
            var sut = Scope.Routine;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("RoutineScope");
        }
        
        [Test]
        public void New_Null_ShouldBeExpected()
        {
            var sut = Scope.Null;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("NullScope");
        }
    }
}