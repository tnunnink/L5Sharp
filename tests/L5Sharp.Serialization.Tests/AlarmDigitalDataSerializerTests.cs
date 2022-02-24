using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class AlarmDigitalDataSerializerTests
    {
        private AlarmDigitalDataSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new AlarmDigitalDataSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new AlarmDigital();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValidType_ShouldBeApproved()
        {
            var component = new AlarmDigital();

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
                @"<AlarmDigitalParameters Severity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0"" ProgTime=""DT#1970-01-01-00:00:00.000_000Z"" EnableIn=""false"" In=""false"" InFault=""false"" Condition=""true"" AckRequired=""true"" Latched=""false""
            ProgAck=""false"" OperAck=""false"" ProgReset=""false"" OperReset=""false"" ProgSuppress=""false"" OperSuppress=""false"" ProgUnsuppress=""false"" OperUnsuppress=""false"" OperShelve=""false"" ProgUnshelve=""false"" OperUnshelve=""false""
            ProgDisable=""false"" OperDisable=""false"" ProgEnable=""false"" OperEnable=""false"" AlarmCountReset=""false"" UseProgTime=""false""/>";
        }
    }
}