using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
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