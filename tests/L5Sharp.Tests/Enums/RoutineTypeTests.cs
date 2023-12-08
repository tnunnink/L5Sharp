using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class RoutineTypeTests
    {
        [Test]
        public void New_Typeless_ShouldNotBeNull()
        {
            var sut = RoutineType.Typeless;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Typeless");
        }
        
        [Test]
        public void New_Ladder_ShouldNotBeNull()
        {
            var sut = RoutineType.RLL;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("RLL");
        }
        
        [Test]
        public void New_FunctionBlock_ShouldNotBeNull()
        {
            var sut = RoutineType.FBD;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("FBD");
        }
        
        [Test]
        public void New_SequentialFunction_ShouldNotBeNull()
        {
            var sut = RoutineType.SFC;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("SFC");
        }
        
        [Test]
        public void New_StructuredText_ShouldNotBeNull()
        {
            var sut = RoutineType.ST;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("ST");
        }
    }
}