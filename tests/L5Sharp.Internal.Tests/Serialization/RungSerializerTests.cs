using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class RungSerializerTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Rung(1, RungType.Normal, "This is a test comment", new NeutralText("XIC(Test);"));
            var serializer = new RungSerializer();

            var xml = serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = new Rung(1, RungType.Normal, "This is a test comment", new NeutralText("XIC(Test);"));
            var serializer = new RungSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var element = GenerateElement();
            var serializer = new RungSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBoolElement_ShouldHaveExpectedProperties()
        {
            var element = GenerateElement();
            var serializer = new RungSerializer();

            var component = serializer.Deserialize(element);

            component.Number.Should().Be(0);
            component.Type.Should().Be(RungType.Normal);
            component.Comment.Should().Be("This is a test comment");
            component.Text.Should().BeEquivalentTo(new NeutralText("XIC(Bit1)XIO(Bit2)OTE(Bit3);"));
        }

        private static XElement GenerateElement()
        {
            var element = new XElement("Rung");
            element.Add(new XAttribute("Number", "0"));
            element.Add(new XAttribute("Type", "N"));
            element.Add(new XAttribute("Comment", "This is a test comment"));
            element.Add(new XAttribute("Text", "XIC(Bit1)XIO(Bit2)OTE(Bit3);"));
            return element;
        }
    }
}