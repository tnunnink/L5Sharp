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
            var component = new DINT();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var component = new BOOL();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Sint_ShouldBeApproved()
        {
            var component = new SINT();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Int_ShouldBeApproved()
        {
            var component = new INT();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Dint_ShouldBeApproved()
        {
            var component = new DINT();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Lint_ShouldBeApproved()
        {
            var component = new LINT();

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Real_ShouldBeApproved()
        {
            var component = new REAL();

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
        
        [Test]
        [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Decimal\" Value=\"1\"/>")]
        [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Binary\" Value=\"2#1\"/>")]
        [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Octal\" Value=\"8#1\"/>")]
        [TestCase("<DataValue DataType=\"BOOL\" Radix=\"Hex\" Value=\"16#1\"/>")]
        public void Deserialize_BoolAsDecimal_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<BOOL>();
            component.Name.Should().Be("BOOL");
            component.Value.Should().Be(true);  
        }
        
        [Test]
        [TestCase("<DataValue DataType=\"SINT\" Radix=\"Decimal\" Value=\"12\"/>")]
        [TestCase("<DataValue DataType=\"SINT\" Radix=\"Binary\" Value=\"2#0000_1100\"/>")]
        [TestCase("<DataValue DataType=\"SINT\" Radix=\"Octal\" Value=\"8#014\"/>")]
        [TestCase("<DataValue DataType=\"SINT\" Radix=\"Hex\" Value=\"16#0c\"/>")]
        [TestCase("<DataValue DataType=\"SINT\" Radix=\"ASCII\" Value=\"'$p'\"/>")]
        public void Deserialize_Sint_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<SINT>();
            component.Name.Should().Be("SINT");
            component.Value.Should().Be(12);  
        }

        [Test]
        [TestCase("<DataValue DataType=\"INT\" Radix=\"Decimal\" Value=\"4321\"/>")]
        [TestCase("<DataValue DataType=\"INT\" Radix=\"Binary\" Value=\"2#0001_0000_1110_0001\"/>")]
        [TestCase("<DataValue DataType=\"INT\" Radix=\"Octal\" Value=\"8#010_341\"/>")]
        [TestCase("<DataValue DataType=\"INT\" Radix=\"Hex\" Value=\"16#10e1\"/>")]
        [TestCase("<DataValue DataType=\"INT\" Radix=\"ASCII\" Value=\"'$10$E1'\"/>")]
        public void Deserialize_Int_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<INT>();
            component.Name.Should().Be("INT");
            component.Value.Should().Be(4321);  
        }
        
        [Test]
        [TestCase("<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"123456\"/>")]
        [TestCase("<DataValue DataType=\"DINT\" Radix=\"Binary\" Value=\"2#0000_0000_0000_0001_1110_0010_0100_0000\"/>")]
        [TestCase("<DataValue DataType=\"DINT\" Radix=\"Octal\" Value=\"8#00_000_361_100\"/>")]
        [TestCase("<DataValue DataType=\"DINT\" Radix=\"Hex\" Value=\"16#0001_e240\"/>")]
        [TestCase("<DataValue DataType=\"DINT\" Radix=\"ASCII\" Value=\"'$00$01$E2@'\"/>")]
        public void Deserialize_Dint_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<DINT>();
            component.Name.Should().Be("DINT");
            component.Value.Should().Be(123456);  
        }
        
        [Test]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"Decimal\" Value=\"112230123\"/>")]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"Binary\" Value=\"2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0110_1011_0000_0111_1110_1110_1011\"/>")]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"Octal\" Value=\"8#0_000_000_000_000_654_077_353\"/>")]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"Hex\" Value=\"16#0000_0000_06b0_7eeb\"/>")]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"ASCII\" Value=\"'$00$00$00$00$06$B0~$EB'\"/>")]
        [TestCase("<DataValue DataType=\"LINT\" Radix=\"ASCII\" Value=\"DT#1970-01-01-00:01:52.230_123Z\"/>")]
        public void Deserialize_Lint_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<LINT>();
            component.Name.Should().Be("LINT");
            component.Value.Should().Be(112230123);  
        }
        
        [Test]
        [TestCase("<DataValue DataType=\"REAL\" Radix=\"Float\" Value=\"1.23\"/>")]
        [TestCase("<DataValue DataType=\"REAL\" Radix=\"Exponential\" Value=\"1.23000000e+000\"/>")]
        public void Deserialize_Real_ShouldBeExpected(string xml)
        {
            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().BeOfType<REAL>();
            component.Name.Should().Be("REAL");
            component.Value.Should().Be(1.23f);  
        }
    }
}