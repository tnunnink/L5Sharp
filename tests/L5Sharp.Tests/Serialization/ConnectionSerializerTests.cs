using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    public class ConnectionSerializerTests
    {
        private ModuleConnectionSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ModuleConnectionSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new ModuleConnection
            {
                Name = "Connection",
                Rpi = 10000,
                Type = ConnectionType.Input
            };

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_ValidComponent_ShouldBeApproved()
        {
            var component = new ModuleConnection
            {
                Name = "Connection",
                Rpi = 10000,
                Type = ConnectionType.Input
            };

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            const string xml = @"<Connection Name=""Output"" RPI=""10000"" Type=""Output"" EventID=""0""
            ProgrammaticallySendEventTrigger=""false"" Unicast=""false""></Connection>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldBeExpectedValues()
        {
            const string xml = @"<Connection Name=""Output"" RPI=""10000"" Type=""Output"" EventID=""0""
            ProgrammaticallySendEventTrigger=""false"" Unicast=""false""></Connection>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Output");
            component.Rpi.Should().Be(10000);
            component.Type.Should().Be(ConnectionType.Output);
            component.Priority.Should().Be(ConnectionPriority.Scheduled);
            component.Unicast.Should().BeFalse();
        }
    }
}