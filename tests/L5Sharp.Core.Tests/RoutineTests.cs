using FluentAssertions;
using L5Sharp.Creators;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        public void Create_NonGeneric_ShouldNotBeNull()
        {
            var routine = Routine.Create("Test");

            routine.Should().NotBeNull();
        }
        
        [Test]
        public void Create_Default_ShouldNotBeNull()
        {
            var routine = Routine.Create<LadderLogic>("Test");

            routine.Should().NotBeNull();
        }

        [Test]
        public void Create_Overloaded_ShouldHaveExpectedValues()
        {
            var routine = Routine.Create<LadderLogic>("Test", "This is a test routine");

            routine.Name.Should().Be("Test");
            routine.Description.Should().Be("This is a test routine");
            routine.Type.Should().Be(RoutineType.Rll);
            routine.Content.Should().NotBeNull();
        }
    }
}