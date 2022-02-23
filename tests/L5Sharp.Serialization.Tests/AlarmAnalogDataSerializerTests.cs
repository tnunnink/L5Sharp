using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class AlarmAnalogDataSerializerTests
    {
        private AlarmAnalogDataSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new AlarmAnalogDataSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new AlarmAnalog();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValidType_ShouldBeApproved()
        {
            var component = new AlarmAnalog();

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
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'Invalid' not valid for the serializer {_serializer.GetType()}.");
        }
        
        [Test]
        public void Deserialize_SimpleArrayStructure_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        private static string GetXml()
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