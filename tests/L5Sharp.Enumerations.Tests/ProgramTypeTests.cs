using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Enumerations.Tests
{
    [TestFixture]
    public class ProgramTypeTests
    {
        [Test]
        public void New_Normal_ShouldNotBeNullAndHaveExpectedName()
        {
            var type = ProgramType.Normal;

            type.Should().NotBeNull();
            type.Name.Should().Be("Normal");
        }

        [Test]
        public void New_EquipmentPhase_ShouldNotBeNullAndHaveExpectedName()
        {
            var type = ProgramType.EquipmentPhase;

            type.Should().NotBeNull();
            type.Name.Should().Be("EquipmentPhase");
        }
        
        [Test]
        public void Create_Normal_ProgramShouldNotBeNull()
        {
            var type = ProgramType.Normal;

            var program = type.Create("Test");

            program.Should().NotBeNull();
            program.Name.Should().Be("Test");
        }
        
        [Test]
        public void CreateGeneric_Normal_ProgramShouldNotBeNull()
        {
            
            var program = ProgramType.Create<Program>("Test");

            program.Type.Should().Be(ProgramType.Normal);
        }
    }
}