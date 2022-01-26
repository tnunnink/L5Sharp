using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataValueMemberSerializerTests
    {
        private DataValueMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new DataValueMemberSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_NonAtomic_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(new Member<IDataType>("InvalidType", new Timer())))
                .Should().Throw<ArgumentException>()
                .WithMessage("DataValueMember must have an atomic data type.");
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = Member.Create<Dint>("Test");

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = Member.Create<Bool>("Test");

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Sint_ShouldBeApproved()
        {
            var component = Member.Create<Sint>("Test");

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Int_ShouldBeApproved()
        {
            var component = Member.Create<Int>("Test");

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Dint_ShouldBeApproved()
        {
            var component = Member.Create<Dint>("Test");

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Lint_ShouldBeApproved()
        {
            var component = Member.Create<Lint>("Test");

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Real_ShouldBeApproved()
        {
            var component = Member.Create<Real>("Test");

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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_Valid_ShouldNotBeNull()
        {
            const string xml =
                "<DataValueMember Name=\"DiagnosticSequenceCount\" DataType=\"SINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBool_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"BOOL\" Value=\"1\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Bool>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(true);
        }

        [Test]
        public void Deserialize_ValidSint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"SINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Sint>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(0);
        }

        [Test]
        public void Deserialize_ValidInt_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"INT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Int>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(0);
        }


        [Test]
        public void Deserialize_ValidDint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Dint>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(0);
        }


        [Test]
        public void Deserialize_ValidLint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"LINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Lint>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(0);
        }

        [Test]
        public void Deserialize_ValidReal_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"REAL\" Radix=\"Float\" Value=\"0.0\"/>";
            var element = XElement.Parse(xml);

            var component = (Member<IAtomicType>)_serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Real>();
            component.Radix.Should().Be(Radix.Float);
            component.DataType.Value.Should().Be(0);
        }
    }
}