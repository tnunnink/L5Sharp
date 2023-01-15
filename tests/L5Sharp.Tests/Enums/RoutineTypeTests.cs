using FluentAssertions;
using L5Sharp.Enums;
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
            var sut = RoutineType.Rll;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Rll");
        }
        
        [Test]
        public void New_FunctionBlock_ShouldNotBeNull()
        {
            var sut = RoutineType.Fbd;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Fbd");
        }
        
        [Test]
        public void New_SequentialFunction_ShouldNotBeNull()
        {
            var sut = RoutineType.Sfc;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Sfc");
        }
        
        [Test]
        public void New_StructuredText_ShouldNotBeNull()
        {
            var sut = RoutineType.St;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("St");
        }
    }
}