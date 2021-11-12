using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
    }
}