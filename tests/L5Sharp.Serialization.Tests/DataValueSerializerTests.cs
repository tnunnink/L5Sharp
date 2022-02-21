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
    public class DataValueSerializerTests
    {
        private DataValueSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new DataValueSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Dint();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = new Bool();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Sint_ShouldBeApproved()
        {
            var component = new Sint();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Int_ShouldBeApproved()
        {
            var component = new Int();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Dint_ShouldBeApproved()
        {
            var component = new Dint();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Lint_ShouldBeApproved()
        {
            var component = new Lint();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Real_ShouldBeApproved()
        {
            var component = new Real();

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
        public void Deserialize_Valid_ShouldNotBeNull()
        {
            const string xml = "<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }
    }
}