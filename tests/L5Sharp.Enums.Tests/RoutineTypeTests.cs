using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
            sut.Name.Should().Be("RLL");
        }
        
        [Test]
        public void New_FunctionBlock_ShouldNotBeNull()
        {
            var sut = RoutineType.Fbd;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("FBD");
        }
        
        [Test]
        public void New_SequentialFunction_ShouldNotBeNull()
        {
            var sut = RoutineType.Sfc;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("SFC");
        }
        
        [Test]
        public void New_StructuredText_ShouldNotBeNull()
        {
            var sut = RoutineType.St;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("ST");
        }
        
        /*[Test]
        public void New_External_ShouldNotBeNull()
        {
            var sut = RoutineType.External;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("External");
        }
        
        [Test]
        public void New_Encrypted_ShouldNotBeNull()
        {
            var sut = RoutineType.Encrypted;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Encrypted");
        }*/
    }
}