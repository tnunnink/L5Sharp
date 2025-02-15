using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class ScopeLevelTests
    {
        [Test]
        public void New_Controller_ShouldBeExpected()
        {
            var sut = ScopeLevel.Controller;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("ControllerScope");
        }
        
        [Test]
        public void New_Program_ShouldBeExpected()
        {
            var sut = ScopeLevel.Program;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("ProgramScope");
        }
        
        [Test]
        public void New_Null_ShouldBeExpected()
        {
            var sut = ScopeLevel.Null;

            sut.Should().NotBeNull();
            sut.Value.Should().Be("NullScope");
        }
    }
}