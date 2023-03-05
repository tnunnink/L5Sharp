using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class PortSerializerTests
    {
        private PortSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new PortSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Port
            {
                Id = 1,
                Type = "ICP",
                Address = "0"
            };

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_ValidComponent_ShouldBeApproved()
        {
            var component = new Port
            {
                Id = 1,
                Type = "ICP",
                Address = "0",
                Upstream = false,
                BusSize = 10
            };

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_ValidComponentNoBus_ShouldBeApproved()
        {
            var component = new Port
            {
                Id = 1,
                Type = "Ethernet",
                Address = "10.11.12.13",
                Upstream = true,
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
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            const string xml = @"<Port Id=""1"" Address=""0"" Type=""5094"" Upstream=""false"">
                <Bus Size=""17""/>
                </Port>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldBeExpectedValues()
        {
            const string xml = @"<Port Id=""1"" Address=""0"" Type=""5094"" Upstream=""false"">
                <Bus Size=""17""/>
                </Port>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Id.Should().Be(1);
            component.Address.Should().Be(new Address("0"));
            component.Type.Should().Be("5094");
            component.Upstream.Should().Be(false);
            component.BusSize.Should().Be(17);
        }

        [Test]
        public void Deserialize_ValidElementNoBus_ShouldBeExpectedValues()
        {
            const string xml = @"<Port Id=""1"" Address=""10.11.12.13"" Type=""Ethernet"" Upstream=""true""></Port>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Id.Should().Be(1);
            component.Address.Should().Be(new Address("10.11.12.13"));
            component.Type.Should().Be("Ethernet");
            component.Upstream.Should().Be(true);
            component.BusSize.Should().Be(0);
        }
    }
}