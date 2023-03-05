using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class RungSerializerTests
    {
        private RungSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new RungSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Rung
            {
                Number = 1,
                Type = RungType.Normal,
                Comment = "This is a test comment",
                Text = new NeutralText("XIC(Test);")
            };

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_Bool_ShouldBeApproved()
        {
            var component = new Rung
            {
                Number = 1,
                Type = RungType.Normal,
                Comment = "This is a test comment",
                Text = new NeutralText("XIC(Test);")
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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'Invalid' not valid for the serializer {_serializer.GetType()}.");
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var element = GenerateElement();

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBoolElement_ShouldHaveExpectedProperties()
        {
            var element = GenerateElement();

            var component = _serializer.Deserialize(element);

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
            element.Add(new XElement("Comment", "This is a test comment"));
            element.Add(new XElement("Text", "XIC(Bit1)XIO(Bit2)OTE(Bit3);"));
            return element;
        }
    }
}