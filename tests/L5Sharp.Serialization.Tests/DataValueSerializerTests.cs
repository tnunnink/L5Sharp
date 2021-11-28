using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataValueSerializerTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Dint();
            var serializer = new DataValueSerializer();

            var xml = serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = new Bool();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Sint_ShouldBeApproved()
        {
            var component = new Sint();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Int_ShouldBeApproved()
        {
            var component = new Int();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Dint_ShouldBeApproved()
        {
            var component = new Dint();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Lint_ShouldBeApproved()
        {
            var component = new Lint();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Real_ShouldBeApproved()
        {
            var component = new Real();
            var serializer = new DataValueSerializer();
            
            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var element = new XElement("DataValue");
            var serializer = new DataValueSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_ValidElement_ShouldHaveExpectedProperties()
        {
            var element = new XElement("DataValue");
            element.Add(new XAttribute("DataType", "BOOL"));
            element.Add(new XAttribute("Radix", "Decimal"));
            element.Add(new XAttribute("Value", "0"));
            var serializer = new DataValueSerializer();

            var component = serializer.Deserialize(element);

            component.Name.Should().Be("Bool");
            component.Radix.Should().Be(Radix.Decimal);
            component.Value.Should().Be(false);
        }
    }
}