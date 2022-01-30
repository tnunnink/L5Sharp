using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
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
            var component = new Port(1, "0", "ICP", false, new Bus(10));

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValidComponent_ShouldBeApproved()
        {
            var component = new Port(1, "0", "ICP", false, new Bus(10));

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValidComponentNoBus_ShouldBeApproved()
        {
            var component = new Port(1, "10.11.12.13", "Ethernet", true);

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
            const string xml = @"<Invalid Id=""1"" Address=""0"" Type=""5094"" Upstream=""false"">
                <Bus Size=""17""/>
                </Invalid>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
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
            component.Address.Should().Be("0");
            component.Type.Should().Be("5094");
            component.Upstream.Should().Be(false);
            component.Bus?.Size.Should().Be(17);
        }
        
        [Test]
        public void Deserialize_ValidElementNoBus_ShouldBeExpectedValues()
        {
            const string xml = @"<Port Id=""1"" Address=""10.11.12.13"" Type=""Ethernet"" Upstream=""true""></Port>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Id.Should().Be(1);
            component.Address.Should().Be("10.11.12.13");
            component.Type.Should().Be("Ethernet");
            component.Upstream.Should().Be(true);
            component.Bus.Should().BeNull();
        }
    }
}