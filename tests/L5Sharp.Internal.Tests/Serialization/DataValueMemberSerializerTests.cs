using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Serialization
{
    [TestFixture]
    public class DataValueMemberSerializerTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = Member.Create<Dint>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = Member.Create<Bool>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Sint_ShouldBeApproved()
        {
            var component = Member.Create<Sint>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Int_ShouldBeApproved()
        {
            var component = Member.Create<Int>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Dint_ShouldBeApproved()
        {
            var component = Member.Create<Dint>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Lint_ShouldBeApproved()
        {
            var component = Member.Create<Lint>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Real_ShouldBeApproved()
        {
            var component = Member.Create<Real>("Test");
            var serializer = new DataValueMemberSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            const string xml =
                "<DataValueMember Name=\"DiagnosticSequenceCount\" DataType=\"SINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);
            var serializer = new DataValueMemberSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBoolElement_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"DiagnosticSequenceCount\" DataType=\"SINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);
            var serializer = new DataValueMemberSerializer();

            var component = (Member<IAtomicType>) serializer.Deserialize(element);

            component.Name.Should().Be("DiagnosticSequenceCount");
            component.DataType.Should().BeOfType<Sint>();
            component.Radix.Should().Be(Radix.Decimal);
            component.DataType.Value.Should().Be(0);
        }
    }
}