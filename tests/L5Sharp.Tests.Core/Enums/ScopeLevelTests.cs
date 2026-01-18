using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class ScopeLevelTests
    {
        [Test]
        public void None_WhenCalled_ShouldBeExpected()
        {
            var level = ScopeLevel.None;

            level.Should().NotBeNull();
            level.Name.Should().Be("None");
            level.Value.Should().Be("None");
        }

        [Test]
        public void Controller_WhenCalled_ShouldBeExpected()
        {
            var level = ScopeLevel.Controller;

            level.Should().NotBeNull();
            level.Name.Should().Be("Controller");
            level.Value.Should().Be("Controller");
        }

        [Test]
        public void Program_WhenCalled_ShouldBeExpected()
        {
            var level = ScopeLevel.Program;

            level.Should().NotBeNull();
            level.Name.Should().Be("Program");
            level.Value.Should().Be("Program");
        }
        
        [Test]
        public void Aoi_WhenCalled_ShouldBeExpected()
        {
            var level = ScopeLevel.Aoi;

            level.Should().NotBeNull();
            level.Name.Should().Be("Aoi");
            level.Value.Should().Be("Aoi");
        }

    }
}