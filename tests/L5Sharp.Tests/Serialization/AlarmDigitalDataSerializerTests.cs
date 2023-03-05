using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Serialization.Data;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class AlarmDigitalDataSerializerTests
    {
        private AlarmDigitalSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new AlarmDigitalSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new ALARM_DIGITAL();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        
        public Task Serialize_ValidType_ShouldBeApproved()
        {
            var component = new ALARM_DIGITAL();

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }
        
        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
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