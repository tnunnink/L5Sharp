using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
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
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Member("Test", new DINT());

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_Bool_ShouldBeApproved()
        {
            var component = new Member("Test", new BOOL());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Sint_ShouldBeApproved()
        {
            var component = new Member("Test", new SINT());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Int_ShouldBeApproved()
        {
            var component = new Member("Test", new INT());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Dint_ShouldBeApproved()
        {
            var component = new Member("Test", new DINT());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Lint_ShouldBeApproved()
        {
            var component = new Member("Test", new LINT());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Real_ShouldBeApproved()
        {
            var component = new Member("Test", new REAL());

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

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<BOOL>();
            component.DataType.As<BOOL>().Radix.Should().Be(Radix.Decimal);
            component.DataType.As<BOOL>().Should().Be(true);
        }

        [Test]
        public void Deserialize_ValidSint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"SINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<SINT>();
            component.DataType.As<SINT>().Radix.Should().Be(Radix.Decimal);
            component.DataType.As<SINT>().Should().Be(0);
        }

        [Test]
        public void Deserialize_ValidInt_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"INT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<INT>();
            component.DataType.As<INT>().Radix.Should().Be(Radix.Decimal);
            component.DataType.As<INT>().Should().Be(0);
        }

        [Test]
        public void Deserialize_ValidDint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"DINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<DINT>();
            component.DataType.As<DINT>().Radix.Should().Be(Radix.Decimal);
            component.DataType.As<DINT>().Should().Be(0);
        }
        
        [Test]
        public void Deserialize_ValidLint_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"LINT\" Radix=\"Decimal\" Value=\"0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<LINT>();
            component.DataType.As<LINT>().Radix.Should().Be(Radix.Decimal);
            component.DataType.As<LINT>().Should().Be(0);
        }

        [Test]
        public void Deserialize_ValidReal_ShouldHaveExpectedProperties()
        {
            const string xml =
                "<DataValueMember Name=\"Test\" DataType=\"REAL\" Radix=\"Float\" Value=\"0.0\"/>";
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<REAL>();
            component.DataType.As<REAL>().Radix.Should().Be(Radix.Float);
            component.DataType.As<REAL>().Should().Be(0);
        }
    }
}