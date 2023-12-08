using FluentAssertions;

namespace L5Sharp.Tests.Enums
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
        public void New_Null_ShouldBeExpected()
        {
            var sut = Scope.Null;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("NullScope");
        }
    }
}