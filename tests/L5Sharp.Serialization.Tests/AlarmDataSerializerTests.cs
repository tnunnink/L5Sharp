using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class AlarmDataSerializerTests
    {
        private AlarmDataSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new AlarmDataSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_InvalidType_ShouldThrowArgumentException()
        {
            var component = new TIMER();

            FluentActions.Invoking(() => _serializer.Serialize(component)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_AlarmDigital_ShouldNotBeNull()
        {
            var component = new ALARM_DIGITAL();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        public void Serialize_AlarmAnalog_ShouldNotBeNull()
        {
            var component = new ALARM_ANALOG();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_AlarmDigital_ShouldBeApproved()
        {
            var component = new ALARM_DIGITAL();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_AlarmAnalog_ShouldBeApproved()
        {
            var component = new ALARM_ANALOG();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<DataValue></DataValue>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'DataValue' not valid for the serializer {_serializer.GetType()}.");
        }

        [Test]
        public void Deserialize_DigitalParameters_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetDigitalParameters());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_AnalogParameters_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetAnalogParameters());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        private static string GetDigitalParameters()
        {
            return
                @"<AlarmDigitalParameters Severity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0"" ProgTime=""DT#1970-01-01-00:00:00.000_000Z"" EnableIn=""false"" In=""false"" InFault=""false"" Condition=""true"" AckRequired=""true"" Latched=""false""
            ProgAck=""false"" OperAck=""false"" ProgReset=""false"" OperReset=""false"" ProgSuppress=""false"" OperSuppress=""false"" ProgUnsuppress=""false"" OperUnsuppress=""false"" OperShelve=""false"" ProgUnshelve=""false"" OperUnshelve=""false""
            ProgDisable=""false"" OperDisable=""false"" ProgEnable=""false"" OperEnable=""false"" AlarmCountReset=""false"" UseProgTime=""false""/>";
        }
        
        private static string GetAnalogParameters()
        {
            return
                @"<AlarmAnalogParameters EnableIn=""false"" InFault=""false"" HHEnabled=""false"" HEnabled=""false"" LEnabled=""false"" LLEnabled=""false"" AckRequired=""true"" ProgAckAll=""false"" OperAckAll=""false"" HHProgAck=""false"" HHOperAck=""false""
            HProgAck=""false"" HOperAck=""false"" LProgAck=""false"" LOperAck=""false"" LLProgAck=""false"" LLOperAck=""false"" ROCPosProgAck=""false"" ROCPosOperAck=""false"" ROCNegProgAck=""false"" ROCNegOperAck=""false"" ProgSuppress=""false""
            OperSuppress=""false"" ProgUnsuppress=""false"" OperUnsuppress=""false"" HHOperShelve=""false"" HOperShelve=""false"" LOperShelve=""false"" LLOperShelve=""false"" ROCPosOperShelve=""false"" ROCNegOperShelve=""false"" ProgUnshelveAll=""false"" HHOperUnshelve=""false""
            HOperUnshelve=""false"" LOperUnshelve=""false"" LLOperUnshelve=""false"" ROCPosOperUnshelve=""false"" ROCNegOperUnshelve=""false"" ProgDisable=""false"" OperDisable=""false"" ProgEnable=""false"" OperEnable=""false"" AlarmCountReset=""false"" HHMinDurationEnable=""true""
            HMinDurationEnable=""true"" LMinDurationEnable=""true"" LLMinDurationEnable=""true"" In=""0.0"" HHLimit=""0.0"" HHSeverity=""500"" HLimit=""0.0"" HSeverity=""500"" LLimit=""0.0"" LSeverity=""500"" LLLimit=""0.0""
            LLSeverity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0"" Deadband=""0.0"" ROCPosLimit=""0.0"" ROCPosSeverity=""500"" ROCNegLimit=""0.0"" ROCNegSeverity=""500"" ROCPeriod=""0.0""/>";
        }
    }
}