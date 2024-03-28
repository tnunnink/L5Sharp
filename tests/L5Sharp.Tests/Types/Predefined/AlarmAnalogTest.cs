using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Predefined
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
            //type.Members.Should().HaveCount(65);
            type.Members.Should().BeEmpty();
            type.EnableIn.Should().Be(false);
            type.In.Should().Be(0);
            type.InFault.Should().Be(false);
            type.HHEnabled.Should().Be(false);
            type.HEnabled.Should().Be(false);
            type.LEnabled.Should().Be(false);
            type.LLEnabled.Should().Be(false);
            type.AckRequired.Should().Be(false);
            type.ProgAckAll.Should().Be(false);
            type.OperAckAll.Should().Be(false);
            type.HHProgAck.Should().Be(false);
            type.HHOperAck.Should().Be(false);
            type.HProgAck.Should().Be(false);
            type.HOperAck.Should().Be(false);
            type.LProgAck.Should().Be(false);
            type.LOperAck.Should().Be(false);
            type.LLProgAck.Should().Be(false);
            type.LLOperAck.Should().Be(false);
            type.ROCPosProgAck.Should().Be(false);
            type.ROCPosOperAck.Should().Be(false);
            type.ROCNegProgAck.Should().Be(false);
            type.ROCNegOperAck.Should().Be(false);
            type.ProgSuppress.Should().Be(false);
            type.OperSuppress.Should().Be(false);
            type.ProgUnsuppress.Should().Be(false);
            type.OperUnsuppress.Should().Be(false);
            type.HHOperShelve.Should().Be(false);
            type.HOperShelve.Should().Be(false);
            type.LOperShelve.Should().Be(false);
            type.LLOperShelve.Should().Be(false);
            type.ROCPosOperShelve.Should().Be(false);
            type.ROCNegOperShelve.Should().Be(false);
            type.ProgUnshelveAll.Should().Be(false);
            type.HHOperUnshelve.Should().Be(false);
            type.HOperUnshelve.Should().Be(false);
            type.LOperUnshelve.Should().Be(false);
            type.LLOperUnshelve.Should().Be(false);
            type.ROCPosOperUnshelve.Should().Be(false);
            type.ROCNegOperUnshelve.Should().Be(false);
            type.ProgDisable.Should().Be(false);
            type.OperDisable.Should().Be(false);
            type.ProgEnable.Should().Be(false);
            type.OperEnable.Should().Be(false);
            type.AlarmCountReset.Should().Be(false);
            type.HHMinDurationEnable.Should().Be(false);
            type.HMinDurationEnable.Should().Be(false);
            type.LMinDurationEnable.Should().Be(false);
            type.LLMinDurationEnable.Should().Be(false);
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
            type.EnableIn.Should().Be(false);
            type.In.Should().Be(0);
            type.InFault.Should().Be(false);
            type.HHEnabled.Should().Be(false);
            type.HEnabled.Should().Be(false);
            type.LEnabled.Should().Be(false);
            type.LLEnabled.Should().Be(false);
            type.AckRequired.Should().Be(true);
            type.ProgAckAll.Should().Be(false);
            type.OperAckAll.Should().Be(false);
            type.HHProgAck.Should().Be(false);
            type.HHOperAck.Should().Be(false);
            type.HProgAck.Should().Be(false);
            type.HOperAck.Should().Be(false);
            type.LProgAck.Should().Be(false);
            type.LOperAck.Should().Be(false);
            type.LLProgAck.Should().Be(false);
            type.LLOperAck.Should().Be(false);
            type.ROCPosProgAck.Should().Be(false);
            type.ROCPosOperAck.Should().Be(false);
            type.ROCNegProgAck.Should().Be(false);
            type.ROCNegOperAck.Should().Be(false);
            type.ProgSuppress.Should().Be(false);
            type.OperSuppress.Should().Be(false);
            type.ProgUnsuppress.Should().Be(false);
            type.OperUnsuppress.Should().Be(false);
            type.HHOperShelve.Should().Be(false);
            type.HOperShelve.Should().Be(false);
            type.LOperShelve.Should().Be(false);
            type.LLOperShelve.Should().Be(false);
            type.ROCPosOperShelve.Should().Be(false);
            type.ROCNegOperShelve.Should().Be(false);
            type.ProgUnshelveAll.Should().Be(false);
            type.HHOperUnshelve.Should().Be(false);
            type.HOperUnshelve.Should().Be(false);
            type.LOperUnshelve.Should().Be(false);
            type.LLOperUnshelve.Should().Be(false);
            type.ROCPosOperUnshelve.Should().Be(false);
            type.ROCNegOperUnshelve.Should().Be(false);
            type.ProgDisable.Should().Be(false);
            type.OperDisable.Should().Be(false);
            type.ProgEnable.Should().Be(false);
            type.OperEnable.Should().Be(false);
            type.AlarmCountReset.Should().Be(false);
            type.HHMinDurationEnable.Should().Be(true);
            type.HMinDurationEnable.Should().Be(true);
            type.LMinDurationEnable.Should().Be(true);
            type.LLMinDurationEnable.Should().Be(true);
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