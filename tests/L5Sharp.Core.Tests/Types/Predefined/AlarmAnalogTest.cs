using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Core.Tests.Types.Predefined
{
    [TestFixture]
    public class AlarmAnalogTest
    {
        [Test]
        public void New__WhenCalled_ShouldNotBeNull()
        {
            var type = new ALARM_ANALOG();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new ALARM_ANALOG();

            type.Name.Should().Be(nameof(ALARM_ANALOG));
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Members.Should().HaveCount(65);
            type.EnableIn.Should().Be(0);
            type.In.Should().Be(0);
            type.InFault.Should().Be(0);
            type.HHEnabled.Should().Be(0);
            type.HEnabled.Should().Be(0);
            type.LEnabled.Should().Be(0);
            type.LLEnabled.Should().Be(0);
            type.AckRequired.Should().Be(0);
            type.ProgAckAll.Should().Be(0);
            type.OperAckAll.Should().Be(0);
            type.HHProgAck.Should().Be(0);
            type.HHOperAck.Should().Be(0);
            type.HProgAck.Should().Be(0);
            type.HOperAck.Should().Be(0);
            type.LProgAck.Should().Be(0);
            type.LOperAck.Should().Be(0);
            type.LLProgAck.Should().Be(0);
            type.LLOperAck.Should().Be(0);
            type.ROCPosProgAck.Should().Be(0);
            type.ROCPosOperAck.Should().Be(0);
            type.ROCNegProgAck.Should().Be(0);
            type.ROCNegOperAck.Should().Be(0);
            type.ProgSuppress.Should().Be(0);
            type.OperSuppress.Should().Be(0);
            type.ProgUnsuppress.Should().Be(0);
            type.OperUnsuppress.Should().Be(0);
            type.HHOperShelve.Should().Be(0);
            type.HOperShelve.Should().Be(0);
            type.LOperShelve.Should().Be(0);
            type.LLOperShelve.Should().Be(0);
            type.ROCPosOperShelve.Should().Be(0);
            type.ROCNegOperShelve.Should().Be(0);
            type.ProgUnshelveAll.Should().Be(0);
            type.HHOperUnshelve.Should().Be(0);
            type.HOperUnshelve.Should().Be(0);
            type.LOperUnshelve.Should().Be(0);
            type.LLOperUnshelve.Should().Be(0);
            type.ROCPosOperUnshelve.Should().Be(0);
            type.ROCNegOperUnshelve.Should().Be(0);
            type.ProgDisable.Should().Be(0);
            type.OperDisable.Should().Be(0);
            type.ProgEnable.Should().Be(0);
            type.OperEnable.Should().Be(0);
            type.AlarmCountReset.Should().Be(0);
            type.HHMinDurationEnable.Should().Be(0);
            type.HMinDurationEnable.Should().Be(0);
            type.LMinDurationEnable.Should().Be(0);
            type.LLMinDurationEnable.Should().Be(0);
            type.HHLimit.Should().Be(0);
            type.HHSeverity.Should().Be(0);
            type.HLimit.Should().Be(0);
            type.HSeverity.Should().Be(0);
            type.LLimit.Should().Be(0);
            type.LSeverity.Should().Be(0);
            type.LLLimit.Should().Be(0);
            type.LLSeverity.Should().Be(0);
            type.MinDurationPRE.Should().Be(0);
            type.ShelveDuration.Should().Be(0);
            type.MaxShelveDuration.Should().Be(0);
            type.Deadband.Should().Be(0);
            type.ROCPosLimit.Should().Be(0);
            type.ROCPosSeverity.Should().Be(0);
            type.ROCNegLimit.Should().Be(0);
            type.ROCNegSeverity.Should().Be(0);
            type.ROCPeriod.Should().Be(0);
        }

        [Test]
        public void New_NullElement_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ALARM_ANALOG(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_InvalidElement_ShouldNotBeNull()
        {
            const string xml = @"<AlarmAnalogParameters />";
            var element = XElement.Parse(xml);

            var type = new ALARM_ANALOG(element);

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_InvalidElement_ShouldThrowInvalidOperationExceptionWhenGettingProperty()
        {
            const string xml = @"<AlarmAnalogParameters />";
            var element = XElement.Parse(xml);

            var type = new ALARM_ANALOG(element);

            FluentActions.Invoking(() => type.EnableIn).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void New_RealElement_ShouldHaveExpectedValues()
        {
            const string xml =
                @"<AlarmAnalogParameters EnableIn=""false"" InFault=""false"" HHEnabled=""false"" HEnabled=""false""
                                           LEnabled=""false"" LLEnabled=""false"" AckRequired=""true"" ProgAckAll=""false""
                                           OperAckAll=""false"" HHProgAck=""false"" HHOperAck=""false""
                                           HProgAck=""false"" HOperAck=""false"" LProgAck=""false"" LOperAck=""false""
                                           LLProgAck=""false"" LLOperAck=""false"" ROCPosProgAck=""false""
                                           ROCPosOperAck=""false"" ROCNegProgAck=""false"" ROCNegOperAck=""false""
                                           ProgSuppress=""false""
                                           OperSuppress=""false"" ProgUnsuppress=""false"" OperUnsuppress=""false""
                                           HHOperShelve=""false"" HOperShelve=""false"" LOperShelve=""false""
                                           LLOperShelve=""false"" ROCPosOperShelve=""false"" ROCNegOperShelve=""false""
                                           ProgUnshelveAll=""false"" HHOperUnshelve=""false""
                                           HOperUnshelve=""false"" LOperUnshelve=""false"" LLOperUnshelve=""false""
                                           ROCPosOperUnshelve=""false"" ROCNegOperUnshelve=""false"" ProgDisable=""false""
                                           OperDisable=""false"" ProgEnable=""false"" OperEnable=""false""
                                           AlarmCountReset=""false"" HHMinDurationEnable=""true""
                                           HMinDurationEnable=""true"" LMinDurationEnable=""true""
                                           LLMinDurationEnable=""true"" In=""0.0"" HHLimit=""0.0"" HHSeverity=""500""
                                           HLimit=""0.0"" HSeverity=""500"" LLimit=""0.0"" LSeverity=""500"" LLLimit=""0.0""
                                           LLSeverity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0""
                                           Deadband=""0.0"" ROCPosLimit=""0.0"" ROCPosSeverity=""500"" ROCNegLimit=""0.0""
                                           ROCNegSeverity=""500"" ROCPeriod=""0.0""/>";
            var element = XElement.Parse(xml);

            var type = new ALARM_ANALOG(element);

            type.Should().NotBeNull();
            type.EnableIn.Should().Be(0);
            type.In.Should().Be(0);
            type.InFault.Should().Be(0);
            type.HHEnabled.Should().Be(0);
            type.HEnabled.Should().Be(0);
            type.LEnabled.Should().Be(0);
            type.LLEnabled.Should().Be(0);
            type.AckRequired.Should().Be(1);
            type.ProgAckAll.Should().Be(0);
            type.OperAckAll.Should().Be(0);
            type.HHProgAck.Should().Be(0);
            type.HHOperAck.Should().Be(0);
            type.HProgAck.Should().Be(0);
            type.HOperAck.Should().Be(0);
            type.LProgAck.Should().Be(0);
            type.LOperAck.Should().Be(0);
            type.LLProgAck.Should().Be(0);
            type.LLOperAck.Should().Be(0);
            type.ROCPosProgAck.Should().Be(0);
            type.ROCPosOperAck.Should().Be(0);
            type.ROCNegProgAck.Should().Be(0);
            type.ROCNegOperAck.Should().Be(0);
            type.ProgSuppress.Should().Be(0);
            type.OperSuppress.Should().Be(0);
            type.ProgUnsuppress.Should().Be(0);
            type.OperUnsuppress.Should().Be(0);
            type.HHOperShelve.Should().Be(0);
            type.HOperShelve.Should().Be(0);
            type.LOperShelve.Should().Be(0);
            type.LLOperShelve.Should().Be(0);
            type.ROCPosOperShelve.Should().Be(0);
            type.ROCNegOperShelve.Should().Be(0);
            type.ProgUnshelveAll.Should().Be(0);
            type.HHOperUnshelve.Should().Be(0);
            type.HOperUnshelve.Should().Be(0);
            type.LOperUnshelve.Should().Be(0);
            type.LLOperUnshelve.Should().Be(0);
            type.ROCPosOperUnshelve.Should().Be(0);
            type.ROCNegOperUnshelve.Should().Be(0);
            type.ProgDisable.Should().Be(0);
            type.OperDisable.Should().Be(0);
            type.ProgEnable.Should().Be(0);
            type.OperEnable.Should().Be(0);
            type.AlarmCountReset.Should().Be(0);
            type.HHMinDurationEnable.Should().Be(1);
            type.HMinDurationEnable.Should().Be(1);
            type.LMinDurationEnable.Should().Be(1);
            type.LLMinDurationEnable.Should().Be(1);
            type.HHLimit.Should().Be(0);
            type.HHSeverity.Should().Be(500);
            type.HLimit.Should().Be(0);
            type.HSeverity.Should().Be(500);
            type.LLimit.Should().Be(0);
            type.LSeverity.Should().Be(500);
            type.LLLimit.Should().Be(0);
            type.LLSeverity.Should().Be(500);
            type.MinDurationPRE.Should().Be(0);
            type.ShelveDuration.Should().Be(0);
            type.MaxShelveDuration.Should().Be(0);
            type.Deadband.Should().Be(0);
            type.ROCPosLimit.Should().Be(0);
            type.ROCPosSeverity.Should().Be(500);
            type.ROCNegLimit.Should().Be(0);
            type.ROCNegSeverity.Should().Be(500);
            type.ROCPeriod.Should().Be(0);
        }

        [Test]
        public Task Serialize_WhenCalled_ShouldBeVerified()
        {
            var type = new ALARM_ANALOG();
            
            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}